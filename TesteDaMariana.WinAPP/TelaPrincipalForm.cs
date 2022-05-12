using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TesteDaMariana.WinAPP.ModuloDisciplina;
using TesteDaMariana.WinAPP.ModuloMateria;
using TesteDaMariana.WinAPP.ModuloQuestao;
using TesteDaMariana.WinAPP.ModuloTeste;

namespace TesteDaMariana.WinAPP
{
    public partial class TelaPrincipalForm : Form
    {
        public TelaPrincipalForm()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnEditar_Click(object sender, EventArgs e)
        {

        }

        private void disciplinasMenuItem_Click(object sender, EventArgs e)
        {
            ListagemDisciplinaControl listagemDisciplina = new();
            panelRegistros.Controls.Add(listagemDisciplina);
        }

        private void materiaMenuItem_Click(object sender, EventArgs e)
        {
            ListagemMateriaControl listagemMateria = new();
            panelRegistros.Controls.Add(listagemMateria);
        }

        private void questaoMenuItem_Click(object sender, EventArgs e)
        {
            ListagemQuestaoControl listagemQuestao = new();
            panelRegistros.Controls.Add(listagemQuestao);
        }

        private void testeMenuItem_Click(object sender, EventArgs e)
        {
            ListagemTesteControl listagemTeste = new();
            panelRegistros.Controls.Add(listagemTeste);
        }
    }
}
