using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteMariana.Dominio.ModuloDisciplina;
using TesteMariana.Dominio.ModuloMateria;

namespace Marian.Infra.BancoDados.ModuloMateria
{
    public class RepositorioMateriaEmBancoDeDados : IRepositorioMateria
    {
        private const string enderecoBanco =
               "Data Source=(LocalDB)\\MSSqlLocalDB;" +
               "Initial Catalog = TesteDaMarianaDb;" +
               "Integrated Security = True;" +
               "Pooling=False";

        #region Sql Queries
        private const string sqlInserir =
            @"INSERT INTO [TBMateria] 
                (
                    [TITULO],
                    [SERIE],
                    [DISCIPLINA_NUMERO]
                  
	            )
	            VALUES
                (
                    @TITULO,
                    @SERIE,
                    @DISCIPLINA_NUMERO
                );SELECT SCOPE_IDENTITY();";

        private const string sqlEditar =
           @"UPDATE [TBMateria]	
		        SET
			        [TITULO] = @TITULO,
                    [SERIE] = @SERIE,
                    [DISCIPLINA_NUMERO] = @DISCIPLINA_NUMERO
			       
		        WHERE
			        [NUMERO] = @Numero";

        private const string sqlExcluir =
            @"DELETE FROM [TBMateria]
		        WHERE
			        [NUMERO] = @NUMERO";

        private const string sqlSelecionarTodos =
           @"SELECT 
		            MA.[NUMERO], 
		            MA.[TITULO],
                    MA.[SERIE],
                    MA.[DISCIPLINA_NUMERO],
                    DI.[NOME]    
                    
		          
	            FROM 
		            [TBMateria] AS MA LEFT JOIN
                    [TbDisciplina] AS DI
                
                ON 
                    DI.NUMERO = MA.DISCIPLINA_NUMERO";

        private const string sqlSelecionarPorNumero =
            @"SELECT 
		            MA.[NUMERO], 
		            MA.[TITULO],
                    MA.[SERIE],
                    MA.[DISCIPLINA_NUMERO],
                    DI.[NOME]    
                    
		          
	            FROM 
		            [TBMateria] AS MA LEFT JOIN
                    [TbDisciplina] AS DI
                
                ON 
                    DI.NUMERO = MA.DISCIPLINA_NUMERO
                WHERE
                    MA.[NUMERO] = @NUMERO";

        #endregion

        public ValidationResult Inserir(Materia novaMateria)
        {
            var validador = new ValidadorMateria();

            var resultadoValidacao = validador.Validate(novaMateria);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoInsercao = new SqlCommand(sqlInserir, conexaoComBanco);

            ConfigurarParametrosMateria(novaMateria, comandoInsercao);

            conexaoComBanco.Open();
            var id = comandoInsercao.ExecuteScalar();
            novaMateria.Numero = Convert.ToInt32(id);

            conexaoComBanco.Close();

            return resultadoValidacao;
        }
        public ValidationResult Editar(Materia disciplina)
        {
            var validador = new ValidadorMateria();

            var resultadoValidacao = validador.Validate(disciplina);

            if (resultadoValidacao.IsValid == false)
                return resultadoValidacao;

            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoEdicao = new SqlCommand(sqlEditar, conexaoComBanco);

            ConfigurarParametrosMateria(disciplina, comandoEdicao);

            conexaoComBanco.Open();
            comandoEdicao.ExecuteNonQuery();
            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public ValidationResult Excluir(Materia materia)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoExclusao = new SqlCommand(sqlExcluir, conexaoComBanco);

            comandoExclusao.Parameters.AddWithValue("Numero", materia.Numero);

            conexaoComBanco.Open();
            int numeroRegistrosExcluidos = comandoExclusao.ExecuteNonQuery();

            var resultadoValidacao = new ValidationResult();

            if (numeroRegistrosExcluidos == 0)
                resultadoValidacao.Errors.Add(new ValidationFailure("", "Não foi possível remover o registro"));

            conexaoComBanco.Close();

            return resultadoValidacao;
        }

        public List<Materia> SelecionarTodos()
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarTodos, conexaoComBanco);

            conexaoComBanco.Open();
            SqlDataReader leitorMateria = comandoSelecao.ExecuteReader();

            List<Materia> materias = new List<Materia>();

            while (leitorMateria.Read())
            {
                Materia materia = ConverterParaMateria(leitorMateria);

                materias.Add(materia);
            }

            conexaoComBanco.Close();

            return materias;
        }
        public Materia SelecionarPorNumero(int numero)
        {
            SqlConnection conexaoComBanco = new SqlConnection(enderecoBanco);

            SqlCommand comandoSelecao = new SqlCommand(sqlSelecionarPorNumero, conexaoComBanco);

            comandoSelecao.Parameters.AddWithValue("Numero", numero);

            conexaoComBanco.Open();
            SqlDataReader leitorMateria = comandoSelecao.ExecuteReader();

            Materia materias = null;
            if (leitorMateria.Read())
                materias = ConverterParaMateria(leitorMateria);

            conexaoComBanco.Close();

            return materias;
        }

        private static Materia ConverterParaMateria(SqlDataReader leitorMateria)
        {
            int numero = Convert.ToInt32(leitorMateria["NUMERO"]);
            string titulo = Convert.ToString(leitorMateria["TITULO"]);
            int serie = Convert.ToInt32(leitorMateria["SERIE"]);

            var materia = new Materia
            {
                Numero = numero,
                Titulo = titulo,
                Serie = (Serie)serie
            };

            var numeroDisciplina = Convert.ToInt32(leitorMateria["DISCIPLINA_NUMERO"]);
            var tituloDisciplina = Convert.ToString(leitorMateria["NOME"]);

            materia.disciplina = new Disciplina
            {
                Numero = numeroDisciplina,
                Titulo = tituloDisciplina,
            };

            return materia;
        }

        private static void ConfigurarParametrosMateria(Materia novaMateria, SqlCommand comando)
        {
            comando.Parameters.AddWithValue("NUMERO", novaMateria.Numero);
            comando.Parameters.AddWithValue("TITULO", novaMateria.Titulo);
            comando.Parameters.AddWithValue("SERIE", novaMateria.Serie);
            comando.Parameters.AddWithValue("DISCIPLINA_NUMERO", novaMateria.disciplina.Numero);
        }

    }
}
