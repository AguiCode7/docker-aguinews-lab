using AC.Core.Modules.Candidates.DTOs;
using FluentValidation;

namespace AC.Core.Modules.Candidates.Validators;

// Reglas del lote: no vacío, con tope de tamaño y cada candidata válida.
public class CreateCandidatesValidator : AbstractValidator<CreateCandidatesDto>
{
    public const int MaxCandidates = 50;

    public CreateCandidatesValidator()
    {
        RuleFor(x => x.Candidates).NotEmpty()
            .Must(c => c is null || c.Count <= MaxCandidates)
            .WithMessage($"Candidates must have at most {MaxCandidates} items.");
        RuleForEach(x => x.Candidates).SetValidator(new CreateCandidateValidator());
    }
}
