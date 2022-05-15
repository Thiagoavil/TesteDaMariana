using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TesteDaMariana.WinAPP.Compartilhado;
using TesteMariana.Dominio.ModuloTeste;

namespace TesteDaMariana.WinAPP.ModuloTeste
{
    public partial class ListagemTesteControl : UserControl
    {
        public ListagemTesteControl()
        {
            InitializeComponent();
            grid.ConfigurarGridZebrado();
            grid.ConfigurarGridSomenteLeitura();
            grid.Columns.AddRange(ObterColunas());
        }

        public DataGridViewColumn[] ObterColunas()
        {
            var colunas = new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { DataPropertyName = "Numero", HeaderText = "Numero"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Enunciado", HeaderText = "Enunciado"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Materia", HeaderText = "Materia"},

                new DataGridViewTextBoxColumn { DataPropertyName = "Disciplina", HeaderText = "Disciplina"},

                new DataGridViewTextBoxColumn { DataPropertyName = "NumeroDeQuestoes", HeaderText = "Numero de questões"},

                new DataGridViewTextBoxColumn { DataPropertyName = "DataDeCriacao", HeaderText = "Data de Criação"},

            };

            return colunas;
        }

        public int ObtemNumeroTesteSelecionado()
        {
            return grid.SelecionarNumero<int>();
        }

        public void AtualizarRegistros(List<Teste> testes)
        {
            grid.Rows.Clear();

            foreach (var teste in testes)
            {
                grid.Rows.Add(teste.Numero, teste.Titulo, teste.Materia.Titulo, teste.Disciplina.Titulo,teste.QuantidadeDeQuestoes, teste.DataDeCriacao.ToShortDateString() );
            }
        }


    }
}
