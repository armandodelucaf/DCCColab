using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ServiceModel;
using DCC.COLAB.WCF.Interface;
using DCCFramework.Utilitarios;
using DCC.COLAB.Web.Util;

namespace DCC.COLAB.Web.Util
{
    public class WCFDispatcher<T> where T : class
    {
        private static Dictionary<Type, Type> proxies;
        private static Dictionary<Type, object> channelFactories = new Dictionary<Type, object>();

        static WCFDispatcher()
        {
            InitChannelFactories();
        }

        private static void InitChannelFactories()
        {
            var channelFactoryCOLAB = new ChannelFactory<ICOLABServico>("DCC.COLAB.COLABServico");
            channelFactories.Add(typeof(ICOLABServico), channelFactoryCOLAB);
        }

        private static ClientBase<TService> Proxy<TService>() where TService : class
        {
            var type = proxies[typeof(TService)];
            return (ClientBase<TService>)Activator.CreateInstance(type);
        }

        public static void FecharProxy(ClientBase<T> proxy)
        {
            try
            {
                if (proxy.State != System.ServiceModel.CommunicationState.Faulted)
                {
                    proxy.Close();
                }
                else
                {
                    proxy.Abort();
                }
            }
            catch (Exception)
            {
                proxy.Abort();
                throw;
            }
        }

        //Using ChannelFactory
        public static TReturn UseService<TReturn>(Func<T, TReturn> code)
        {
            var chanFactory = GetCachedFactory();
            T channel = chanFactory.CreateChannel();
            bool error = true;
            try
            {
                TReturn result;
                using (OperationContextScope escopo = new OperationContextScope((IClientChannel)channel))
                {
                    ColocarUsuarioNoHeader();
                    result = code(channel);
                }

                ((IClientChannel)channel).Close();
                error = false;
                return result;
            }
            catch (Exception ex)
            {
                var erro = ex;
                throw ex;
            }
            finally
            {
                if (error)
                {
                    ((IClientChannel)channel).Abort();
                }
            }
        }

        //Using ChannelFactory
        public static void UseService(Action<T> code)
        {
            var chanFactory = GetCachedFactory();
            T channel = chanFactory.CreateChannel();
            bool error = true;
            try
            {
                using (OperationContextScope escopo = new OperationContextScope((IClientChannel)channel))
                {
                    ColocarUsuarioNoHeader();
                    code(channel);
                }
                ((IClientChannel)channel).Close();
                error = false;
            }
            catch (Exception ex)
            {
                var erro = ex;
                throw ex;
            }
            finally
            {
                if (error)
                {
                    ((IClientChannel)channel).Abort();
                }
            }
        }

        private static ChannelFactory<T> GetCachedFactory()
        {
            if (!channelFactories.ContainsKey(typeof(T)))
            {
                throw new Exception("Não foi encontrado configuração de endpoint WCF para " + typeof(T).ToString() + ". ");
            }
            return (ChannelFactory<T>)channelFactories[typeof(T)];
        }

        private static void ColocarUsuarioNoHeader()
        {
            var usuario = string.Empty;
            try
            {
                usuario = SessaoUtil.UsuarioLogin;
            }
            catch (Exception)
            {
                usuario = "-";
            }
            MessageHeader<String> header = new MessageHeader<String>();
            header.Content = usuario;
            header.Actor = "";
            System.ServiceModel.Channels.MessageHeader untypedHeader = header.GetUntypedHeader("usuario", "ns");
            OperationContext.Current.OutgoingMessageHeaders.Add(untypedHeader);
        }
    }
}