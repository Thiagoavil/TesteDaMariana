CREATE TABLE [dbo].[TBQuestao] (
    [NUMERO]            INT           NOT NULL,
    [TITULO]            VARCHAR (100) NOT NULL,
    [DISCIPLINA_NUMERO] INT           NOT NULL,
    [MATERIA_NUMERO]    INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([NUMERO] ASC)
);

