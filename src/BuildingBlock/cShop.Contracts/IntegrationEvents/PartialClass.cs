using MediatR;

namespace IntegrationEvents;

public partial class ProductCreated : INotification;
public partial class OrderComplete : INotification;

public partial class CustomerCreatedIntegrationEvent : INotification;
public partial class ShipperCreatedIntegrationEvent : INotification;