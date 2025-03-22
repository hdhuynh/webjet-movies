using MediatR;

namespace Webjet.Domain.Common.Base;

public abstract record DomainEvent : INotification;