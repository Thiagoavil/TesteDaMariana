using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteMariana.Dominio.Compartilhado;
using TesteMariana.Dominio.ModuloDisciplina;
using TesteMariana.Dominio.ModuloMateria;

namespace TesteMariana.Dominio.ModuloTeste
{
    public class Teste : EntidadeBase<Teste>
    {
        public Materia Materia { get; set; }
        public Disciplina Disciplina { get; set; }
        public int QuantidadeDeQuestoes { get; set; }
        public DateTime DataDeCriacao { get; set; }
        public override void Atualizar(Teste registro)
        {
        }
    }
}
