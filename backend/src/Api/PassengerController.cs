using Microsoft.AspNetCore.Mvc;
using TitanicExplorer.Business;

namespace TitanicExplorer.Api
{
    public static class PassengerController
    {
        public static void MapPassenger(this WebApplication app)
        {
            app.MapGet("/passengers", () =>
            {
                var passengers = Data.CsvLoader.LoadPassengers("../Data/titanic-passenger-list.csv");
                return passengers;
            })
            .WithName("GetPassengers")
            .WithOpenApi();

            app.MapPost("/search-passengers", ([FromBody] Search form) =>
            {
                var passengers = Data.CsvLoader.LoadPassengers("../Data/titanic-passenger-list.csv");

                return FilterPassengers(passengers, form);
            })
            .WithName("PostPassengers")
            .WithOpenApi();
        }

        private static IEnumerable<Passenger> FilterPassengers(IEnumerable<Passenger> passengers, Search search)
        {
            IEnumerable<Passenger> filteredPassengers = passengers;

            if (search.Survived != null)
            {
                filteredPassengers = filteredPassengers.Where(p => p.Survived == search.Survived);
            }

            if (search.Class != null)
            {
                filteredPassengers = filteredPassengers.Where(p => p.Pclass == search.Class);
            }

            if (search.Sex != null)
            {
                filteredPassengers = filteredPassengers.Where(p => (int)p.Sex == search.Sex);
            }

            if (search.Age != null)
            {
                filteredPassengers = filteredPassengers.Where(p => p.Age == search.Age);
            }

            if (search.Fare != null)
            {
                filteredPassengers = filteredPassengers.Where(p => p.Fare >= search.Fare);
            }

            return filteredPassengers;
        }
    }
}