using FluentValidation;

namespace CardActionService.Api.Requests;

public class CardRequestValidator : AbstractValidator<CardRequest>
{
    public CardRequestValidator(IWebHostEnvironment env)
    {
        var isDev = env.IsDevelopment();

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId is required.")
            .Length(3, 20).WithMessage("UserId length must be between 3 and 20 characters.")
            .Matches("^[a-zA-Z0-9]+$").WithMessage("UserId must be alphanumeric.");

        if (isDev)
        {
            RuleFor(x => x.CardNumber)
                .NotEmpty().WithMessage("CardNumber is required.");
        }
        else
        {
            RuleFor(x => x.CardNumber)
                .NotEmpty().WithMessage("CardNumber is required.")
                .Length(3, 19).WithMessage("CardNumber length must be between 3 and 19 digits.")
                .Matches("^[0-9]+$").WithMessage("CardNumber must contain only digits.")
                .Must(IsValidLuhn).WithMessage("CardNumber is invalid (failed Luhn check).");
        }
    }

    static private bool IsValidLuhn(string? number)
    {
        if (string.IsNullOrWhiteSpace(number))
            return false;

        var sum = 0;
        var alternate = false;

        for (var i = number.Length - 1; i >= 0; i--)
        {
            var c = number[i];
            if (!char.IsDigit(c))
                return false;

            var n = c - '0';
            if (alternate)
            {
                n *= 2;
                if (n > 9)
                    n -= 9;
            }

            sum += n;
            alternate = !alternate;
        }

        return sum % 10 == 0;
    }
}