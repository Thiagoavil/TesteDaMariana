using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteDaMariana.WinAPP.Compartilhado;

namespace TesteDaMariana.WinAPP.ModuloTeste
{
    internal class ConfiguracaoToolBoxTeste : ConfiguracaoToolBoxBase
    {
        public override string TipoCadastro => "Controle de Testes";

        public override string TooltipInserir => "Inserir um novo teste";

        public override string TooltipEditar => "Editar um teste existente";

        public override string TooltipExcluir => "Excluir um teste existente";

        public override string TooltipGerarPdf => "Gerar um PDF";
        public override string TooltipDuplicar => "Duplicar Teste";
        public override bool GerarpdfHabilitado { get { return true; } }
        public override bool duplicarHabilitado { get { return true; } }
    }
}
