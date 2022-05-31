using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteMariana.Dominio.ModuloDisciplina;
using TesteMariana.Dominio.ModuloMateria;
using TesteMariana.Dominio.ModuloQuestao;

namespace Marian.Infra.BancoDados.ModuloQuestao
{
    public  class RepositorioQuestaoEmBancoDeDados : IRepositorioQuestao
    {
        private const string enderecoBanco =
               "Data Source=(LocalDB)\\MSSqlLocalDB;" +
               "Initial Catalog = TesteDaMarianaDb;" +
               "Integrated Security = True;" +
               "Pooling=False";

        #region Sql Queries
        private const string sqlInserir =
            @"INSERT INTO [TBQuestao] 
                (
                    [TITULO],
                    [DISCIPLINA_NUMERO],
                    [MATERIA_NUMERO]
	            )
	            VALUES
                (
                    @TITULO,
                    @DISCIPLINA_NUMERO,      
                    @MATERIA_NUMERO

                );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
           @"UPDATE [TBQuestao]	
		        SET
			        [TITULO],
                    [DISCIPLINA_NUMERO],
                    [MATERIA_NUMERO]
			       
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlExcluir =
            @"DELETE FROM [TBQuestao]
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlSelecionarTodos =
           @"SELECT 
		            Q.[NUMERO], 
		            Q.[TITULO],
                    

                    D.[NUMERO] AS DISCIPLINA_NUMERO,
                    D.[NOME] as DISCIPLINA_NOME,
                    
                    M.[NUMERO] AS MATERIA_NUMERO,
                    M.[TITULO] as MATERIA_NOME,
                    M.[SERIE] AS MATERIA_SERIE
		          
	            FROM 
		            [TBQuestao] AS Q INNER JOIN
                    [TbDisciplina] AS D
                ON
                    D.[NUMERO] = Q.[DISCIPLINA_NUMERO]

                    INNER JOIN [TBMateria] AS M
                ON 
                    M.[NUMERO] = Q.[MATERIA_NUMERO]";

        private const string sqlSelecionarPorNumero =
          @"SELECT 
		            Q.[NUMERO], 
		            Q.[TITULO],
                    

                    D.[NUMERO] AS DISCIPLINA_NUMERO,
                    D.[NOME] as DISCIPLINA_NOME,
                    
                    M.[NUMERO] AS MATERIA_NUMERO,
                    M.[TITULO] as MATERIA_NOME,
                    M.[SERIE] AS MATERIA_SERIE
		          
	            FROM 
		            [TBQuestao] AS Q INNER JOIN
                    [TbDisciplina] AS D
                ON
                    D.[NUMERO] = Q.[DISCIPLINA_NUMERO]

                    INNER JOIN [TBMateria] AS M
                ON 
                    M.[NUMERO] = Q.[MATERIA_NUMERO]
                WHERE
                    M.[NUMERO] = @NUMERO";

        private const string sqlSelecionarAlternativas =
           @"SELECT 
	            [NUMERO],
                [CORRETA],
                [RESPOSTA],
                [QUESTAO_NUMERO] 
              FROM 
	            [TBAlternativa]
              WHERE 
	            [QUESTAO_NUMERO] = @QUESTAO_NUMERO";

        private const string sqlInserirAlternativas =
            @"INSERT INTO [DBO].[TBAlternativa]
                (
		            [CORRETA],
                    [RESPOSTA],
                    [QUESTAO_NUMERO] 
	            )
                 VALUES
                (
		            @CORRETA,
		            @RESPOSTA,
		            @QUESTAO_NUMERO		   
	            ); SELECT SCOPE_IDENTITY();";

        private const string sqlExcluirAlternativa =
            @"DELETE FROM [TBAlternativa]
                 WHERE
                    [NUMERO_QUESTAO] = @NUMERO_QUESTAO";


        #endregion

        public ValidationResult Inserir(Questao novaQuestao)
        {
            var validador = new ValidadorQuestao();

            var resultadoValidacao = validador.Validate(novaQuestao);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            SqlCommand comandoInsercaoAlternativa =
                new SqlCommand(sqlInserirAlternativas, conexaoComBanco);

            ConfigurarParametrosQuestao(novaQuestao, comandoInsercao);

            conexaoComBanco.Open();
            var _id = comandoInsercao.ExecuteScalar();
            int id = Convert.ToInt32(_id);
            novaQuestao.Numero = id;
            foreach (var alternativa in novaQuestao.alternativas)
            {
                alternativa.Numero = id;
                comandoInsercaoAlternativa.Parameters.Clear();
                ConfigurarParametrosAlternativa(alternativa, comandoInsercaoAlternativa);
                comandoInsercaoAlternativa.ExecuteScalar();
            }
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        private void ConfigurarParametrosAlternativa(Alternativas alternativa, SqlCommand comandoInsercaoAlternativa)
        {
            comandoInsercaoAlternativa.Parameters.AddWithValue("QUESTAO_NUMERO", alternativa.Numero);
            comandoInsercaoAlternativa.Parameters.AddWithValue("CORRETA", alternativa.Correta);
            comandoInsercaoAlternativa.Parameters.AddWithValue("RESPOSTA", alternativa.Resposta);
        }

        public ValidationResult Editar(Questao questao)
        {
            var validador = new ValidadorQuestao();

            var resultadoValidacao = validador.Validate(questao);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            SqlCommand comandoExcluirAlternativa = new SqlCommand(sqlExcluirAlternativa, conexaoComBanco);
            SqlCommand comandoInserirAlternativa = new SqlCommand(sqlInserirAlternativas, conexaoComBanco);


            conexaoComBanco.Open();

            comandoExcluirAlternativa.Parameters.AddWithValue("NUMERO_QUESTAO", questao.Numero);
            comandoExcluirAlternativa.ExecuteNonQuery();

            ConfigurarParametrosQuestao(questao, comandoEdicao);

            foreach (var alternativa in questao.alternativas)
            {
                alternativa.Numero = questao.Numero;
                ConfigurarParametrosAlternativa(alternativa, comandoInserirAlternativa);
                comandoInserirAlternativa.ExecuteNonQuery();
                comandoInserirAlternativa.Parameters.Clear();
            }
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Questao questao)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            SqlCommand comandoExclusaoAlternativa = new SqlCommand(sqlExcluirAlternativa, conexaoComBanco);

            comandoExclusaoAlternativa.Parameters.AddWithValue("NUMERO_QUESTAO", questao.Numero);

            comandoExclusao.Parameters.AddWithValue("NUMERO", questao.Numero);

            conexaoComBanco.Open();
            comandoExclusaoAlternativa.ExecuteNonQuery();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public List<Questao> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorQuestao = comandoSelecao.ExecuteReader();

            List<Questao> questoes = new List<Questao>();

            while (leitorQuestao.Read())
            {
                Questao questao = ConverterParaQuestao(leitorQuestao);

                questoes.Add(questao);
            }

            conexaoComBanco.Close();

            return questoes;
        }
        public Questao SelecionarPorNumero(int numero)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("Numero", numero);

            conexaoComBanco.Open();
            SqlDataReader leitorQuestoes = comandoSelecao.ExecuteReader();

            Questao questao = null;
            if (leitorQuestoes.Read())
                questao = ConverterParaQuestao(leitorQuestoes);

            conexaoComBanco.Close();

            return questao;
        }

        private static Questao ConverterParaQuestao(SqlDataReader leitorQuestao)
        {

            int numeroQuestao = Convert.ToInt32(leitorQuestao["NUMERO"]);
            string tituloQuestao = Convert.ToString(leitorQuestao["TITULO"]);

            int numeroDisciplina = Convert.ToInt32(leitorQuestao["DISCIPLINA_NUMERO"]);
            string tituloDisciplina = Convert.ToString(leitorQuestao["DISCIPLINA_NOME"]);

            int numeroMateria = Convert.ToInt32(leitorQuestao["MATERIA_NUMERO"]);
            string tituloMateria = Convert.ToString(leitorQuestao["MATERIA_NOME"]);
            int serie = Convert.ToInt32(leitorQuestao["MATERIA_SERIE"]);

            var questao = new Questao
            {
                Numero = numeroQuestao,
                Titulo = tituloQuestao,

                disciplina = new Disciplina
                {
                    Numero = numeroDisciplina,
                    Titulo = tituloDisciplina
                },
                materia = new Materia
                {
                    Numero=numeroMateria,
                    Titulo=tituloMateria,
                    Serie = (Serie)serie,
                },
                
                alternativas = new List<Alternativas>()

            };

            return questao;
        }

        private static void ConfigurarParametrosQuestao(Questao novaQuestao, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NUMERO", novaQuestao.Numero);
            comando.Parameters.AddWithValue("TITULO", novaQuestao.Titulo);
            comando.Parameters.AddWithValue("DISCIPLINA_NUMERO", novaQuestao.disciplina.Numero);
            comando.Parameters.AddWithValue("MATERIA_NUMERO", novaQuestao.materia.Numero);

        }

    }
}
