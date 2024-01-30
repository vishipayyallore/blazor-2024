using AutoMapper;
using FluentValidation.Results;
using MediatR;
using TicketManagement.Application.Contracts.Infrastructure;
using TicketManagement.Application.Contracts.Persistence;
using TicketManagement.Application.Models.Mail;
using TicketManagement.Domain.Entities;

namespace TicketManagement.Application.Features.Events.Commands.CreateEvent;

public class CreateEventCommandHandler(IEventRepository eventRepository, IMapper mapper, IEmailService emailService) : IRequestHandler<CreateEventCommand, Guid>
{
    private readonly IEventRepository _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    private readonly IEmailService _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));

    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        Event? @event = _mapper.Map<Event>(request);

        CreateEventCommandValidator validator = new(_eventRepository);
        ValidationResult? validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count > 0)
        {
            throw new Exceptions.ValidationException(validationResult);
        }

        @event = await _eventRepository.AddAsync(@event);

        //Sending email notification to admin address
        Email? email = new() { To = "john@example.com", Body = $"A new event was created: {request}", Subject = "A new event was created" };

        try
        {
            await _emailService.SendEmail(email);
        }
        catch (Exception ex)
        {
            //this shouldn't stop the API from doing else so this can be logged
            Console.WriteLine($"Mailing about event {@event.EventId} failed due to an error with the mail service: {ex.Message}");
        }

        return @event.EventId;
    }
}
