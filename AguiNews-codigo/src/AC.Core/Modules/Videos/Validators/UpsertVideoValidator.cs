using AC.Core.Modules.Videos.DTOs;
using FluentValidation;

namespace AC.Core.Modules.Videos.Validators;

// Reglas de cada video que llega desde el feed del canal.
public class UpsertVideoValidator : AbstractValidator<UpsertVideoDto>
{
    public const int YouTubeIdMaxLength = 20;
    public const int TitleMaxLength = 200;

    public UpsertVideoValidator()
    {
        // Los ids de YouTube solo usan letras, números, "-" y "_".
        RuleFor(x => x.YouTubeId).NotEmpty().MaximumLength(YouTubeIdMaxLength)
            .Matches("^[A-Za-z0-9_-]+$");
        RuleFor(x => x.Title).NotEmpty().MaximumLength(TitleMaxLength);
        RuleFor(x => x.Kind).IsInEnum();
        RuleFor(x => x.PublishedAt).NotEmpty();
    }
}
