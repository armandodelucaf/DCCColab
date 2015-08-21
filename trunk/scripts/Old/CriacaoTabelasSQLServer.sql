USE [DCCCOLAB]
GO

CREATE TABLE [dbo].[Perfil_Acesso](
	[id_Perfil_Acesso] [int] IDENTITY(1,1) NOT NULL,
	[nm_Perfil_Acesso] [varchar](200) NOT NULL,
	[moderador] [bit] NOT NULL

	CONSTRAINT [PK_Perfil_Acesso] PRIMARY KEY CLUSTERED 
	([id_Perfil_Acesso])WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],

	CONSTRAINT [UK_Perfil_Acesso] UNIQUE NONCLUSTERED 
	([nm_Perfil_Acesso]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[Usuario](
	[id_Usuario] [int] IDENTITY(1,1) NOT NULL,
	[id_Facebook] [int] NULL,
	[nm_Usuario] [varchar](128) NOT NULL,
	[email] [varchar](64) NOT NULL,
	[id_Perfil_Acesso] [int] NOT NULL,
	[senha] VARBINARY (MAX) NOT NULL,
	
	CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
	([id_Usuario]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 70) ON [PRIMARY],
 
	CONSTRAINT [UQ_Usuario_id_Facebook] UNIQUE NONCLUSTERED 
	([id_Facebook]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 70) ON [PRIMARY],
	
	CONSTRAINT [FK_Usuario_Perfil_Acesso] FOREIGN KEY (id_Perfil_Acesso) REFERENCES [dbo].[Perfil_Acesso] (id_Perfil_Acesso)
) ON [PRIMARY]

CREATE TABLE [dbo].[Disciplina](
	[id_Disciplina] [int] IDENTITY(1,1) NOT NULL,
	[nm_Disciplina] [varchar](128) NOT NULL,
	
	CONSTRAINT [PK_Disciplina] PRIMARY KEY CLUSTERED 
	([id_Disciplina]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 70) ON [PRIMARY],
	
	CONSTRAINT [UQ_Disciplina_nm_Disciplina] UNIQUE NONCLUSTERED 
	([nm_Disciplina]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 70) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[Tema](
	[id_Tema] [int] IDENTITY(1,1) NOT NULL,
	[nm_Tema] [varchar](128) NOT NULL,
	[id_Disciplina] [int] NOT NULL,
	
	CONSTRAINT [PK_Tema] PRIMARY KEY CLUSTERED 
	([id_Tema]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 70) ON [PRIMARY],
	
	CONSTRAINT [UQ_Tema_nm_Tema] UNIQUE NONCLUSTERED 
	([nm_Tema]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 70) ON [PRIMARY],
	
	CONSTRAINT [FK_Tema_Disciplina] FOREIGN KEY (id_Disciplina) REFERENCES [dbo].[Disciplina] (id_Disciplina)
) ON [PRIMARY]

CREATE TABLE [dbo].[Disciplina_Professor](
	[id_Disciplina] [int] NOT NULL,
	[id_Usuario] [int] NOT NULL,
	[ano] [int] NOT NULL,
	[periodo] [tinyint] NOT NULL,
	
	CONSTRAINT [PK_Disciplina_Professor] UNIQUE NONCLUSTERED 
	([id_Disciplina], [id_Usuario]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 70) ON [PRIMARY],
	
	CONSTRAINT [UQ_Disciplina_Professor] UNIQUE NONCLUSTERED 
	([id_Disciplina], [id_Usuario], [ano], [periodo]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 70) ON [PRIMARY],
	
	CONSTRAINT [FK_Disciplina_Professor_Disciplina] FOREIGN KEY (id_Disciplina) REFERENCES [dbo].[Disciplina] (id_Disciplina),
	
	CONSTRAINT [FK_Disciplina_Professor_Usuario] FOREIGN KEY (id_Usuario) REFERENCES [dbo].[Usuario] (id_Usuario)
) ON [PRIMARY]

CREATE TABLE [dbo].[Prova](
	[id_Prova] [int] IDENTITY(1,1) NOT NULL,
	[src] [varchar](255) NOT NULL,
	[descricao] [varchar](255) NOT NULL,
	[id_Usuario] [int] NOT NULL,
	[id_Disciplina] [int] NOT NULL,
	[id_Professor] [int] NOT NULL,
	
	CONSTRAINT [PK_Prova] PRIMARY KEY CLUSTERED 
	([id_Prova]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 70) ON [PRIMARY],
	
	CONSTRAINT [FK_Prova_Usuario_1] FOREIGN KEY (id_Usuario) REFERENCES [dbo].[Usuario] (id_Usuario),
	
	CONSTRAINT [FK_Prova_Usuario_2] FOREIGN KEY (id_Professor) REFERENCES [dbo].[Usuario] (id_Usuario),
	
	CONSTRAINT [FK_Prova_Disciplina] FOREIGN KEY (id_Disciplina) REFERENCES [dbo].[Disciplina] (id_Disciplina)
) ON [PRIMARY]

CREATE TABLE [dbo].[Conteudo_Prova](
	[id_Conteudo_Prova] [int] IDENTITY(1,1) NOT NULL,
	[src] [varchar](255) NOT NULL,
	[descricao] [varchar](255) NOT NULL,
	[id_Usuario] [int] NOT NULL,
	[id_Prova] [int] NOT NULL,
	
	CONSTRAINT [PK_Conteudo_Prova] PRIMARY KEY CLUSTERED 
	([id_Conteudo_Prova]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 70) ON [PRIMARY],
	
	CONSTRAINT [FK_Conteudo_Prova_Usuario] FOREIGN KEY (id_Usuario) REFERENCES [dbo].[Usuario] (id_Usuario),
	
	CONSTRAINT [FK_Conteudo_Prova_Prova] FOREIGN KEY (id_Prova) REFERENCES [dbo].[Prova] (id_Prova)
) ON [PRIMARY]

CREATE TABLE [dbo].[Conteudo_Tema](
	[id_Conteudo_Tema] [int] IDENTITY(1,1) NOT NULL,
	[src] [varchar](255) NOT NULL,
	[descricao] [varchar](255) NOT NULL,
	[id_Usuario] [int] NOT NULL,
	[id_Tema] [int] NOT NULL,
	
	CONSTRAINT [PK_Conteudo_Tema] PRIMARY KEY CLUSTERED 
	([id_Conteudo_Tema]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 70) ON [PRIMARY],
	
	CONSTRAINT [FK_Conteudo_Tema_Usuario] FOREIGN KEY (id_Usuario) REFERENCES [dbo].[Usuario] (id_Usuario),
	
	CONSTRAINT [FK_Conteudo_Tema_Tema] FOREIGN KEY (id_Tema) REFERENCES [dbo].[Tema] (id_Tema)
) ON [PRIMARY]

CREATE TABLE [dbo].[Prova_Tema](
	[id_Prova] [int] NOT NULL,
	[id_Tema] [int] NOT NULL,
	
	CONSTRAINT [PK_Prova_Tema] UNIQUE NONCLUSTERED 
	([id_Prova], [id_Tema]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 70) ON [PRIMARY],
	
	CONSTRAINT [FK_Prova_Tema_Prova] FOREIGN KEY (id_Prova) REFERENCES [dbo].[Prova] (id_Prova),
	
	CONSTRAINT [FK_Prova_Tema_Tema] FOREIGN KEY (id_Tema) REFERENCES [dbo].[Tema] (id_Tema)
) ON [PRIMARY]

CREATE TABLE [dbo].[Avaliacao_Prova](
	[id_Usuario] [int] NOT NULL,
	[id_Prova] [int] NOT NULL,
	[tipo] [int] NOT NULL,
	[nota] [tinyint] NOT NULL,
	
	CONSTRAINT [PK_Avaliacao_Prova] UNIQUE NONCLUSTERED 
	([id_Usuario], [id_Prova]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 70) ON [PRIMARY],
	
	CONSTRAINT [FK_Avaliacao_Prova_Usuario] FOREIGN KEY (id_Usuario) REFERENCES [dbo].[Usuario] (id_Usuario),
	
	CONSTRAINT [FK_Avaliacao_Prova_Prova] FOREIGN KEY (id_Prova) REFERENCES [dbo].[Prova] (id_Prova)
) ON [PRIMARY]

CREATE TABLE [dbo].[Avaliacao_Conteudo_Prova](
	[id_Usuario] [int] NOT NULL,
	[id_Conteudo_Prova] [int] NOT NULL,
	[tipo] [int] NOT NULL,
	[nota] [tinyint] NOT NULL,
	
	CONSTRAINT [PK_Avaliacao_Conteudo_Prova] UNIQUE NONCLUSTERED 
	([id_Usuario], [id_Conteudo_Prova]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 70) ON [PRIMARY],
	
	CONSTRAINT [FK_Avaliacao_Conteudo_Prova_Usuario] FOREIGN KEY (id_Usuario) REFERENCES [dbo].[Usuario] (id_Usuario),
	
	CONSTRAINT [FK_Avaliacao_Conteudo_Prova_Conteudo_Prova] FOREIGN KEY (id_Conteudo_Prova) REFERENCES [dbo].[Conteudo_Prova] (id_Conteudo_Prova)
) ON [PRIMARY]

CREATE TABLE [dbo].[Avaliacao_Conteudo_Tema](
	[id_Usuario] [int] NOT NULL,
	[id_Conteudo_Tema] [int] NOT NULL,
	[tipo] [int] NOT NULL,
	[nota] [tinyint] NOT NULL,
	
	CONSTRAINT [PK_Avaliacao_Conteudo_Tema] UNIQUE NONCLUSTERED 
	([id_Usuario], [id_Conteudo_Tema]) WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 70) ON [PRIMARY],
	
	CONSTRAINT [FK_Avaliacao_Conteudo_Tema_Usuario] FOREIGN KEY (id_Usuario) REFERENCES [dbo].[Usuario] (id_Usuario),
	
	CONSTRAINT [FK_Avaliacao_Conteudo_Tema_Conteudo_Tema] FOREIGN KEY (id_Conteudo_Tema) REFERENCES [dbo].[Conteudo_Tema] (id_Conteudo_Tema)
) ON [PRIMARY]
