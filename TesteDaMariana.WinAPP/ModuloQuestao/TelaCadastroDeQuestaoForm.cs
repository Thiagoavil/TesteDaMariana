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
        private List<Materia> materias;
        private List<Alternativas> alternativas;
        private IRepositorioQuestao repositorioQuestao;
        public TelaCadastroDeQuestaoForm(List<Disciplina> disciplinas,List<Materia> materias,IRepositorioQuestao repositorioQuestao)
        {
            InitializeComponent();
            this.repositorioQuestao=repositorioQuestao;
            CarregarDisciplinas(disciplinas);
            this.materias = materias;
            alternativas = new();
            
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
        private void CarregarMaterias(List<Materia> materias)
        {
            
            comboBoxMateria.Items.Clear();

            foreach (var item in materias)
            {
                if(item.disciplina==(Disciplina)comboBoxDisciplina.SelectedItem)
                {
                    comboBoxMateria.Items.Add(item);
                }
               
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
                Alternativas alternativa = new ();

                alternativa.Resposta = textBoxRespostas.Text;
                alternativas.Add (alternativa);
                listBox1.Items.Add(alternativa);

                if(checkBoxRespostaCerta.Checked)
                    alternativa.Correta = true;
                else
                    alternativa.Correta = false;
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
            
            foreach(Alternativas item in alternativas  )
            {
                questao.alternativas.Add(item);
            }

            var resultadoValidacao = GravarRegistro(Questao);

            if(resultadoValidacao.IsValid==false)
            {
                string erro = resultadoValidacao.Errors[0].ErrorMessage;

                TelaPrincipalForm.Instancia.AtualizarRodape(erro);

                DialogResult = DialogResult.None;
            }
            
        }

        private void comboBoxDisciplina_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBoxMateria.Enabled = true;
            CarregarMaterias(materias);
        }
    }
}
