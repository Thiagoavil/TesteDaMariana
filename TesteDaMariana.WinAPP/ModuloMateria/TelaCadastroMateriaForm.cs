using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TesteMariana.Dominio.ModuloDisciplina;
using TesteMariana.Dominio.ModuloMateria;

namespace TesteDaMariana.WinAPP.ModuloMateria
{
    public partial class TelaCadastroMateriaForm : Form
    {
        private Materia materia;
        public TelaCadastroMateriaForm(List<Disciplina> disciplinas)
        {
            InitializeComponent();
            CarregarDisciplinas(disciplinas);
        }
        private void CarregarDisciplinas(List<Disciplina> disciplinas)
        {
            comboBoxDisciplina.Items.Clear();

            foreach (var item in disciplinas)
            {
                comboBoxDisciplina.Items.Add(item);
            }
        }

        public Func<Materia, ValidationResult> GravarRegistro { get; set; }

        public Materia Materia
        {
            get { return materia; }
            set
            {
                materia = value;
               

                if (materia.Serie == Serie.Primeira)
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
                comboBoxDisciplina.SelectedItem = materia.disciplina;
                textBoxNome.Text = materia.Titulo;
                
            }
        }

        private void TelaCadastroCompromissosForm_Load(object sender, EventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("");
        }

        private void TelaCadastroCompromisssosForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            TelaPrincipalForm.Instancia.AtualizarRodape("");
        }

        private void buttonGravar_Click(object sender, EventArgs e)
        {
            materia.Titulo = textBoxNome.Text;
            materia.disciplina = (Disciplina)comboBoxDisciplina.SelectedItem;

            if (radioButton1.Checked)
            {
                materia.Serie = Serie.Primeira;
            }
            else
            {
                materia.Serie = Serie.Segunda;
            }

            var resultadoValidacao = GravarRegistro(materia);

            if (resultadoValidacao.IsValid == false)
            {
                string erro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                DialogResult = DialogResult.None;
            }
        }
    }
}
