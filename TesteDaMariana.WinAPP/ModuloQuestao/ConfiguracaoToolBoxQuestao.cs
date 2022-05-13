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
        public override string TipoCadastro => "Controle de Compromissos";

        public override string TooltipInserir => "Inserir um novo compromisso";

        public override string TooltipEditar => "Editar um compromisso existente";

        public override string TooltipExcluir => "Excluir um compromisso existente";

        public override string TooltipFiltrar { get { return "Filtrar Compromisso por Status"; } }

        public override bool FiltrarHabilitado => true;
    }
}
