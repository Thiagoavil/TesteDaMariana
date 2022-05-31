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
using TesteMariana.Dominio.ModuloTeste;

namespace Marian.Infra.BancoDados.ModuloTeste
{
    public class RepositorioTesteEmBancoDeDados : IRepositorioTeste
    {
        private const string enderecoBanco =
               "Data Source=(LocalDB)\\MSSqlLocalDB;" +
               "Initial Catalog = TesteDaMarianaDb;" +
               "Integrated Security = True;" +
               "Pooling=False";

        #region Sql Queries
        private const string sqlInserir =
            @"INSERT INTO [TBTeste] 
                (
                    TITULO,
                    DISCIPLINA_NUMERO, 
                    MATERIA_NUMERO, 
                    PROVAO,
                    DATADECRIACAO, 
                    QUANTIDADEQUESTOES
                  
	            )
	            VALUES
                (
                    @TITULO,
                    @DISCIPLINA_NUMERO, 
                    @MATERIA_NUMERO, 
                    @PROVAO,
                    @DATADECRIACAO, 
                    @QUANTIDADEQUESTOES
                                  
                );SELECT SCOPE_IDENTITY();";

        private const string sqlInserirQuestoesTabela =
            @"INSERT INTO [TBTESTE_TBQUESTAO]
                (
                    TESTE_NUMERO,
                    QUESTAO_NUMERO
                )
                    VALUES
                (
                    @TESTE_NUMERO,
                    @QUESTAO_NUMERO
                )";

        private const string sqlEditar =
           @"UPDATE [TBTESTE]	
		        SET
			        TITULO = @TITULO,
                    DISCIPLINA_NUMERO = @DISCIPLINA_NUMERO , 
                    MATERIA_NUMERO = @MATERIA_NUMERO, 
                    PROVAO = @PROVAO,
                    DATADECRIACAO = @DATADECRIACAO, 
                    QUANTIDADEQUESTOES = @QUANTIDADEQUESTOES
			       
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlExcluir =
            @"DELETE FROM [TBTESTE]
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlExcluirTabelaNN =
            @"DELETE FROM [TBTESTE_TBQUESTAO]
                WHERE
                    TESTE_NUMERO = @NUMERO";

        private const string sqlSelecionarTodos =
           @"SELECT 
		            T.TITULO,
                    T.PROVAO,
                    T.DATADECRIACAO, 
                    T.QUANTIDADEQUESTOES,

                    M.NOME AS MATERIA_NOME,
                    M.NUMERO AS MATERIA_NUMERO,
                    M.SERIE AS MATERIA_SERIE,

                    D.NOME AS DISCIPLINA_NOME,
                    D.NUMERO AS DISCIPLINA_NUMERO

	            FROM 
		            TBTESTE AS T INNER JOIN 
                    TBDISCIPLINA AS D
	            ON 
                    T.DISCIPLINA_NUMERO = D.NUMERO

	                INNER JOIN TB_MATERIA AS M
	            ON 
                    T.MATERIA_NUMERO = M.NUMERO";

        private const string sqlSelecionarPorNumero =
          @"SELECT 
		            T.TITULO,
                    T.PROVAO,
                    T.DATADECRIACAO, 
                    T.QUANTIDADEQUESTOES,

                    M.NOME AS MATERIA_NOME,
                    M.NUMERO AS MATERIA_NUMERO,
                    M.SERIE AS MATERIA_SERIE,

                    D.NOME AS DISCIPLINA_NOME,
                    D.NUMERO AS DISCIPLINA_NUMERO

	            FROM 
		            TBTESTE AS T INNER JOIN 
                    TBDISCIPLINA AS D
	            ON 
                    T.DISCIPLINA_NUMERO = D.NUMERO

	                INNER JOIN TB_MATERIA AS M
	            ON 
                    T.MATERIA_NUMERO = M.NUMERO

                WHERE
                    [NUMERO] = @NUMERO";

        #endregion

        public ValidationResult Inserir(Teste novoTeste)
        {
            var validador = new ValidadorTeste();

            var resultado = validador.Validate(novoTeste);

            if (!resultado.IsValid)
                return resultado;

            SqlConnection conexaoComBanco = new(enderecoBanco);

            SqlCommand comandoInsercao = new(sqlInserir, conexaoComBanco); 

            ConfigurarParametrosTeste(novoTeste, comandoInsercao);

            conexaoComBanco.Open();

            var id = comandoInsercao.ExecuteScalar(); 

            novoTeste.Numero = Convert.ToInt32(id);

            SqlCommand comandoInserirQuestoes = new(sqlInserirQuestoesTabela, conexaoComBanco);

            foreach (var questao in novoTeste.questoes)
            {
                comandoInserirQuestoes.Parameters.Clear();
                ConfigurarParametroQuestao_Teste(questao, novoTeste, comandoInserirQuestoes);
                comandoInserirQuestoes.ExecuteNonQuery();
            }

            conexaoComBanco.Close();

            return resultado;
        }
    
        public ValidationResult Editar(Teste teste)
        {
            var validador = new ValidadorTeste();

            var resultadoValidacao = validador.Validate(teste);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosTeste(teste, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Teste teste)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusaoTeste = new SqlCommand(sqlExcluir, conexaoComBanco);

            SqlCommand comandoExclusaoNN = new(sqlExcluirTabelaNN, conexaoComBanco);

            comandoExclusaoTeste.Parameters.AddWithValue("NUMERO", teste.Numero);

            comandoExclusaoNN.Parameters.AddWithValue("NUMERO", teste.Numero);

            conexaoComBanco.Open();

            int numeroRegistrosExcluidos = comandoExclusaoTeste.ExecuteNonQuery();

            var resultado = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultado.Errors.Add(new ValidationFailure("", "Não deu pra deletar"));
            else
                comandoExclusaoNN.ExecuteNonQuery();

            conexaoComBanco.Close();

            return resultado;
        }

        public List<Teste> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorTeste = comandoSelecao.ExecuteReader();

            List<Teste> teste = new List<Teste>();

            while (leitorTeste.Read())
            {
                Teste testes = ConverterParaTeste(leitorTeste);

                teste.Add(testes);
            }

            conexaoComBanco.Close();

            return teste;
        }
        public Teste SelecionarPorNumero(int numero)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("Numero", numero);

            conexaoComBanco.Open();
            SqlDataReader leitorTeste = comandoSelecao.ExecuteReader();

            Teste teste = null;
            if (leitorTeste.Read())
                teste = ConverterParaTeste(leitorTeste);

            conexaoComBanco.Close();

            return teste;
        }

        private static Teste ConverterParaTeste(SqlDataReader leitorteste)
        {
            int numero = Convert.ToInt32(leitorteste["NUMERO"]);
            string titulo = Convert.ToString(leitorteste["TITULO"]);
            int quantidadeQuestoes = Convert.ToInt32(leitorteste["QUANTIDADEQUESTOES"]);
            DateTime dataCriacao = Convert.ToDateTime(leitorteste["DATADECRIACAO"]);
            bool provao = Convert.ToBoolean(leitorteste["PROVAO"]);

           
            int numeroDisciplina = Convert.ToInt32(leitorteste["DISCIPLINA_NUMERO"]);
            string nomeDisciplina = Convert.ToString(leitorteste["DISCIPLINA_NOME"]);

            
            int numeroMateria = Convert.ToInt32(leitorteste["MATERIA_NUMERO"]);
            string nomeMateria = Convert.ToString(leitorteste["MATERIA_NOME"]);
            int serie = Convert.ToInt32(leitorteste["MATERIA_SERIE"]);

            var teste = new Teste
            {
                Numero = numero,
                Titulo = titulo,
                QuantidadeDeQuestoes= quantidadeQuestoes,
                DataDeCriacao= dataCriacao,
                Provao= provao,

                Disciplina = new Disciplina
                {
                    Numero = numeroDisciplina,
                    Titulo = nomeDisciplina
                },
                Materia = new Materia
                {
                    Numero = numeroMateria,
                    Titulo = nomeMateria,
                    Serie = (Serie)serie,
                },

            };
            return teste;
        }

        private static void ConfigurarParametrosTeste(Teste novoTeste, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("Numero", novoTeste.Numero);
            comando.Parameters.AddWithValue("TITULO", novoTeste.Titulo);

        }
        private void ConfigurarParametroQuestao_Teste(Questao questao, Teste novoTeste, SqlCommand comandoInserirNParaN)
        {
            comandoInserirNParaN.Parameters.AddWithValue("TESTE_ID", novoTeste.Numero);
            comandoInserirNParaN.Parameters.AddWithValue("QUESTAO_ID", questao.Numero);
        }

    }
}
