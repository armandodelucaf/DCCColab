USE `dcccolab`;

INSERT INTO `Perfil_Acesso` (nm_Perfil_Acesso, moderador) VALUES ('Administrador', 1);
INSERT INTO `Perfil_Acesso` (nm_Perfil_Acesso, moderador) VALUES ('Professor', 0);
INSERT INTO `Perfil_Acesso` (nm_Perfil_Acesso, moderador) VALUES ('Monitor', 0);
INSERT INTO `Perfil_Acesso` (nm_Perfil_Acesso, moderador) VALUES ('Aluno', 0);

INSERT INTO `Usuario` (id_Facebook ,nm_Usuario, id_Perfil_Acesso, email, senha) 
	VALUES (NULL, 'admin', 1, 'armandodelucaf@gmail.com', DES_ENCRYPT('1234'));