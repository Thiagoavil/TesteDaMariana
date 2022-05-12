using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TesteDaMariana.WinAPP.Compartilhado;
using TesteMariana.Dominio.ModuloDisciplina;
using TesteMariana.Dominio.ModuloMateria;

namespace TesteDaMariana.WinAPP.ModuloDisciplina
{
    internal class ControladorDisciplina : ControladorBase
    {
        private readonly IRepositorioDisciplina repositorioDisciplina;
        private readonly IRepositorioMateria repositorioMateria;

        private TabelaDisciplinaControl tabelaDisciplina;
        public ControladorDisciplina(IRepositorioDisciplina repositorioDisciplina, IRepositorioMateria repositorioMateria)
        {
            this.repositorioDisciplina = repositorioDisciplina;
            this.repositorioMateria = repositorioMateria;
        }

        public override void Inserir()
        {
            var contatos = repositorioDisciplina.SelecionarTodos();

            TelaCadastroDeDisciplinaForm tela = new TelaCadastroDeDisciplinaForm(contatos);
            tela.Disciplina = new Disciplina();

            tela.GravarRegistro = repositorioDisciplina.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarCompromissos();
            }
        }

        public override void Editar()
        {
            Disciplina disciplinaSelecionada = ObtemDisciplinaSelecionada();

            if (disciplinaSelecionada == null)
            {
                MessageBox.Show("Selecione uma disciplina primeiro",
                "Edição de Disciplina", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var disciplina = repositorioDisciplina.SelecionarTodos();

            TelaCadastroDeDisciplinaForm tela = new TelaCadastroDeDisciplinaForm(contatos);

            tela.Disciplina = disciplinaSelecionada;

            tela.GravarRegistro = repositorioDisciplina.Editar;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarCompromissos();
            }
        }

        public override void Excluir()
        {
            Disciplina disciplinaSelecionada = ObtemDisciplinaSelecionada();

            if (disciplinaSelecionada == null)
            {
                MessageBox.Show("Selecione uma disciplina primeiro",
                "Exclusão de disciplina", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir a disciplina?",
                "Exclusão de disciplina", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                repositorioDisciplina.Excluir(disciplinaSelecionada);
                CarregarCompromissos();
            }
        }

        public override void Filtrar()
        {
            TelaFiltroCompromissosForm telaFiltro = new TelaFiltroCompromissosForm();

            if (telaFiltro.ShowDialog() == DialogResult.OK)
            {
                var statusSelecionado = telaFiltro.StatusSelecionado;
                var dataInicial = telaFiltro.DataInicial.Date;
                var dataFinal = telaFiltro.DataFinal.Date;

                CarregarCompromissosComFiltro(statusSelecionado, dataInicial, dataFinal);
            }
        }

        public override UserControl ObtemListagem()
        {
            if (tabelaCompromissos == null)
                tabelaCompromissos = new TabelaCompromissosControl();

            CarregarCompromissos();

            return tabelaCompromissos;
        }

        public override ConfiguracaoToolboxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolboxCompromisso();
        }


        private Compromisso ObtemDisciplinaSelecionada()
        {
            var numero = tabelaCompromissos.ObtemNumeroCompromissoSelecionado();

            return repositorioCompromisso.SelecionarPorNumero(numero);
        }

        private void CarregarCompromissosComFiltro(StatusCompromissoEnum statusSelecionado, DateTime dataInicial, DateTime dataFinal)
        {
            string tipoCompromisso;
            List<Compromisso> compromissos;

            switch (statusSelecionado)
            {
                case StatusCompromissoEnum.Futuros:
                    compromissos = repositorioCompromisso.SelecionarCompromissosFuturos(dataInicial, dataFinal);
                    tipoCompromisso = "futuro(s)";
                    break;

                case StatusCompromissoEnum.Passados:
                    compromissos = repositorioCompromisso.SelecionarCompromissosPassados(DateTime.Now);
                    tipoCompromisso = "passado(s)";
                    break;

                default:
                    compromissos = repositorioCompromisso.SelecionarTodos();
                    tipoCompromisso = ""; break;
            }

            tabelaCompromissos.AtualizarRegistros(compromissos);

            TelaPrincipalForm.Instancia.AtualizarRodape($"Visualizando {compromissos.Count} compromisso(s) {tipoCompromisso}");
        }

        private void CarregarCompromissos()
        {
            List<Compromisso> compromissos = repositorioCompromisso.SelecionarTodos();

            tabelaCompromissos.AtualizarRegistros(compromissos);

            TelaPrincipalForm.Instancia.AtualizarRodape($"Visualizando {compromissos.Count} compromisso(s)");
        }
    }
}

