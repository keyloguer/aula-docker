use master;

GO

CREATE TABLE Times
(
	Id uniqueidentifier NOT NULL PRIMARY KEY,
	Nome varchar(100) NOT NULL,
	Email varchar(150) NOT NULL,
	EmailEnviado bit default(0) not null,
	ConfirmacaoEmail bit default(0) not null
);

CREATE TABLE Integrantes
(
	Id uniqueidentifier NOT NULL PRIMARY KEY,
	Nome varchar(150) NOT NULL,
	RG varchar(10) NOT NULL,
	DataNascimento DATE NOT NULL,
	Telefone varchar(20) NOT NULL,
	Universidade varchar(150) NULL,
	Curso varchar(150) NULL,
	PossuiDeficiencia bit NOT NULL,
	DescricacaoDeficiencia varchar(100) NULL,
	TimeId uniqueIdentifier,
	DataRegistro DateTime,
	Linkedin varchar(150) NULL,
	Git varchar(150) NULL,
	Experiencia varchar(200) NULL,
	Categoria varchar(10) NULL,
	ComunidadeDev varchar(150) NULL,
	Email varchar(150) NULL
);

alter table [dbo].[Integrantes] add constraint fk_time FOREIGN KEY (TimeId) references Times(Id);
