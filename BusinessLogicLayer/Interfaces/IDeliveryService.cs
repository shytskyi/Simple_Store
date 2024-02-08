using Domain;
using Domain.Contracts;

namespace BusinessLogicLayer.Interfaces
{
    public interface IDeliveryService
    {
        string UniqueCode { get; }
        string Name { get; }
        Form CreateForm(Order order);
        Form MoveNext(int orderId, int step, IReadOnlyDictionary<string, string> values);

    }
}
