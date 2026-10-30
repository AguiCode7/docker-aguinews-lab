using AC.Core.Modules.Videos.DTOs;
using FluentValidation;

namespace AC.Core.Modules.Videos.Validators;

// El enlace a la noticia es opcional, pero si viene debe ser una URL web.
public class LinkNewsValidator : AbstractValidator<LinkNewsDto>
{
    public const int NewsUrlMaxLength = 2000;

    public LinkNewsValidator()
    {
        RuleFor(x => x.NewsUrl!)
            .MaximumLength(NewsUrlMaxLength)
            .Must(BeHttpUrl).WithMessage("NewsUrl must be an absolute http or https URL.")
            .When(x => !string.IsNullOrWhiteSpace(x.NewsUrl));
    }

    // Solo enlaces web: evita esquemas como javascript: o file:.
    private static bool BeHttpUrl(string url) =>
        Uri.TryCreate(url, UriKind.Absolute, out var uri)
        && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
}
