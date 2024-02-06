using PresentationLayer.Models;
using System.Text;

namespace PresentationLayer
{
    public static class SessionExtensions                                   // maybe this class is a business logic layer => in the future
    {
        private const string key = "Cart";
        public static void Set(this ISession session, Cart value)
        {
            if (value == null)
                return;

            using (var stream = new MemoryStream())
            using (var writer = new BinaryWriter(stream, Encoding.UTF8, true))
            {
                writer.Write(value.OrderId);
                writer.Write(value.TotalCount);
                writer.Write(value.TotalPrice);

                session.Set(key, stream.ToArray());  
            }
        }

        public static bool TryGetCart(this ISession session, out Cart value)
        {
            if (session.TryGetValue(key, out byte[]? buffer))
            {
                using (var stream = new MemoryStream(buffer))
                using (var reader = new BinaryReader(stream, Encoding.UTF8, true))
                {
                    var oderId = reader.ReadInt32();
                    var totalCount = reader.ReadInt32();
                    var totalPrice = reader.ReadDecimal();

                    value = new Cart(oderId)
                    {
                        TotalCount = totalCount,
                        TotalPrice = totalPrice
                    };

                    return true;
                }
            }
            value = null;
            return false;
        }
    }
}
