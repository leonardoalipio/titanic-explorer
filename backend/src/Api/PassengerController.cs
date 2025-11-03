using System.Linq.Expressions;
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
                form.ESex = ParseSex(form.Sex);
                
                return FilterPassengers(passengers, form);
            })
            .WithName("PostPassengers")
            .WithOpenApi();
        }

        private static IEnumerable<Passenger> FilterPassengers(IEnumerable<Passenger> passengers, Search search)
        {
            Expression? currentExpression = null;
            var passengerParameter = Expression.Parameter(typeof(Passenger));

            if (search.Survived != null)
            {
                var survivedValue = Expression.Constant(search.Survived.Value);
                var passengerSurvived = Expression.Property(passengerParameter, "Survived");
                currentExpression = Expression.Equal(passengerSurvived, survivedValue);
            }

            if (search.Class != null)
            {
                var PclassValue = Expression.Constant(search.Class.Value);
                var passengerPclass = Expression.Property(passengerParameter, "Pclass");
                var PclassEquals = Expression.Equal(passengerPclass, PclassValue);
                if (currentExpression == null)
                    currentExpression = PclassEquals;
                else
                    currentExpression = Expression.And(currentExpression, PclassEquals);
            }

            if (search.ESex != null)
            {
                var sexValue = Expression.Constant(search.ESex.Value);
                var passengerSex = Expression.Property(passengerParameter, "Sex");
                var sexEquals = Expression.Equal(passengerSex, sexValue);
                if (currentExpression == null)
                    currentExpression = sexEquals;
                else
                    currentExpression = Expression.And(currentExpression, sexEquals);
            }

            if (search.Age != null)
            {
                var ageValue = Expression.Constant(search.Age.Value);
                var passengerAge = Expression.Property(passengerParameter, "Age");
                var ageEquals = Expression.Equal(passengerAge, ageValue);
                if (currentExpression == null)
                    currentExpression = ageEquals;
                else
                    currentExpression = Expression.And(currentExpression, ageEquals);
            }

            if (search.Fare != null)
            {
                var fareValue = Expression.Constant(search.Fare.Value);
                var passengerFare = Expression.Property(passengerParameter, "Fare");
                var fareEquals = Expression.Equal(passengerFare, fareValue);
                if (currentExpression == null)
                    currentExpression = fareEquals;
                else
                    currentExpression = Expression.And(currentExpression, fareEquals);
            }

            if (currentExpression != null)
            {
                var expression = Expression.Lambda<Func<Passenger, bool>>(
                    currentExpression, false, new List<ParameterExpression> { passengerParameter });
                var func = expression.Compile();
                passengers = passengers.Where(func);
            }

            return passengers;
        }

        public static ESex? ParseSex(int? value)
        {
            return value switch
            {
                0 => ESex.Male,
                1 => ESex.Female,
                _ => null,
            };
        }
    }
}