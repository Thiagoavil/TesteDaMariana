using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteMariana.Dominio.Compartilhado
{
    public abstract class EntidadeBase<T>
    {
        public string Titulo { get; set; }
        public int Numero { get; set; }

        public abstract void Atualizar(T registro);
    }
}
