using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TesteDaMariana.WinAPP.Compartilhado
{
    public class ControladorBase
    {
        public abstract void Inserir();
        public abstract void Editar();
        public abstract void Excluir();

        public virtual void AdicionarItens() { }

        public virtual void AtualizarItens() { }

        public virtual void Filtrar() { }

        public virtual void Agrupar() { }

        public abstract UserControl ObtemListagem();

        public abstract ConfiguracaoToolboxBase ObtemConfiguracaoToolbox();
    }
}
