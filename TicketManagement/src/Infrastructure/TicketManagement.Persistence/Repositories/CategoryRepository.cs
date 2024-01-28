using Microsoft.EntityFrameworkCore;
using TicketManagement.Application.Contracts.Persistence;
using TicketManagement.Domain.Entities;

namespace TicketManagement.Persistence.Repositories;

public class CategoryRepository(GloboTicketDbContext dbContext) : BaseRepository<Category>(dbContext), ICategoryRepository
{

    public async Task<List<Category>> GetCategoriesWithEvents(bool includePassedEvents)
    {
        List<Category>? allCategories = await _dbContext.Categories.Include(x => x.Events).ToListAsync();

        if (!includePassedEvents)
        {
            allCategories.ForEach(p => p.Events.ToList().RemoveAll(c => c.Date < DateTime.Today));
        }

        return allCategories;
    }

}
