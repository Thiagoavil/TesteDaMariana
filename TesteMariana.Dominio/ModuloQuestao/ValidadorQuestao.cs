using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TesteMariana.Dominio.ModuloQuestao
{
    public class ValidadorQuestao : AbstractValidator<Questao>
    {
        public ValidadorQuestao()
        {
            RuleFor(x => x.Titulo)
                .NotNull()
                .NotEmpty();

            RuleFor(x => x.materia)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.disciplina)
                .NotEmpty()
                .NotNull();
        }
    }
}
