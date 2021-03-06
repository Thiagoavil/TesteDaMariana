using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteDaMariana.WinAPP.Compartilhado;

namespace TesteDaMariana.WinAPP.ModuloQuestao
{
    internal class ConfiguracaoToolBoxQuestao : ConfiguracaoToolBoxBase
    {
        public override string TipoCadastro => "Controle de Questões";

        public override string TooltipInserir => "Inserir uma nova questão";

        public override string TooltipEditar => "Editar uma questão existente";

        public override string TooltipExcluir => "Excluir uma questão existente";

        public override string TooltipGerarPdf { get { return string.Empty; } }
        public override string TooltipDuplicar { get { return string.Empty; } }
    }
}
