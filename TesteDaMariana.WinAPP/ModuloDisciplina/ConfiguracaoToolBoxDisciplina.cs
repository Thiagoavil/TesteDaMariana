using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteDaMariana.WinAPP.Compartilhado;

namespace TesteDaMariana.WinAPP.ModuloDisciplina
{
    public class ConfiguracaoToolBoxDisciplina : ConfiguracaoToolBoxBase
    {
        public override string TipoCadastro => "Controle de Disciplinas";

        public override string TooltipInserir => "Inserir uma nova disciplina";

        public override string TooltipEditar => "Editar uma disciplina existente";

        public override string TooltipExcluir => "Excluir uma disciplina existente";

        public override string TooltipGerarPdf => "Gerar um PDF";
    }
}
