using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteDaMariana.WinAPP.Compartilhado
{
    public abstract class ConfiguracaoToolBoxBase
    {
        public abstract string TipoCadastro { get; }

        public abstract string TooltipInserir { get; }

        public abstract string TooltipEditar { get; }

        public abstract string TooltipExcluir { get; }

        public abstract string TooltipGerarPdf { get; }
        public abstract string TooltipDuplicar { get; }

        public virtual bool InserirHabilitado { get { return true; } }

        public virtual bool EditarHabilitado { get { return true; } }

        public virtual bool ExcluirHabilitado { get { return true; } }
        public virtual bool GerarpdfHabilitado { get { return false; } }
        public virtual bool duplicarHabilitado { get { return false; } }

    }
}
