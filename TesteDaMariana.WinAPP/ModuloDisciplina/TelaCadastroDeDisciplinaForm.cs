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

namespace TesteDaMariana.WinAPP
{
    public partial class TelaCadastroDeDisciplinaForm : Form
    {
        private Disciplina disciplina;
        private IRepositorioDisciplina repositorioDisciplinas;
        public TelaCadastroDeDisciplinaForm(IRepositorioDisciplina repositorioDisciplinas)
        {
            InitializeComponent();
            this.repositorioDisciplinas=repositorioDisciplinas;
        }


        public Func<Disciplina, ValidationResult> GravarRegistro { get; set; }

        public Disciplina Disciplina
        {
            get { return disciplina; }
            set
            {
                disciplina = value;

                textBoxNome.Text = disciplina.Titulo;
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
            disciplina.Titulo = textBoxNome.Text;

            var resultadoValidacao = GravarRegistro(disciplina);
            List<Disciplina> disciplinas = repositorioDisciplinas.SelecionarTodos();
            
            foreach(var item in disciplinas )
            {
                if (disciplina.Titulo==item.Titulo && disciplina.Numero!=item.Numero)
                {
                    MessageBox.Show("Nome já existe",
                    "Disciplina", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            if (resultadoValidacao.IsValid == false)
            {
                string erro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                DialogResult = DialogResult.None;
            }
        }
    }
}
