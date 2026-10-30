using AC.Core.Modules.Candidates.DTOs;
using FluentValidation;

namespace AC.Core.Modules.Candidates.Validators;

// Reglas de cada candidata que llega desde n8n.
public class CreateCandidateValidator : AbstractValidator<CreateCandidateDto>
{
    public const int TitleMaxLength = 300;
    public const int UrlMaxLength = 2000;
    public const int SourceMaxLength = 100;
    public const int MaxSources = 10;
    public const int MinScore = 0;
    public const int MaxScore = 30;

    public CreateCandidateValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(TitleMaxLength);
        RuleFor(x => x.Url).NotEmpty().MaximumLength(UrlMaxLength)
            .Must(BeHttpUrl).WithMessage("Url must be an absolute http or https URL.");
        RuleFor(x => x.Source).NotEmpty().MaximumLength(SourceMaxLength);
        RuleFor(x => x.Sources)
            .Must(s => s is null || s.Count <= MaxSources)
            .WithMessage($"Sources must have at most {MaxSources} items.");
        RuleForEach(x => x.Sources).NotEmpty().MaximumLength(SourceMaxLength);
        RuleFor(x => x.Score).InclusiveBetween(MinScore, MaxScore);
    }

    // Solo enlaces web: evita esquemas como javascript: o file:.
    private static bool BeHttpUrl(string url) =>
        Uri.TryCreate(url, UriKind.Absolute, out var uri)
        && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
}
