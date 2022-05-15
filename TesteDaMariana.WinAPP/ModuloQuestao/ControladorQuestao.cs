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

namespace TesteDaMariana.WinAPP.ModuloQuestao
{
    internal class ControladorQuestao : ControladorBase
    {
        private readonly IRepositorioMateria repositorioMateria;
        private readonly IRepositorioDisciplina repositorioDisciplina;
        private readonly IRepositorioQuestao repositorioQuestao;


        private ListagemQuestaoControl tabelaQuestao;
        public ControladorQuestao(IRepositorioMateria repositorioMateria, IRepositorioDisciplina repositorioDisciplina, IRepositorioQuestao repositorioQuestao)
        {
            this.repositorioMateria = repositorioMateria;
            this.repositorioDisciplina = repositorioDisciplina;
            this.repositorioQuestao = repositorioQuestao;
        }

        public override void Inserir()
        {
            var materia = repositorioMateria.SelecionarTodos();
            var disciplina = repositorioDisciplina.SelecionarTodos();

            TelaCadastroDeQuestaoForm tela = new TelaCadastroDeQuestaoForm(materia,disciplina);
            tela.Questao = new Questao();

            tela.GravarRegistro = repositorioMateria.Inserir;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarQuestoes();
            }
        }

        public override void Editar()
        {
            Questao QuestaoSelecionada = ObtemQuestaoSelecionada();

            if (QuestaoSelecionada == null)
            {
                MessageBox.Show("Selecione uma questão primeiro",
                "Edição de Questão", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var materia = repositorioMateria.SelecionarTodos();
            var disciplina = repositorioDisciplina.SelecionarTodos();

            TelaCadastroDeQuestaoForm tela = new TelaCadastroDeQuestaoForm(materia,disciplina);

            tela.Questao = QuestaoSelecionada;

            tela.GravarRegistro = repositorioQuestao.Editar;

            DialogResult resultado = tela.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                CarregarQuestoes();
            }
        }

        public override void Excluir()
        {
            Questao questaoSelecionada = ObtemQuestaoSelecionada();

            if (questaoSelecionada == null)
            {
                MessageBox.Show("Selecione uma Questão primeiro",
                "Exclusão de questão", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult resultado = MessageBox.Show("Deseja realmente excluir a questão?",
                "Exclusão de questão", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (resultado == DialogResult.OK)
            {
                repositorioQuestao.Excluir(questaoSelecionada);
                CarregarQuestoes();
            }
        }



        public override UserControl ObtemListagem()
        {
            if (tabelaQuestao == null)
                tabelaQuestao = new ListagemQuestaoControl();

            CarregarQuestoes();

            return tabelaQuestao;
        }

        public override ConfiguracaoToolBoxBase ObtemConfiguracaoToolbox()
        {
            return new ConfiguracaoToolBoxQuestao();
        }

        private Questao ObtemQuestaoSelecionada()
        {
            var numero = tabelaQuestao.ObtemNumeroQuestaoSelecionada();

            return repositorioQuestao.SelecionarPorNumero(numero);
        }

        private void CarregarQuestoes()
        {
            List<Questao> questoes = repositorioQuestao.SelecionarTodos();

            tabelaQuestao.AtualizarRegistros(questoes);

            TelaPrincipalForm.Instancia.AtualizarRodape($"Visualizando {questoes.Count} Questõe(s)");
        }
        public override void PDF()
        {
            throw new NotImplementedException();
        }
    }
}
