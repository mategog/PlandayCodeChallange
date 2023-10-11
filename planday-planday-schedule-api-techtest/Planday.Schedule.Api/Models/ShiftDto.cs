using FluentValidation;

namespace Planday.Schedule.Api.Models
{
    public class ShiftDto
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }

    public class ShiftValidator : AbstractValidator<ShiftDto>
    {
        public ShiftValidator()
        {
            RuleFor(e => e.Start)
            .NotNull().WithMessage("Start date is required.")
            .Must(BeValidDateTime).WithMessage("Start date should be a valid date and time.");

            RuleFor(e => e.End)
                .NotNull().WithMessage("End date is required.")
                .Must(BeValidDateTime).WithMessage("End date should be a valid date and time.")
                .GreaterThan(e => e.Start).WithMessage("End date must be greater than Start date.");

            RuleFor(e => e.End.Day)
                .Equal(e => e.Start.Day).WithMessage("Start and end date should be on the same day.");
        }

        private bool BeValidDateTime(DateTime date)
        {
            return date != DateTime.MinValue && date != DateTime.MaxValue;
        }
    }
}
