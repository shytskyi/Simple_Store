using BusinessLogicLayer.Interfaces;
using Domain;
using Domain.Contracts;
using Domain.Contracts.Fields;

namespace BusinessLogicLayer
{
    public class CashPaymentService : IPaymentService
    {
        public string UniqueCode => "Cash";

        public string Name => "Cash payment";

        public Form CreateForm(Order order)
        {
            return new Form(UniqueCode, order.Id, 1, false, new Field[0]);
        }

        public OrderPayment GetPayment(Form form)
        {
            if (form.UniqueCode != UniqueCode || !form.IsFinal)
                throw new InvalidOperationException("Invalid form payment");

            return new OrderPayment(UniqueCode, "Cash payment", new Dictionary<string,string>());
        }

        public Form NextForm(int orderId, int step, IReadOnlyDictionary<string, string> values)
        {
            if (step != 1)
                throw new InvalidOperationException("Invalid cash step");

            return new Form(UniqueCode, orderId, 2, true, new Field[0]);
        }
    }
}
