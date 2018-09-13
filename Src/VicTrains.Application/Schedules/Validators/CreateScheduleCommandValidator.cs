using FluentValidation;
using VicTrains.Application.Schedules.Commands;

namespace VicTrains.Application.Schedules.Validators
{
    public class CreateScheduleCommandValidator : AbstractValidator<CreateScheduleCommand>
    {
        public CreateScheduleCommandValidator()
        {
            RuleFor(x => x.TrainLineId).NotEmpty();
            RuleFor(x => x.DepartureStation).MaximumLength(40).NotEmpty();
            RuleFor(x => x.DepartureDateTime).NotEmpty();
            RuleFor(x => x.ArrivalStation).MaximumLength(40).NotEmpty();
            RuleFor(x => x.ArrivalDateTime).NotEmpty();
        }
    }
}
