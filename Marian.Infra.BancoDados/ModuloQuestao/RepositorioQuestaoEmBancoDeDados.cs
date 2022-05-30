using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            @"INSERT INTO [TbDISCIPLINA] 
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
           @"UPDATE [TbDisciplina]	
		        SET
			        [TITULO],
                    [DISCIPLINA_NUMERO],
                    [MATERIA_NUMERO]
			       
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlExcluir =
            @"DELETE FROM [TbDisciplina]
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlSelecionarTodos =
           @"SELECT 
		            [NUMERO], 
		            [TITULO],
                    [DISCIPLINA_NUMERO],
                    [MATERIA_NUMERO] 
		          
	            FROM 
		            [TBQuestao]";

        private const string sqlSelecionarPorNumero =
           @"SELECT 
		            [NUMERO], 
		            [TITULO],
                    [DISCIPLINA_NUMERO],
                    [MATERIA_NUMERO] 
		          
	            FROM 
		            [TBQuestao]
                WHERE
                    [NUMERO] = @NUMERO";

        private const string sqlSelecionarItensTarefa =
           @"SELECT 
	            [NUMERO],
                [TITULO],
                [CONCLUIDO],
                [TAREFA_NUMERO]
              FROM 
	            [TBAlternativas]
              WHERE 
	            [TAREFA_NUMERO] = @TAREFA_NUMERO";

        #endregion

        public ValidationResult Inserir(Questao novaQuestao)
        {
            var validador = new ValidadorQuestao();

            var resultadoValidacao = validador.Validate(novaQuestao);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosQuestao(novaQuestao, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            novaQuestao.Numero = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }
        public ValidationResult Editar(Questao questao)
        {
            var validador = new ValidadorQuestao();

            var resultadoValidacao = validador.Validate(questao);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosQuestao(questao, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Questao questao)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("Numero", questao.Numero);

            conexaoComBanco.Open();
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
            int numero = Convert.ToInt32(leitorQuestao["Numero"]);
            string titulo = Convert.ToString(leitorQuestao["TITULO"]);


            var questao = new Questao
            {
                Numero = numero,
                Titulo = titulo,

            };

            return questao;
        }

        private static void ConfigurarParametrosQuestao(Questao novaQuestao, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NUMERO", novaQuestao.Numero);
            comando.Parameters.AddWithValue("TITULO", novaQuestao.Titulo);
            comando.Parameters.AddWithValue("DISCIPLINA_NUMERO", novaQuestao.disciplina);
            comando.Parameters.AddWithValue("MATERIA_NUMERO", novaQuestao.materia);

        }

    }
}
