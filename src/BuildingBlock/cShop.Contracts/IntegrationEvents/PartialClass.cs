using MediatR;

namespace IntegrationEvents;

public partial class ProductCreatedIntegrationEvent : INotification;

public partial class CustomerCreatedIntegrationEvent : INotification;
public partial class ShipperCreatedIntegrationEvent : INotification;
public partial class OrderConfirmIntegrationEvent : INotification;