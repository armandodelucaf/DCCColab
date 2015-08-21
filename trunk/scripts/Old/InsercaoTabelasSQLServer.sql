USE [DCCCOLAB]
GO

INSERT INTO DCCCOLAB..[Perfil_Acesso] (nm_Perfil_Acesso, moderador) VALUES ('Administrador', 1);
INSERT INTO DCCCOLAB..[Perfil_Acesso] (nm_Perfil_Acesso, moderador) VALUES ('Professor', 0);
INSERT INTO DCCCOLAB..[Perfil_Acesso] (nm_Perfil_Acesso, moderador) VALUES ('Monitor', 0);
INSERT INTO DCCCOLAB..[Perfil_Acesso] (nm_Perfil_Acesso, moderador) VALUES ('Aluno', 0);

GO

INSERT INTO DCCCOLAB..[Usuario] (id_Facebook ,nm_Usuario, id_Perfil_Acesso, email, senha) 
	VALUES (NULL, 'admin', 1, 'armandodelucaf@gmail.com', PWDENCRYPT('1234'));
GO
