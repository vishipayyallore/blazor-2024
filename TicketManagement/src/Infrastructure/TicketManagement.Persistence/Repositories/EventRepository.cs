﻿using TicketManagement.Application.Contracts.Persistence;
using TicketManagement.Domain.Entities;

namespace TicketManagement.Persistence.Repositories;


public class EventRepository(TicketManagementDbContext dbContext) : BaseRepository<Event>(dbContext), IEventRepository
{
    public Task<bool> IsEventNameAndDateUnique(string name, DateTime eventDate)
    {
        bool matches = _dbContext.Events.Any(e => e.Name.Equals(name) && e.Date.Date.Equals(eventDate.Date));

        return Task.FromResult(matches);
    }
}
