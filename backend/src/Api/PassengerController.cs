using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TitanicExplorer.Api
{
    public static class Passenger
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
    }
    }
}