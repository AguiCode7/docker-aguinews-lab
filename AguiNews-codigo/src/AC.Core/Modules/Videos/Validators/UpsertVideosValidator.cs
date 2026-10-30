using AC.Core.Modules.Videos.DTOs;
using FluentValidation;

namespace AC.Core.Modules.Videos.Validators;

// Reglas del lote: no vacío, con tope de tamaño y cada video válido.
public class UpsertVideosValidator : AbstractValidator<UpsertVideosDto>
{
    public const int MaxVideos = 100;

    public UpsertVideosValidator()
    {
        RuleFor(x => x.Videos).NotEmpty()
            .Must(v => v is null || v.Count <= MaxVideos)
            .WithMessage($"Videos must have at most {MaxVideos} items.");
        RuleForEach(x => x.Videos).SetValidator(new UpsertVideoValidator());
    }
}
