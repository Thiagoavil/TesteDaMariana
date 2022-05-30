using System;
using System.Collections.Generic;
using Marian.Infra.BancoDados.ModuloMateria;
using System.Windows.Forms;
using TesteDaMariana.WinAPP.Compartilhado;
using TesteDaMariana.WinAPP.ModuloDisciplina;
using TesteDaMariana.WinAPP.ModuloMateria;
using TesteDaMariana.WinAPP.ModuloQuestao;
using TesteDaMariana.WinAPP.ModuloTeste;
using TesteMariana.Infra.Arquivos.Compartilhado;
using TesteMariana.Infra.Arquivos.ModuloDisciplina;
using TesteMariana.Infra.Arquivos.ModuloMateria;
using TesteMariana.Infra.Arquivos.ModuloQuestao;
using TesteMariana.Infra.Arquivos.ModuloTeste;
using TesteDaMariana.WinAPP.Compartilhado;
using Marian.Infra.BancoDados.ModuloDisciplina;
using Marian.Infra.BancoDados.ModuloQuestao;
using Marian.Infra.BancoDados.ModuloTeste;

namespace TesteDaMariana.WinAPP
{
    public partial class TelaPrincipalForm : Form
    {
        private ControladorBase controlador;
        private Dictionary<string, ControladorBase> controladores;
        private DataContext contextoDados;
        public TelaPrincipalForm(DataContext contextoDados)
        {
            InitializeComponent();
            Instancia = this;

            labelRodape.Text = string.Empty;
            labelTipoCadastro.Text = string.Empty;

            this.contextoDados = contextoDados;

            InicializarControladores();
        }
        public static TelaPrincipalForm Instancia
        {
            get;
            private set;
        }
        public void AtualizarRodape(string mensagem)
        {
            labelRodape.Text = mensagem;
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            controlador.Inserir();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            controlador.Editar();
        }
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            controlador.Excluir();
        }
        private void toolStripButtonDuplicar_Click(object sender, EventArgs e)
        {
            controlador.Duplicar();
        }
        private void btnPDF_Click(object sender, EventArgs e)
        {
            controlador.PDF();
        }

        private void disciplinasMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
        }

        private void materiaMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
        }

        private void questaoMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender);
        }

        private void testeMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurarTelaPrincipal((ToolStripMenuItem)sender); ;
        }
        private void ConfigurarBotoes(ConfiguracaoToolBoxBase configuracao)
        {
            btnInserir.Enabled = configuracao.InserirHabilitado;
            btnEditar.Enabled = configuracao.EditarHabilitado;
            btnExcluir.Enabled = configuracao.ExcluirHabilitado;
            btnPDF.Enabled = configuracao.GerarpdfHabilitado;
            toolStripButtonDuplicar.Enabled = configuracao.duplicarHabilitado;
        }

        private void ConfigurarTooltips(ConfiguracaoToolBoxBase configuracao)
        {
            btnInserir.ToolTipText = configuracao.TooltipInserir;
            btnEditar.ToolTipText = configuracao.TooltipEditar;
            btnExcluir.ToolTipText = configuracao.TooltipExcluir;
            btnPDF.ToolTipText = configuracao.TooltipGerarPdf;
            toolStripButtonDuplicar.ToolTipText= configuracao.TooltipDuplicar;
        }

        private void ConfigurarTelaPrincipal(ToolStripMenuItem opcaoSelecionada)
        {
            var tipo = opcaoSelecionada.Text;

            controlador = controladores[tipo];

            ConfigurarToolbox();

            ConfigurarListagem();
        }

        private void ConfigurarToolbox()
        {
            ConfiguracaoToolBoxBase configuracao = controlador.ObtemConfiguracaoToolbox();

            if (configuracao != null)
            {
                toolStrip1.Enabled = true;

                labelTipoCadastro.Text = configuracao.TipoCadastro;

                ConfigurarTooltips(configuracao);

                ConfigurarBotoes(configuracao);
            }
        }

        private void ConfigurarListagem()
        {
            AtualizarRodape("");

            var listagemControl = controlador.ObtemListagem();

            panelRegistros.Controls.Clear();

            listagemControl.Dock = DockStyle.Fill;

            panelRegistros.Controls.Add(listagemControl);
        }

        private void InicializarControladores()
        {
            var repositorioDisciplina = new RepositorioDisciplinaEmBancoDeDados();
            var repositorioMateria = new RepositorioMateriaEmBancoDeDados();
            var repositorioQuestao = new RepositorioQuestaoEmBancoDeDados();
            var repositorioTeste = new RepositorioTesteEmBancoDeDados();

            controladores = new Dictionary<string, ControladorBase>();

            controladores.Add("Disciplina", new ControladorDisciplina(repositorioDisciplina,repositorioMateria));
            controladores.Add("Matéria", new ControladorMateria(repositorioMateria,repositorioDisciplina));
            controladores.Add("Questão", new ControladorQuestao( repositorioDisciplina,repositorioMateria,repositorioQuestao));
            controladores.Add("Teste", new ControladorTeste (repositorioTeste,repositorioMateria, repositorioDisciplina,repositorioQuestao));
        }

    }
}
