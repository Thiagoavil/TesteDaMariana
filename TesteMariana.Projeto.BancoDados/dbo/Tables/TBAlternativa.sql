CREATE TABLE [dbo].[TBAlternativa] (
    [NUMERO]         INT           NOT NULL,
    [CORRETA]        BIT           NOT NULL,
    [RESPOSTA]       VARCHAR (100) NOT NULL,
    [QUESTAO_NUMERO] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([NUMERO] ASC)
);

