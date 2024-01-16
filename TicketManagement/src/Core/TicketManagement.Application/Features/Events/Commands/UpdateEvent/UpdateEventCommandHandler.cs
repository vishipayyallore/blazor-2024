using AutoMapper;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;
using TicketManagement.Application.Contracts.Persistence;

namespace TicketManagement.Application.Features.Events.Commands.UpdateEvent
{
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IMapper _mapper;

        public UpdateEventCommandHandler(IMapper mapper, IAsyncRepository<Event> eventRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        public async Task Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {

            var eventToUpdate = await _eventRepository.GetByIdAsync(request.EventId);

            _mapper.Map(request, eventToUpdate, typeof(UpdateEventCommand), typeof(Event));

            await _eventRepository.UpdateAsync(eventToUpdate);
        }
    }
}