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

namespace TesteDaMariana.WinAPP.ModuloDisciplina
{
    public partial class ListagemDisciplinaControl : UserControl
    {
        public ListagemDisciplinaControl()
        {
            InitializeComponent();
        }

        public int ObtemNumeroCompromissoSelecionado()
        {
            return Grid.SelecionarNumero<int>();
        }

        public void AtualizarDisciplina(List<Compromisso> compromissos)
        {
            grid.Rows.Clear();

            foreach (var compromisso in compromissos)
            {
                grid.Rows.Add(compromisso.Numero, compromisso.Assunto,
                    compromisso.Data.ToShortDateString(), compromisso.HoraInicio,
                    compromisso.Contato?.Nome);
            }
        }

    }
}
