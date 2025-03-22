namespace Webjet.Backend.Customers.EventHandlers;

public record MessageDto(string From, string To, string Subject, string Body);