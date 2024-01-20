using AutoMapper;
using FluentValidation.Results;
using MediatR;
using TicketManagement.Application.Contracts.Persistence;
using TicketManagement.Domain.Entities;

namespace TicketManagement.Application.Features.Events.Commands.CreateEvent;

public class CreateEventCommandHandler(IEventRepository eventRepository, IMapper mapper) : IRequestHandler<CreateEventCommand, Guid>
{
    private readonly IEventRepository _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

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

        return @event.EventId;
    }
}
