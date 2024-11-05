using MediatR;

namespace IntegrationEvents;

public partial class ProductCreated : INotification;
public partial class OrderComplete : INotification;