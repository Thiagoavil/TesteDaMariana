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
    }
}
