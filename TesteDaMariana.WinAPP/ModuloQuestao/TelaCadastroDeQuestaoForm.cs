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

namespace TesteDaMariana.WinAPP.ModuloQuestao
{
    public partial class TelaCadastroDeQuestaoForm : Form
    {
        public TelaCadastroDeQuestaoForm(List<Disciplina> disciplinas)
        {
            InitializeComponent();
            CarregarDisciplinas(disciplinas);
            var disciplinaselecionada = (Disciplina)comboBoxDisciplina.SelectedItem;
            CarregarMaterias(disciplinaselecionada);
            
        }
        private Questao questao;

        public Func<Questao, ValidationResult> GravarRegistro { get; set; }

        private void CarregarDisciplinas(List<Disciplina> disciplinas)  
        {
            comboBoxDisciplina.Items.Clear();

            foreach (var item in disciplinas)
            {
                comboBoxDisciplina.Items.Add(item);
            }
        }
        private void CarregarMaterias(Disciplina disciplinaselecionada)
        {
            
            comboBoxMateria.Items.Clear();

            foreach (var item in disciplinaselecionada.materias)
            {
                comboBoxMateria.Items.Add(item);
            }
        }


        public Questao Questao
        {
            get { return questao; }
            set
            {
                questao = value;

                textBoxEnunciado.Text = questao.Titulo;
                comboBoxDisciplina.SelectedItem = questao.disciplina;
                comboBoxMateria.SelectedItem = questao.materia;
                
                foreach (Alternativas item in questao.alternativas)
                {
                    listBox1.Items.Add(item);
                }

            }
        }
        public List<Alternativas> AlternativasAdicionadas
        {
            get
            {
                return listBox1.Items.Cast<Alternativas>().ToList();
            }
        }

        private void buttonAdicionar_Click(object sender, EventArgs e)
        {
            List<string> titulos = AlternativasAdicionadas.Select(x => x.Resposta).ToList();

            if (titulos.Count == 0 || titulos.Contains(textBoxRespostas.Text) == false)
            {
                Alternativas alternativas = new ();

                alternativas.Resposta = textBoxRespostas.Text;

                listBox1.Items.Add(alternativas);
            }
        }
        

        private void buttonCancelar_Click(object sender, EventArgs e)
        {

        }

        private void buttonGravar_Click(object sender, EventArgs e)
        {
            questao.Titulo = textBoxEnunciado.Text;
            questao.disciplina = (Disciplina)comboBoxDisciplina.SelectedItem;
            questao.materia=(Materia)comboBoxMateria.SelectedItem;

            
        }

    }
}
