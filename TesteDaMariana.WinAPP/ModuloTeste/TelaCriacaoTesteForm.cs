using FluentValidation.Results;
using System;
using System.Collections.Generic;
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
        private List<Materia> materias;
        private List<Questao> questoes;
        public TelaCriacaoTesteForm(List<Disciplina> disciplinas, List<Materia> materias, List<Questao> questoes)
        {
            InitializeComponent();
            this.materias = materias;
            this.questoes = questoes;


            CarregarDisciplinas(disciplinas);

            CarregarMaterias(materias);

        }

        public Func<Teste, ValidationResult> GravarRegistro { get; set; }

        private void CarregarMaterias(List<Materia> materias)
        {

            comboBoxMateria.Items.Clear();

            foreach (var item in materias)
            {
                if (item.disciplina == (Disciplina)comboBoxDisciplina.SelectedItem)
                {
                    comboBoxMateria.Items.Add(item);
                }
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


                foreach (Questao item in teste.questoes)
                {
                    listBoxQuestoes.Items.Add(item);
                }



                if (teste.Provao == true)
                    checkBoxProvão.Checked = true;
                else
                    checkBoxProvão.Checked = false;
            }
        }

        private void buttonGravar_Click(object sender, EventArgs e)
        {
            teste.Titulo = textBoxTitulo.Text;
            teste.DataDeCriacao = txtData.Value;

            if (checkBoxProvão.Checked)
                teste.Provao = true;
            else
                teste.Provao = false;


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
            List<Questao> Questoes = new();
            if (checkBoxProvão.Checked == true)
                Questoes = CarregarQuestoesProvao();
            else
                Questoes = CarregarQuestoesMateria();

            listBoxQuestoes.Items.Clear();

            foreach (Questao item in Questoes)
            {
                listBoxQuestoes.Items.Add(item);
            }
        }

        private List<Questao> CarregarQuestoesProvao()
        {
            List<Questao> Questoes = new();
            int numeroDeQuestoes = Convert.ToInt32(maskedTextBoxQuestoes.Text);
            Disciplina disciplinaSelecionada = (Disciplina)comboBoxDisciplina.SelectedItem;
            List<Questao> sortearQuestão = new();


            for (int i = 0; i < numeroDeQuestoes; i++)
            {

                //Materia Aleatória
                Random rnd = new Random();

                Materia materiaselecionada = materias[rnd.Next(materias.Count)];

                foreach (Questao item in questoes)
                {
                    if (item.materia == materiaselecionada)
                        sortearQuestão.Add(item);
                }

                //Questão aleatória
                Random random = new Random();

                Questao questaoAleatoria = sortearQuestão[random.Next(sortearQuestão.Count)];

                if (Questoes.Contains(questaoAleatoria))
                {
                    i--;
                }
                else
                {
                    Questoes.Add(questaoAleatoria);
                }

            }

            return Questoes;
        }

        private List<Questao> CarregarQuestoesMateria()
        {

            int numeroDeQuestoes = Convert.ToInt32(maskedTextBoxQuestoes.Text);
            Materia materiaselecionada = (Materia)comboBoxMateria.SelectedItem;
            List<Questao> Questoes = new();
            List<Questao> sortearQuestão = new();

            foreach (Questao item in questoes)
            {
                if (item.materia == materiaselecionada)
                    sortearQuestão.Add(item);
            }

            //Questões Aleatórias


            for (int i = 0; i < numeroDeQuestoes; i++)
            {

                Random rnd = new Random();

                Questao questaoAleatoria = sortearQuestão[rnd.Next(sortearQuestão.Count)];

                if (Questoes.Contains(questaoAleatoria))
                {
                    i--;
                }
                else
                {
                    Questoes.Add(questaoAleatoria);
                }


            }
            return Questoes;
        }



    }
}
