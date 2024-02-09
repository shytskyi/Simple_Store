using BusinessLogicLayer.Interfaces;
using Domain;
using Domain.Contracts;
using Domain.Contracts.Fields;

namespace BusinessLogicLayer
{
    public class PostamateDeliveryService : IDeliveryService
    {
        private static IReadOnlyDictionary<string, string> cities = new Dictionary<string, string>
        {
            {"1", "Wroclaw"},
            {"2", "Krakow"}
        };
        private static IReadOnlyDictionary<string, IReadOnlyDictionary<string, string>> postamates = new Dictionary<string, IReadOnlyDictionary<string, string>>
        {
            {
                "1",
                new Dictionary<string, string>
                {
                    {"1", "Grabiszynska"},
                    {"2", "Pochyla"},
                    {"3", "Wysoka"}
                }
            },
            {
                "2",
                new Dictionary<string, string>
                {
                    {"1", "Plac Legionow"},
                    {"2", "Lwowska"},
                    {"3", "Zdrowa"}
                }
            }
        };

        public string UniqueCode => "Postamate";

        public string Name => "using Postamate";

        public OrderDelivery GetDelivery(Form form)
        {
            if (form.UniqueCode != UniqueCode || !form.IsFinal)
                throw new InvalidOperationException("Invalid form");

            var cityId = form.Fields.Single(field => field.Name == "city").Value;
            var cityName = cities[cityId];
            var postamateId = form.Fields.Single(field => field.Name == "postamate").Value;
            var postamateName = postamates[cityId][postamateId];
            var parameters = new Dictionary<string, string>
            {
                {nameof(cityId), cityId},
                {nameof(cityName), cityName},
                {nameof(postamateId), postamateId},
                {nameof(postamateName), postamateName}
            };

            var description = $"City: {cityName}\nPostamate: {postamateName}";

            return new OrderDelivery(UniqueCode, description, 5m, parameters);
        }

        public Form CreateForm(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));
            return new Form(UniqueCode, order.Id, 1, false, new[]
            {
                new SelectionField("City", "city", "1", cities),

            });
        }

        public Form NextForm(int orderId, int step, IReadOnlyDictionary<string, string> values)
        {
            {
                if (step == 1)
                {
                    if (values["city"] == "1")
                    {
                        return new Form(UniqueCode, orderId, 2, false, new Field[]
                        {
                            new HiddenField("City", "city", "1"),
                            new SelectionField("Postamate", "postamate", "1", postamates["1"])
                        });
                    }
                    else if (values["city"] == "2")
                    {
                        return new Form(UniqueCode, orderId, 2, false, new Field[]
                        {
                            new HiddenField("City", "city", "2"),
                            new SelectionField("Postamate", "postamate", "4", postamates["2"])
                        });
                    }
                    else
                        throw new InvalidOperationException("Invalid postamate city.");
                }
                else if (step == 2)
                {
                    return new Form(UniqueCode, orderId, 3, true, new Field[]
                        {
                            new HiddenField("City", "city", values["city"]),
                            new HiddenField("Postamate", "postamate", values["postamate"])
                        });
                }
                else
                    throw new InvalidOperationException("Invalid postamate step.");
            }
        }
    }
}
