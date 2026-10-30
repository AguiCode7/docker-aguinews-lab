using AC.Core.Modules.Articles.DTOs;
using FluentValidation;

namespace AC.Core.Modules.Articles.Validators;

// Reglas de cada noticia que llega desde n8n.
public class CreateArticleValidator : AbstractValidator<CreateArticleDto>
{
    public const int TitleMaxLength = 300;
    public const int UrlMaxLength = 2000;
    public const int TopicMaxLength = 60;
    public const int HeadlineMaxLength = 400;
    public const int LeadMaxLength = 1000;
    public const int ReadingMaxLength = 8000;
    public const int MaxKeyPoints = 5;
    public const int KeyPointMaxLength = 300;
    public const int MinReadingMinutes = 1;
    public const int MaxReadingMinutes = 60;

    public CreateArticleValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(TitleMaxLength);
        RuleFor(x => x.Url).NotEmpty().MaximumLength(UrlMaxLength)
            .Must(BeHttpUrl).WithMessage("Url must be an absolute http or https URL.");
        RuleFor(x => x.Topic).NotEmpty().MaximumLength(TopicMaxLength);
        RuleFor(x => x.Headline).NotEmpty().MaximumLength(HeadlineMaxLength);
        RuleFor(x => x.Lead).NotEmpty().MaximumLength(LeadMaxLength);
        RuleFor(x => x.Reading).NotEmpty().MaximumLength(ReadingMaxLength);
        RuleFor(x => x.KeyPoints)
            .Must(p => p is null || p.Count <= MaxKeyPoints)
            .WithMessage($"KeyPoints must have at most {MaxKeyPoints} items.");
        RuleForEach(x => x.KeyPoints).MaximumLength(KeyPointMaxLength);
        RuleFor(x => x.ReadingMinutes)
            .InclusiveBetween(MinReadingMinutes, MaxReadingMinutes);
    }

    // Solo enlaces web: evita esquemas como javascript: o file: en el sitio.
    private static bool BeHttpUrl(string url) =>
        Uri.TryCreate(url, UriKind.Absolute, out var uri)
        && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
}
