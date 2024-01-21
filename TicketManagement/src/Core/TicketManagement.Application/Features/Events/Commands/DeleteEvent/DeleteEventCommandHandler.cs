using AutoMapper;
using MediatR;
using TicketManagement.Application.Contracts.Persistence;
using TicketManagement.Domain.Entities;

namespace TicketManagement.Application.Features.Events.Commands.DeleteEvent;

public class DeleteEventCommandHandler(IAsyncRepository<Event> eventRepository, IMapper mapper) : IRequestHandler<DeleteEventCommand>
{
    private readonly IAsyncRepository<Event> _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var eventToDelete = await _eventRepository.GetByIdAsync(request.EventId);

        await _eventRepository.DeleteAsync(eventToDelete);
    }
}
