using Domain;
using Domain.Contracts;

namespace BusinessLogicLayer.Interfaces
{
    public interface IPaymentService
    {
        string UniqueCode { get; }
        string Name { get; }
        Form CreateForm(Order order);
        Form NextForm(int orderId, int step, IReadOnlyDictionary<string, string> values);
        OrderPayment GetPayment(Form form);
    }
}
