using OOPSample.Shared.Domain.Model.ValueObjects;

namespace OOPSample.Sales.Domain.Model.Aggregates;

public class SalesOrder(int customerId)
{
    public Guid Id { get; private set; } = GenerateOrderId();
    public int CustomerId { get; } = customerId;
    public ESalesOrderStatus Status { get; private set; } = ESalesOrderStatus.PendingPayment;
    private Address _shippingAddress;
    public string ShippingAddress => _shippingAddress.AddressAsString;
    public double PaidAmount { get; private set; } = 0;
    private readonly List<SalesOrderItem> _items = [];

    public void AddItem(int productId, int quantity, double unitPrice)
    {
        if (Status != ESalesOrderStatus.PendingPayment)
            throw new InvalidOperationException("Can't modify an order once payment is processed");
        _items.Add(new SalesOrderItem(Id, productId, quantity, unitPrice));
    }

    public void Cancel()
    {
        Status = ESalesOrderStatus.Cancelled;
    }
    
    
    

    private static Guid GenerateOrderId()
    {
        return Guid.NewGuid();
    }
}