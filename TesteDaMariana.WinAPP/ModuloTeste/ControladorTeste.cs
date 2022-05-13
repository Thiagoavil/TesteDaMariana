using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TesteDaMariana.WinAPP.Compartilhado;
using TesteMariana.Dominio.ModuloDisciplina;
using TesteMariana.Dominio.ModuloMateria;
using TesteMariana.Dominio.ModuloQuestao;
using TesteMariana.Dominio.ModuloTeste;

namespace TesteDaMariana.WinAPP.ModuloTeste
{
    internal class ControladorTeste : ControladorBase
    {
        private readonly IRepositorioMateria repositorioMateria;
        private readonly IRepositorioDisciplina repositorioDisciplina;
        private readonly IRepositorioQuestao repositorioQuestao;
        private readonly IRepositorioTeste repositorioTeste;


        private ListagemTesteControl tabelaTeste;
        public ControladorTeste(IRepositorioMateria repositorioMateria, IRepositorioDisciplina repositorioDisciplina, IRepositorioQuestao repositorioQuestao, IRepositorioTeste repositorioTeste)
        {
            this.repositorioMateria = repositorioMateria;
            this.repositorioDisciplina = repositorioDisciplina;
            this.repositorioQuestao = repositorioQuestao;
            this.repositorioTeste = repositorioTeste;
        }

        public override void Inserir()
        {
            var materia = repositorioMateria.SelecionarTodos();
            var disciplina = repositorioDisciplina.SelecionarTodos();
            var questao =repositorioQuestao.SelecionarTodos();

            TelaCriacaoTesteForm tela = new TelaCriacaoTesteForm(materia, disciplina,questao);
            tela.Teste = new Teste();

            tela.GravarRegistro = repositorioTeste.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarTestes();
            }
        }

        public override void Editar()
        {
            Teste TesteSelecionado = ObtemTesteSelecionado();

            if (TesteSelecionado == null)
            {
                MessageBox.Show("Selecione um teste primeiro",
                "Edição de Teste", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var materia = repositorioMateria.SelecionarTodos();
            var disciplina = repositorioDisciplina.SelecionarTodos();
            var questão = repositorioQuestao.SelecionarTodos();

            TelaCriacaoTesteForm tela = new TelaCriacaoTesteForm(materia, disciplina,questão);

            tela.Teste = TesteSelecionado;

            tela.GravarRegistro = repositorioQuestao.Editar;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarTestes();
            }
        }

        public override void Excluir()
        {
            Teste TesteSelecionado = ObtemTesteSelecionado();

            if (TesteSelecionado == null)
            {
                MessageBox.Show("Selecione uma Teste primeiro",
                "Exclusão de teste", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir o teste?",
                "Exclusão de teste", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                repositorioTeste.Excluir(TesteSelecionado);
                CarregarTestes();
            }
        }



        public override UserControl ObtemListagem()
        {
            if (tabelaTeste == null)
                tabelaTeste = new ListagemTesteControl();

            CarregarTestes();

            return tabelaTeste;
        }

        public override ConfiguracaoToolBoxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolBoxTeste();
        }

        private Teste ObtemTesteSelecionado()
        {
            var numero = tabelaTeste.ObtemNumeroTesteSelecionado();

            return repositorioTeste.SelecionarPorNumero(numero);
        }

        private void CarregarTestes()
        {
            List<Teste> testes = repositorioTeste.SelecionarTodos();

            tabelaTeste.AtualizarRegistros(testes);

            TelaPrincipalForm.Instancia.AtualizarRodape($"Visualizando {testes.Count} Teste(s)");
        }
    }
}
