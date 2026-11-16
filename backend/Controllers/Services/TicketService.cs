namespace backend.Services;
public class TicketService : ITicketService
{
    public string GetServiceStatus() => "Сервис заявок готов к работе (заготовка)";
}