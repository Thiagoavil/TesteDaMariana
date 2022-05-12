using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteDaMariana.WinAPP.ModuloDisciplina
{
    public class ConfiguracaoToolBoxDisciplina
    {
        public override string TipoCadastro => "Controle de Compromissos";

        public override string TooltipInserir => "Inserir um novo compromisso";

        public override string TooltipEditar => "Editar um compromisso existente";

        public override string TooltipExcluir => "Excluir um compromisso existente";

        public override string TooltipFiltrar { get { return "Filtrar Compromisso por Status"; } }

        public override bool FiltrarHabilitado => true;
    }
}
