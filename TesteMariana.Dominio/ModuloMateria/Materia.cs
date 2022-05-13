using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteMariana.Dominio.Compartilhado;
using TesteMariana.Dominio.ModuloDisciplina;

namespace TesteMariana.Dominio.ModuloMateria
{
    public class Materia : EntidadeBase<Materia>
    {
        public Disciplina disciplina { get; set; }
        public Serie Serie { get; set; }



        public override void Atualizar(Materia registro)
        {
        }
    }
}
