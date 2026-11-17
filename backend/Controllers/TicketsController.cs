using Microsoft.AspNetCore.Mvc;
using backend.Dtos;
using backend.Services;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;
    private readonly ILogger<TicketsController> _logger;

    // Внедрение зависимостей (DI) через конструктор
    public TicketsController(ITicketService ticketService, ILogger<TicketsController> logger)
    {
        _ticketService = ticketService;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult<IEnumerable<TicketResponse>> GetTickets()
    {
        return Ok(_ticketService.GetTickets());
    }

    [HttpGet("{id}")]
    public ActionResult<TicketResponse> GetTicket(int id)
    {
        var ticket = _ticketService.GetTicket(id);
        if (ticket == null) return NotFound();
        return Ok(ticket);
    }

    [HttpPost]
    public ActionResult<TicketResponse> CreateTicket([FromBody] CreateTicketRequest request)
    {
        try
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(request.Title) || request.Title.Length < 5 || request.Title.Length > 80)
                return BadRequest(new { message = "Тема должна быть от 5 до 80 символов" });

            if (request.Description.Length < 10 || request.Description.Length > 500)
                return BadRequest(new { message = "Описание должно быть от 10 до 500 символов" });

            if (request.EquipmentId <= 0)
                return BadRequest(new { message = "ID оборудования должен быть больше 0" });

            // Временный сбой для проверки лога (из методички)
            // throw new InvalidOperationException("Учебный сбой");

            var created = _ticketService.CreateTicket(request);
            return CreatedAtAction(nameof(GetTicket), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            // Безопасный лог
            _logger.LogError(ex, "CreateTicket failed; equipmentId={EquipmentId}; error={ErrorType}", 
                request.EquipmentId, ex.GetType().Name);
            
            // Безопасный ответ клиенту (без stack trace)
            return StatusCode(500, new { message = "Внутренняя ошибка сервера. Попробуйте позже." });
        }
    }

    [HttpPatch("{id}/assignee")]
    public IActionResult AssignTicket(int id, [FromBody] UpdateAssigneeRequest request)
    {
        if (request.AssigneeUserId <= 0) return BadRequest(new { message = "Некорректный ID исполнителя" });

        var success = _ticketService.AssignTicket(id, request.AssigneeUserId);
        if (!success) return NotFound();

        return Ok(new { message = "Исполнитель назначен" });
    }

    [HttpPatch("{id}/status")]
    public IActionResult UpdateStatus(int id, [FromBody] UpdateStatusRequest request)
    {
        var (success, error) = _ticketService.UpdateStatus(id, request.Status);

        if (!success)
        {
            if (error == "Not Found") return NotFound();
            if (error == "Bad Request") return BadRequest(new { message = "Неверный статус" });
            if (error == "Conflict") return Conflict(new { message = "Нельзя перевести в работу без исполнителя" });
        }

        return Ok(new { message = "Статус обновлен" });
    }
}