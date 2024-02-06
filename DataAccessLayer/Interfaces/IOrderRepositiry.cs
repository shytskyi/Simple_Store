using Domain;

namespace DataAccessLayer.Interfaces
{
    public interface IOrderRepositiry
    {
        Order Create();
        Order GetById(int id);
        void Update(Order order);
    }
}
