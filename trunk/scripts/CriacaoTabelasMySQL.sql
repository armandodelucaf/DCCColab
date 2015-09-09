USE `dcccolab`;

DROP TABLE IF EXISTS `Perfil_Acesso`;
DROP TABLE IF EXISTS `Usuario`;
DROP TABLE IF EXISTS `Disciplina`;
DROP TABLE IF EXISTS `Tema`;
DROP TABLE IF EXISTS `Disciplina_Professor`;
DROP TABLE IF EXISTS `Prova`;
DROP TABLE IF EXISTS `Conteudo_Prova`;
DROP TABLE IF EXISTS `Conteudo_Tema`;
DROP TABLE IF EXISTS `Prova_Tema`;
DROP TABLE IF EXISTS `Avaliacao_Prova`;
DROP TABLE IF EXISTS `Avaliacao_Conteudo_Prova`;
DROP TABLE IF EXISTS `Avaliacao_Conteudo_Tema`;

CREATE TABLE `Perfil_Acesso`(
	`id_Perfil_Acesso` int AUTO_INCREMENT NOT NULL,
	`nm_Perfil_Acesso` varchar(200) NOT NULL,
	`moderador` tinyint NOT NULL,

	CONSTRAINT `PK_Perfil_Acesso` PRIMARY KEY 
	(`id_Perfil_Acesso`),

	CONSTRAINT `UK_Perfil_Acesso` UNIQUE 
	(`nm_Perfil_Acesso`) 
);

CREATE TABLE `Usuario`(
	`id_Usuario` int AUTO_INCREMENT NOT NULL,
	`id_Facebook` int NULL,
	`nm_Usuario` varchar(128) NOT NULL,
	`email` varchar(64) NOT NULL,
	`id_Perfil_Acesso` int NOT NULL,
	`senha` LONGBLOB NOT NULL,
	
	CONSTRAINT `PK_Usuario` PRIMARY KEY 
	(`id_Usuario`) ,
 
	CONSTRAINT `UQ_Usuario_id_Facebook` UNIQUE 
	(`id_Facebook`) ,
	
	CONSTRAINT FOREIGN KEY (id_Perfil_Acesso) REFERENCES `Perfil_Acesso` (id_Perfil_Acesso)
);

CREATE TABLE `Disciplina`(
	`id_Disciplina` int AUTO_INCREMENT NOT NULL,
	`nm_Disciplina` varchar(128) NOT NULL,
	
	CONSTRAINT `PK_Disciplina` PRIMARY KEY 
	(`id_Disciplina`) ,
	
	CONSTRAINT `UQ_Disciplina_nm_Disciplina` UNIQUE 
	(`nm_Disciplina`) 
);

CREATE TABLE `Tema`(
	`id_Tema` int AUTO_INCREMENT NOT NULL,
	`nm_Tema` varchar(128) NOT NULL,
	`id_Disciplina` int NOT NULL,
	
	CONSTRAINT `PK_Tema` PRIMARY KEY 
	(`id_Tema`) ,
	
	CONSTRAINT `UQ_Tema_nm_Tema` UNIQUE 
	(`nm_Tema`) ,
	
	CONSTRAINT FOREIGN KEY (id_Disciplina) REFERENCES `Disciplina` (id_Disciplina)
);

CREATE TABLE `Disciplina_Professor`(
	`id_Disciplina` int NOT NULL,
	`id_Usuario` int NOT NULL,
	`ano` int NOT NULL,
	`periodo` tinyint NOT NULL,
	
	CONSTRAINT `PK_Disciplina_Professor` UNIQUE 
	(`id_Disciplina`, `id_Usuario`) ,
	
	CONSTRAINT `UQ_Disciplina_Professor` UNIQUE 
	(`id_Disciplina`, `id_Usuario`, `ano`, `periodo`) ,
	
	CONSTRAINT FOREIGN KEY (id_Disciplina) REFERENCES `Disciplina` (id_Disciplina),
	
	CONSTRAINT FOREIGN KEY (id_Usuario) REFERENCES `Usuario` (id_Usuario)
);

CREATE TABLE `Prova`(
	`id_Prova` int AUTO_INCREMENT NOT NULL,
	`src` varchar(255) NOT NULL,
	`descricao` varchar(255) NOT NULL,
	`id_Usuario` int NOT NULL,
	`id_Disciplina` int NOT NULL,
	`id_Professor` int NOT NULL,
	
	CONSTRAINT `PK_Prova` PRIMARY KEY 
	(`id_Prova`) ,
	
	CONSTRAINT FOREIGN KEY (id_Usuario) REFERENCES `Usuario` (id_Usuario),
	
	CONSTRAINT FOREIGN KEY (id_Professor) REFERENCES `Usuario` (id_Usuario),
	
	CONSTRAINT FOREIGN KEY (id_Disciplina) REFERENCES `Disciplina` (id_Disciplina)
);

CREATE TABLE `Conteudo_Prova`(
	`id_Conteudo_Prova` int AUTO_INCREMENT NOT NULL,
	`src` varchar(255) NOT NULL,
	`descricao` varchar(255) NOT NULL,
	`id_Usuario` int NOT NULL,
	`id_Prova` int NOT NULL,
	
	CONSTRAINT `PK_Conteudo_Prova` PRIMARY KEY 
	(`id_Conteudo_Prova`) ,
	
	CONSTRAINT FOREIGN KEY (id_Usuario) REFERENCES `Usuario` (id_Usuario),
	
	CONSTRAINT FOREIGN KEY (id_Prova) REFERENCES `Prova` (id_Prova)
);

CREATE TABLE `Conteudo_Tema`(
	`id_Conteudo_Tema` int AUTO_INCREMENT NOT NULL,
	`src` varchar(255) NOT NULL,
	`descricao` varchar(255) NOT NULL,
	`id_Usuario` int NOT NULL,
	`id_Tema` int NOT NULL,
	
	CONSTRAINT `PK_Conteudo_Tema` PRIMARY KEY 
	(`id_Conteudo_Tema`) ,
	
	CONSTRAINT FOREIGN KEY (id_Usuario) REFERENCES `Usuario` (id_Usuario),
	
	CONSTRAINT FOREIGN KEY (id_Tema) REFERENCES `Tema` (id_Tema)
);

CREATE TABLE `Prova_Tema`(
	`id_Prova` int NOT NULL,
	`id_Tema` int NOT NULL,
	
	CONSTRAINT `PK_Prova_Tema` UNIQUE  
	(`id_Prova`, `id_Tema`) ,
	
	CONSTRAINT FOREIGN KEY (id_Prova) REFERENCES `Prova` (id_Prova),
	
	CONSTRAINT FOREIGN KEY (id_Tema) REFERENCES `Tema` (id_Tema)
);

CREATE TABLE `Avaliacao_Prova`(
	`id_Usuario` int NOT NULL,
	`id_Prova` int NOT NULL,
	`tipo` int NOT NULL,
	`nota` tinyint NOT NULL,
	
	CONSTRAINT `PK_Avaliacao_Prova` UNIQUE  
	(`id_Usuario`, `id_Prova`) ,
	
	CONSTRAINT FOREIGN KEY (id_Usuario) REFERENCES `Usuario` (id_Usuario),
	
	CONSTRAINT FOREIGN KEY (id_Prova) REFERENCES `Prova` (id_Prova)
);

CREATE TABLE `Avaliacao_Conteudo_Prova`(
	`id_Usuario` int NOT NULL,
	`id_Conteudo_Prova` int NOT NULL,
	`tipo` int NOT NULL,
	`nota` tinyint NOT NULL,
	
	CONSTRAINT `PK_Avaliacao_Conteudo_Prova` UNIQUE  
	(`id_Usuario`, `id_Conteudo_Prova`) ,
	
	CONSTRAINT FOREIGN KEY (id_Usuario) REFERENCES `Usuario` (id_Usuario),
	
	CONSTRAINT FOREIGN KEY (id_Conteudo_Prova) REFERENCES `Conteudo_Prova` (id_Conteudo_Prova)
);

CREATE TABLE `Avaliacao_Conteudo_Tema`(
	`id_Usuario` int NOT NULL,
	`id_Conteudo_Tema` int NOT NULL,
	`tipo` int NOT NULL,
	`nota` tinyint NOT NULL,
	
	CONSTRAINT `PK_Avaliacao_Conteudo_Tema` UNIQUE  
	(`id_Usuario`, `id_Conteudo_Tema`) ,
	
	CONSTRAINT FOREIGN KEY (id_Usuario) REFERENCES `Usuario` (id_Usuario),
	
	CONSTRAINT FOREIGN KEY (id_Conteudo_Tema) REFERENCES `Conteudo_Tema` (id_Conteudo_Tema)
);
