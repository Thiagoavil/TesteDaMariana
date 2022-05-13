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
using TesteMariana.Dominio.ModuloDisciplina;

namespace TesteDaMariana.WinAPP.ModuloDisciplina
{
    public partial class ListagemDisciplinaControl : UserControl
    {
        public ListagemDisciplinaControl()
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

                new DataGridViewTextBoxColumn { DataPropertyName = "Titulo", HeaderText = "Titulo"},
            };

            return colunas;
        }

        public int ObtemNumeroCompromissoSelecionado()
        {
            return grid.SelecionarNumero<int>();
        }

        public void AtualizarDisciplina(List<Disciplina> disciplinas)
        {
            grid.Rows.Clear();

            foreach (var disciplina in disciplinas)
            {
                grid.Rows.Add(disciplina.Numero,disciplina.Titulo);
            }
        }

    }
}
