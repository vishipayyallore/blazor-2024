using AutoMapper;
using MediatR;
using TicketManagement.Application.Contracts.Persistence;
using TicketManagement.Domain.Entities;

namespace TicketManagement.Application.Features.Events.Queries.GetEventsList;

public class GetEventsListQueryHandler(IAsyncRepository<Event> eventRepository, IMapper mapper) : IRequestHandler<GetEventsListQuery, List<EventListVm>>
{
    private readonly IAsyncRepository<Event> _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<List<EventListVm>> Handle(GetEventsListQuery request, CancellationToken cancellationToken)
    {
        IOrderedEnumerable<Event>? allEvents = (await _eventRepository.ListAllAsync()).OrderBy(x => x.Date);

        return _mapper.Map<List<EventListVm>>(allEvents);
    }
}
