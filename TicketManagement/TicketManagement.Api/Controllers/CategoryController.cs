using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicketManagement.Application.Features.Categories.Commands.CreateCategory;
using TicketManagement.Application.Features.Categories.Queries.GetCategoriesList;
using TicketManagement.Application.Features.Categories.Queries.GetCategoriesListWithEvents;

namespace TicketManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpGet("all", Name = "GetAllCategories")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CategoryListVm>>> GetAllCategories()
    {
        var dtos = await _mediator.Send(new GetCategoriesListQuery());

        return Ok(dtos);
    }

    [HttpGet("allwithevents", Name = "GetCategoriesWithEvents")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CategoryEventListVm>>> GetCategoriesWithEvents(bool includeHistory)
    {
        GetCategoriesListWithEventsQuery getCategoriesListWithEventsQuery = new GetCategoriesListWithEventsQuery() { IncludeHistory = includeHistory };

        var dtos = await _mediator.Send(getCategoriesListWithEventsQuery);
        return Ok(dtos);
    }

    [HttpPost(Name = "AddCategory")]
    public async Task<ActionResult<CreateCategoryCommandResponse>> Create([FromBody] CreateCategoryCommand createCategoryCommand)
    {
        var response = await _mediator.Send(createCategoryCommand);
        return Ok(response);
    }
}