using DataAccessLayer.Interfaces;
using Domain;

namespace DataAccessLayer.Repository
{
    public class OrderRepository : IOrderRepositiry
    {
        private readonly List<Order> orders = new List<Order>();
        public Order Create()
        {
            int nextId = orders.Count + 1;
            var order = new Order(nextId, new OrderItem[0]);

            orders.Add(order);
            return order;
        }

        public Order GetById(int id)
        {
            return orders.Single(order => order.Id == id);
        }

        public void Update(Order order)
        {
            ;
        }
    }
}
