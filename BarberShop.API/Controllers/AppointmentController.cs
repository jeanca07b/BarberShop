using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly IMediator _mediator;

    public AppointmentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("barber/{barberId}")]
    public async Task<IActionResult> GetByBarber(
    Guid barberId,
    [FromQuery] DateTime date)
    {
        var result = await _mediator.Send(
            new GetAppointmentsByBarberAndDateQuery(barberId, date));

        return Ok(result);
    }

    [Authorize(Roles = "Client")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateAppointmentCommand command)
    {
        var result = await _mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(result.Value);
    }

    [HttpGet("availability/{barberId}")]
    public async Task<IActionResult> GetAvailability(
    Guid barberId,
    [FromQuery] DateTime date,
    [FromQuery] int slotDuration = 30)
    {
        var result = await _mediator.Send(
            new GetBarberAvailabilityQuery(
                barberId,
                date,
                slotDuration));

        return Ok(result);
    }
}