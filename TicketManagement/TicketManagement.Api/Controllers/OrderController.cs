using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TicketManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
}
