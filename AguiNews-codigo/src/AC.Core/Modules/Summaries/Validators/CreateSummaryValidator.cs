using AC.Core.Modules.Articles.Validators;
using AC.Core.Modules.Summaries.DTOs;
using FluentValidation;

namespace AC.Core.Modules.Summaries.Validators;

// Reglas de validación de la edición que llega desde n8n.
public class CreateSummaryValidator : AbstractValidator<CreateSummaryDto>
{
    public const int SourceMaxLength = 200;
    public const int MaxArticles = 10;

    public CreateSummaryValidator()
    {
        RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required.");
        RuleFor(x => x.Source).NotEmpty().MaximumLength(SourceMaxLength);
        RuleFor(x => x.NewsCount).GreaterThan(0);
        RuleFor(x => x.Articles)
            .Must(a => a is null || a.Count <= MaxArticles)
            .WithMessage($"Articles must have at most {MaxArticles} items.");
        RuleForEach(x => x.Articles).SetValidator(new CreateArticleValidator());
    }
}
