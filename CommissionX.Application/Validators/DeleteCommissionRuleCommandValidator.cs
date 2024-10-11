using CommissionX.Application.Commands;
using FluentValidation;

namespace CommissionX.Application.Validators
{
    public class DeleteCommissionRuleCommandValidator : AbstractValidator<DeleteCommissionRuleCommand>
    {
        public DeleteCommissionRuleCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Commission rule ID is required for deletion.");
        }
    }
}