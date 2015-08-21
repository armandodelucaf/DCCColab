using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DCCFramework
{
    public class GerenciadorDoArquivoDeQueries
    {
        private static GerenciadorDoArquivoDeQueries gerenciadorDoArquivoDeQueries = null;
        private Dictionary<string, string> queries;
        private const string CHAVE_NOME_ARQUIVO_QUERIES = "nomeArquivoDeQueries";
        private const string NO_RAIZ_DO_ARQUIVO_DE_QUERIES = "Queries";

        private GerenciadorDoArquivoDeQueries()
        {
            queries = new Dictionary<string, string>();
        }

        public static GerenciadorDoArquivoDeQueries Instancia
        {
            get
            {
                if (gerenciadorDoArquivoDeQueries == null)
                {
                    gerenciadorDoArquivoDeQueries = new GerenciadorDoArquivoDeQueries();
                    gerenciadorDoArquivoDeQueries.CarregarQueries();
                }
                return gerenciadorDoArquivoDeQueries;
            }
        }

        private string ObterNomeDoArquivoDeQueries()
        {
            string fileConexao = ConfigurationManager.AppSettings[CHAVE_NOME_ARQUIVO_QUERIES];

            if (fileConexao == null || fileConexao == string.Empty)
            {
                throw new Exception("Não foi possível encontrar o nome do arquivo de queries no arquivo de configuração da aplicação.");
            }

            return fileConexao;
        } 

        private void CarregarQueries()
        {
            var xmlDoc = new XmlDocument();
            var nomeArquivoQueries = ObterNomeDoArquivoDeQueries();
            var caminhoArquivoQueries = String.Format("{0}\\{1}", AppDomain.CurrentDomain.BaseDirectory, nomeArquivoQueries);

            if (!File.Exists(caminhoArquivoQueries))
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);

                caminhoArquivoQueries = String.Format("{0}\\{1}", Path.GetDirectoryName(path), nomeArquivoQueries);
            }

            if (!File.Exists(caminhoArquivoQueries))
            {
                throw new Exception(string.Format("O arquivo de queries não foi carregado, pois não o encontramos em: {0} e {1}",  caminhoArquivoQueries, AppDomain.CurrentDomain.BaseDirectory + "\\" + nomeArquivoQueries));
            }

            if (File.Exists(caminhoArquivoQueries))
            {
                xmlDoc.Load(caminhoArquivoQueries);
                XmlNode rootNode = xmlDoc.SelectSingleNode(NO_RAIZ_DO_ARQUIVO_DE_QUERIES);

                foreach (XmlNode node in rootNode.ChildNodes)
                {
                    foreach (XmlNode innerNode in node.ChildNodes)
                    {
                        if (innerNode.NodeType != XmlNodeType.Comment)
                        {
                            queries.Add(innerNode.Name, innerNode.InnerText);
                            queries.Add(node.Name + "/" + innerNode.Name, innerNode.InnerText);
                        }
                    }                    
                }
            }
        }

        public string ObterQuery(string nomeQuery)
        {
            if (queries.ContainsKey(nomeQuery))
            {
                return queries[nomeQuery];
            }
            else
            {
                throw new ArgumentException("Não foi encontrada nenhuma query com o nome '" + nomeQuery + "'");
            }
        }

    }
}
