using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteDaMariana.WinAPP.Compartilhado;

namespace TesteDaMariana.WinAPP.ModuloMateria
{
    internal class ConfiguracaoToolBoxMateria : ConfiguracaoToolBoxBase
    {
        public override string TipoCadastro => "Controle de Materias";

        public override string TooltipInserir => "Inserir uma nova materia";

        public override string TooltipEditar => "Editar uma materia existente";

        public override string TooltipExcluir => "Excluir uma materia existente";

        public override string TooltipGerarPdf { get { return string.Empty; } }
        public override string TooltipDuplicar { get { return string.Empty; } }

    }
}
