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
            btnPDF.Enabled = configuracao.TooltipGerarpdfHabilitado;
        }

        private void ConfigurarTooltips(ConfiguracaoToolBoxBase configuracao)
        {
            btnInserir.ToolTipText = configuracao.TooltipInserir;
            btnEditar.ToolTipText = configuracao.TooltipEditar;
            btnExcluir.ToolTipText = configuracao.TooltipExcluir;
            btnPDF.ToolTipText = configuracao.TooltipGerarPdf;
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
            var repositorioDisciplina = new RepositorioDisciplinaEmArquivo(contextoDados);
            var repositorioMateria = new RepositorioMateriaEmArquivo(contextoDados);
            var repositorioQuestao = new RepositorioQuestaoEmArquivo(contextoDados);
            var repositorioTeste = new RepositorioTesteEmArquivo(contextoDados);

            controladores = new Dictionary<string, ControladorBase>();

            controladores.Add("Disciplina", new ControladorDisciplina(repositorioDisciplina));
            controladores.Add("Matéria", new ControladorMateria(repositorioMateria,repositorioDisciplina));
            controladores.Add("Questão", new ControladorQuestao( repositorioDisciplina,repositorioQuestao));
            controladores.Add("Teste", new ControladorTeste (repositorioTeste,repositorioMateria, repositorioDisciplina,repositorioQuestao));
        }

    }
}
