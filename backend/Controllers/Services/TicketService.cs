using backend.Dtos;
using backend.Models;

namespace backend.Services;

public class TicketService : ITicketService
{
    // Временное хранилище в памяти
    private readonly List<Ticket> _tickets = new();
    private int _nextId = 1;

    public TicketService()
    {
        // Добавим одну тестовую заявку для начала
        _tickets.Add(new Ticket 
        { 
            Id = _nextId++, 
            Number = "REQ-2026-001", 
            Title = "Не включается монитор", 
            EquipmentId = 3, 
            Description = "Нет изображения", 
            Status = "New" 
        });
    }

    public IEnumerable<TicketResponse> GetTickets()
    {
        return _tickets.Select(t => MapToResponse(t)).ToList();
    }

    public TicketResponse? GetTicket(int id)
    {
        var ticket = _tickets.FirstOrDefault(t => t.Id == id);
        return ticket == null ? null : MapToResponse(ticket);
    }

    public TicketResponse CreateTicket(CreateTicketRequest request)
    {
        var ticket = new Ticket
        {
            Id = _nextId,
            Number = $"REQ-{DateTime.Now.Year}-{_nextId:D3}",
            Title = request.Title,
            EquipmentId = request.EquipmentId,
            Description = request.Description,
            Status = "New",
            AssigneeUserId = null
        };

        _tickets.Add(ticket);
        _nextId++;
        return MapToResponse(ticket);
    }

    public bool AssignTicket(int id, int assigneeId)
    {
        var ticket = _tickets.FirstOrDefault(t => t.Id == id);
        if (ticket == null) return false;

        ticket.AssigneeUserId = assigneeId;
        return true;
    }

    public (bool Success, string? Error) UpdateStatus(int id, string status)
    {
        var ticket = _tickets.FirstOrDefault(t => t.Id == id);
        if (ticket == null) return (false, "Not Found");

        var allowedStatuses = new[] { "New", "InProgress", "Closed", "Cancelled" };
        if (!allowedStatuses.Contains(status))
        {
            return (false, "Bad Request");
        }

        // Правило: нельзя перевести в работу (InProgress), если нет исполнителя
        if (status == "InProgress" && ticket.AssigneeUserId == null)
        {
            return (false, "Conflict");
        }

        ticket.Status = status;
        return (true, null);
    }

    public string GetServiceStatus() => "Сервис заявок работает в памяти";

    // Вспомогательный метод для преобразования Модели в DTO
    private TicketResponse MapToResponse(Ticket t)
    {
        return new TicketResponse
        {
            Id = t.Id,
            Number = t.Number,
            Title = t.Title,
            EquipmentId = t.EquipmentId,
            Description = t.Description,
            Status = t.Status,
            AssigneeUserId = t.AssigneeUserId
        };
    }
}