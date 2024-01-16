using AutoMapper;
using TicketManagement.Application.Features.Categories.Commands.CreateCateogry;
using TicketManagement.Application.Features.Categories.Queries.GetCategoriesList;
using TicketManagement.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using TicketManagement.Application.Features.Events.Commands.CreateEvent;
using TicketManagement.Application.Features.Events.Commands.UpdateEvent;
using TicketManagement.Application.Features.Events.Queries.GetEventDetail;
using TicketManagement.Application.Features.Events.Queries.GetEventsList;
using TicketManagement.Domain.Entities;

namespace TicketManagement.Application.Profiles
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            _ = CreateMap<Event, EventListVm>().ReverseMap();
            _ = CreateMap<Event, EventDetailVm>().ReverseMap();
            _ = CreateMap<Category, CategoryDto>();
            _ = CreateMap<Category, CategoryListVm>();
            _ = CreateMap<Category, CategoryEventListVm>();

            _ = CreateMap<Event, CreateEventCommand>().ReverseMap();
            _ = CreateMap<Event, UpdateEventCommand>().ReverseMap();
            _ = CreateMap<Event, CategoryEventDto>().ReverseMap();

            _ = CreateMap<Category, CreateCategoryCommand>().ReverseMap();
            _ = CreateMap<Category, CreateCategoryDto>().ReverseMap();
        }

    }
}
