using backend.Dtos;

namespace backend.Services;

public interface ITicketService
{
    IEnumerable<TicketResponse> GetTickets();
    TicketResponse? GetTicket(int id);
    TicketResponse CreateTicket(CreateTicketRequest request);
    bool AssignTicket(int id, int assigneeId);
    (bool Success, string? Error) UpdateStatus(int id, string status);
    string GetServiceStatus();
}