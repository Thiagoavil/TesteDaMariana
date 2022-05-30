CREATE TABLE [dbo].[TBMateria] (
    [Numero]            INT           IDENTITY (1, 1) NOT NULL,
    [Titulo]            VARCHAR (100) NOT NULL,
    [Serie]             INT           NOT NULL,
    [Disciplina_Numero] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Numero] ASC),
    CONSTRAINT [FK_DISCIPLINA_NUMERO] FOREIGN KEY ([Disciplina_Numero]) REFERENCES [dbo].[TbDisciplina] ([Numero])
);

