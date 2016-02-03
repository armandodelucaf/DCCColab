USE `dcccolab`;

DROP TABLE IF EXISTS `Usuario`;
DROP TABLE IF EXISTS `Perfil_Acesso`;
DROP TABLE IF EXISTS `Disciplina`;
DROP TABLE IF EXISTS `Tema`;
DROP TABLE IF EXISTS `Disciplina_Professor`;
DROP TABLE IF EXISTS `Tipo_Prova`;
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
	`id_Facebook` varchar(30) NULL,
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
	`nu_Periodo_Recomendado` tinyint NOT NULL,
	
	CONSTRAINT `PK_Disciplina` PRIMARY KEY 
	(`id_Disciplina`),
	
	CONSTRAINT `UQ_Disciplina_nm_Disciplina` UNIQUE 
	(`nm_Disciplina`) 
);

CREATE TABLE `Tema`(
	`id_Tema` int AUTO_INCREMENT NOT NULL,
	`nm_Tema` varchar(128) NOT NULL,
	`ds_Tema` varchar(255) NULL,
	`id_Disciplina` int NOT NULL,
	
	CONSTRAINT `PK_Tema` PRIMARY KEY 
	(`id_Tema`) ,
	
	CONSTRAINT `UQ_Tema` UNIQUE 
	(`nm_Tema`, `id_Disciplina`),
	
	CONSTRAINT FOREIGN KEY (id_Disciplina) REFERENCES `Disciplina` (id_Disciplina)
);

CREATE TABLE `Turma`(
	`id_Turma` int AUTO_INCREMENT NOT NULL,
	`id_Disciplina` int NOT NULL,
	`nu_Ano` int NOT NULL,
	`nu_Semestre` tinyint NOT NULL,
	`cd_Turma` varchar(10) NULL,
	
	CONSTRAINT `PK_Turma` UNIQUE 
	(`id_Turma`) ,
	
	CONSTRAINT `UQ_Turma` UNIQUE 
	(`id_Disciplina`, `nu_Ano`, `nu_Semestre`, `cd_Turma`) ,
	
	CONSTRAINT FOREIGN KEY (id_Disciplina) REFERENCES `Disciplina` (id_Disciplina)
);

CREATE TABLE `Turma_Professor`(
	`id_Turma` int NOT NULL,
	`id_Professor` int NOT NULL,
	
	CONSTRAINT `Turma_Professor` UNIQUE 
	(`id_Turma`, `id_Professor`) ,
	
	CONSTRAINT FOREIGN KEY (id_Turma) REFERENCES `Turma` (id_Turma),
	
	CONSTRAINT FOREIGN KEY (id_Professor) REFERENCES `Usuario` (id_Usuario)
);

CREATE TABLE `Tipo_Prova`(
	`id_Tipo_Prova` int AUTO_INCREMENT NOT NULL,
	`nm_Tipo_Prova` varchar(64) NOT NULL,
	
	CONSTRAINT `PK_Tipo_Prova` PRIMARY KEY 
	(`id_Tipo_Prova`) ,
	
	CONSTRAINT `UQ_Tipo_Prova` UNIQUE 
	(`nm_Tipo_Prova`) 
);

CREATE TABLE `Prova`(
	`id_Prova` int AUTO_INCREMENT NOT NULL,
	`src` longblob NOT NULL,
	`titulo` varchar(100) NULL,
	`descricao` varchar(255) NULL,
	`id_Usuario` int NOT NULL,
	`id_Turma` int NOT NULL,
	`id_Tipo_Prova` int NOT NULL,
	`aval_Professor` tinyint NOT NULL,
	`aval_Monitor` tinyint NOT NULL,
	
	CONSTRAINT `PK_Prova` PRIMARY KEY 
	(`id_Prova`) ,
	
	CONSTRAINT FOREIGN KEY (id_Usuario) REFERENCES `Usuario` (id_Usuario),
	
	CONSTRAINT FOREIGN KEY (id_Turma) REFERENCES `Turma` (id_Turma),
	
	CONSTRAINT FOREIGN KEY (id_Tipo_Prova) REFERENCES `Tipo_Prova` (id_Tipo_Prova)
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
	`nota` tinyint NOT NULL,
	
	CONSTRAINT `PK_Avaliacao_Prova` UNIQUE  
	(`id_Usuario`, `id_Prova`) ,
	
	CONSTRAINT FOREIGN KEY (id_Usuario) REFERENCES `Usuario` (id_Usuario),
	
	CONSTRAINT FOREIGN KEY (id_Prova) REFERENCES `Prova` (id_Prova)
);

CREATE TABLE `Tipo_Link`(
	`id_Tipo_Link` int AUTO_INCREMENT NOT NULL,
	`nm_Tipo_Link` varchar(64) NOT NULL,
	
	CONSTRAINT `PK_Tipo_Link` PRIMARY KEY 
	(`id_Tipo_Link`) ,
	
	CONSTRAINT `UQ_Tipo_Link` UNIQUE 
	(`nm_Tipo_Link`) 
);

CREATE TABLE `Link`(
	`id_Link` int AUTO_INCREMENT NOT NULL,
	`url` varchar(255) NULL,
	`src` longblob NULL,
	`flag_upload` tinyint NOT NULL,
	`titulo` varchar(100) NULL,
	`descricao` varchar(255) NULL,
	`id_Usuario` int NOT NULL,
	`id_Disciplina` int NOT NULL,
	`id_Tipo_Link` int NOT NULL,
	`aval_Professor` tinyint NOT NULL,
	`aval_Monitor` tinyint NOT NULL,
	
	CONSTRAINT `PK_Link` PRIMARY KEY 
	(`id_Link`) ,
	
	CONSTRAINT FOREIGN KEY (id_Usuario) REFERENCES `Usuario` (id_Usuario),
	
	CONSTRAINT FOREIGN KEY (id_Tipo_Link) REFERENCES `Tipo_Link` (id_Tipo_Link),
	
	CONSTRAINT FOREIGN KEY (id_Disciplina) REFERENCES `Disciplina` (id_Disciplina)
);

CREATE TABLE `Link_Tema`(
	`id_Link` int NOT NULL,
	`id_Tema` int NOT NULL,
	
	CONSTRAINT `PK_Link_Tema` UNIQUE  
	(`id_Link`, `id_Tema`) ,
	
	CONSTRAINT FOREIGN KEY (id_Link) REFERENCES `Link` (id_Link),
	
	CONSTRAINT FOREIGN KEY (id_Tema) REFERENCES `Tema` (id_Tema)
);

CREATE TABLE `Avaliacao_Link`(
	`id_Usuario` int NOT NULL,
	`id_Link` int NOT NULL,
	`nota` tinyint NOT NULL,
	
	CONSTRAINT `PK_Avaliacao_Link` UNIQUE  
	(`id_Usuario`, `id_Link`) ,
	
	CONSTRAINT FOREIGN KEY (id_Usuario) REFERENCES `Usuario` (id_Usuario),
	
	CONSTRAINT FOREIGN KEY (id_Link) REFERENCES `Link` (id_Link)
);

CREATE TABLE `Notificacao`(
	`id_Notificacao` int NOT NULL,
	`id_Referencia` int NOT NULL,
	`tipo_Referencia` int NOT NULL,
	`txt_Notificacao` varchar(255) NOT NULL,
	`id_Usuario` int NOT NULL,
	`dt_Notificacao` DATETIME NOT NULL,
	
	CONSTRAINT `PK_Notificacao` PRIMARY KEY  
	(`id_Notificacao`) ,
	
	CONSTRAINT `UK_Notificacao` UNIQUE  
	(`id_Referencia`, `tipo_Referencia`, `id_Usuario`) ,
	
	CONSTRAINT FOREIGN KEY (id_Usuario) REFERENCES `Usuario` (id_Usuario)
);