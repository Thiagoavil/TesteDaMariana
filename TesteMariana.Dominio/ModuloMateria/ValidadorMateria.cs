using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteMariana.Dominio.ModuloMateria
{
    public class ValidadorMateria : AbstractValidator<Materia>
    {
        public ValidadorMateria()
        {
            RuleFor(x => x.Titulo)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.disciplina)
                .NotNull()
                .NotEmpty();
        }
    }
}
