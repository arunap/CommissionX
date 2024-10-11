using CommissionX.Application.Commands;
using FluentValidation;

namespace CommissionX.Application.Validators
{
    public class UpdateCommissionRuleCommandValidator : AbstractValidator<UpdateCommissionRuleCommand>
    {
        public UpdateCommissionRuleCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Commission rule ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Commission rule name is required.")
                .MaximumLength(100).WithMessage("Commission rule name cannot exceed 100 characters.");

            RuleFor(x => x.Value)
                .GreaterThan(0).WithMessage("Commission rule value must be greater than zero.");

            RuleFor(x => x.RuleContextType)
                .IsInEnum().WithMessage("Invalid rule context type.");

            RuleFor(x => x.RateCalculationType)
                .IsInEnum().WithMessage("Invalid rate calculation type.");

            RuleFor(x => x.CommissionRuleType)
                .IsInEnum().WithMessage("Invalid commission rule type.");
        }
    }
}