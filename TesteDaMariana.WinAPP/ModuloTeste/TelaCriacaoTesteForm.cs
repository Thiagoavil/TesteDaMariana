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
using TesteMariana.Dominio.ModuloQuestao;
using TesteMariana.Dominio.ModuloTeste;

namespace TesteDaMariana.WinAPP.ModuloTeste
{
    public partial class TelaCriacaoTesteForm : Form
    {
        private Teste teste;
        public TelaCriacaoTesteForm(List<Disciplina>disciplinas)
        {
            InitializeComponent();
            foreach (Questao item in teste.questoes)
            {
                listBoxQuestoes.Items.Add(item);
            }
            CarregarDisciplinas(disciplinas);
            var disciplinaselecionada = (Disciplina)comboBoxDisciplina.SelectedItem;
            CarregarMaterias(disciplinaselecionada);
            
        }

        public Func<Teste, ValidationResult> GravarRegistro { get; set; }

        private void CarregarMaterias(Disciplina disciplinaSelecionada)
        {
            comboBoxMateria.Items.Clear();

            foreach (var item in disciplinaSelecionada.materias)
            {
                comboBoxMateria.Items.Add(item);
            }
        }

        private void CarregarDisciplinas(List<Disciplina> disciplinas)
        {
            comboBoxDisciplina.Items.Clear();

            foreach (var item in disciplinas)
            {
                comboBoxDisciplina.Items.Add(item);
            }
        }

        public Teste Teste
        {
            get
            {
                return teste;
            }
            set
            {
                teste = value;
                textBoxTitulo.Text = teste.Titulo;
                comboBoxDisciplina.SelectedItem = teste.Disciplina;
                comboBoxMateria.SelectedItem = teste.Materia;
                txtData.Text = teste.DataDeCriacao.ToShortDateString();
                maskedTextBoxQuestoes.Text = teste.QuantidadeDeQuestoes.ToString();

                if(teste.Provao==true)
                    checkBoxProvão.Checked=true;
                else
                    checkBoxProvão.Checked = false;
            }
        }

        private void buttonGravar_Click(object sender, EventArgs e)
        {
            teste.Titulo = textBoxTitulo.Text;
            teste.DataDeCriacao =txtData.Value;

            if (checkBoxProvão.Checked)
                teste.Provao=true;
            else
                teste.Provao=false; 
            

            var resultadoValidacao = GravarRegistro(teste);

            if (resultadoValidacao.IsValid == false)
            {
                string erro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                DialogResult = DialogResult.None;
            }
        }

        private void buttonSortearQuestoes_Click(object sender, EventArgs e)
        {
            List<Questao> Questoes =new();
            if (checkBoxProvão.Checked == true)
               Questoes = CarregarQuestoesProvao();
            else
                Questoes = CarregarQuestoesMateria();

            listBoxQuestoes.Items.Clear();

            foreach(Questao item in Questoes)
            {
                listBoxQuestoes.Items.Add(item);
            }
        }

        private List<Questao> CarregarQuestoesProvao()
        {
            List<Questao> Questoes=new();
            int numeroDeQuestoes = Convert.ToInt32(maskedTextBoxQuestoes.Text);
            Disciplina disciplinaSelecionada = (Disciplina)comboBoxDisciplina.SelectedItem;
            
            for (int i = 0; i < numeroDeQuestoes; i++)
            {

                //Materia Aleatória
                Random rnd = new Random();

                Materia materiaselecionada = disciplinaSelecionada.materias[rnd.Next(disciplinaSelecionada.materias.Count)];


                //Questão aleatória
                Random random = new Random();

                Questao questaoAleatoria = materiaselecionada.questoes[random.Next(materiaselecionada.questoes.Count)];

                Questoes.Add(questaoAleatoria);

            }
           
            return Questoes;
        }
        
        private List<Questao> CarregarQuestoesMateria()
        {
            
            int numeroDeQuestoes = Convert.ToInt32(maskedTextBoxQuestoes.Text);
            Materia materiaselecionada = (Materia)comboBoxMateria.SelectedItem;
            List<Questao> Questoes=new();

            //Questões Aleatórias
            for (int i = 0; i < numeroDeQuestoes; i++)
            {
                Random rnd = new Random();

                Questao questaoAleatoria = materiaselecionada.questoes[rnd.Next(materiaselecionada.questoes.Count)];

                Questoes.Add(questaoAleatoria);

            }
            return Questoes;
        }

    }
}
