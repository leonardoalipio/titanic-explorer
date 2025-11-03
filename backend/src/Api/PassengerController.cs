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
                currentExpression = CreateExpression(
                    search.Survived.Value,
                    currentExpression,
                    "Survived",
                    passengerParameter
                );
            }

            if (search.Class != null)
            {
                currentExpression = CreateExpression(
                    search.Class.Value,
                    currentExpression,
                    "Pclass",
                    passengerParameter
                );
            }

            if (search.ESex != null)
            {
                currentExpression = CreateExpression(
                    search.ESex.Value,
                    currentExpression,
                    "Sex",
                    passengerParameter
                );
            }

            if (search.Age != null)
            {
                currentExpression = CreateExpression(
                    search.Age.Value,
                    currentExpression,
                    "Age",
                    passengerParameter
                );
            }

            if (search.Fare != null)
            {
                currentExpression = CreateExpression(
                    search.Fare.Value,
                    currentExpression,
                    "Fare",
                    passengerParameter,
                    ">"
                );
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

        private static Expression CreateExpression<T>(
            T value,
            Expression? currentExpression,
            string propertyName,
            ParameterExpression objectParameter,
            string operatorType = "=")
        {
            var constant = Expression.Constant(value);
            var property = Expression.Property(objectParameter, propertyName);
            Expression operatorExpression;
            switch (operatorType)
            {
                case ">":
                    operatorExpression = Expression.GreaterThan(property, constant);
                    break;
                case "<":
                    operatorExpression = Expression.LessThan(property, constant);
                    break;
                case ">=":
                    operatorExpression = Expression.GreaterThanOrEqual(property, constant);
                    break;
                case "<=":
                    operatorExpression = Expression.LessThanOrEqual(property, constant);
                    break;
                default:
                    operatorExpression = Expression.Equal(property, constant);
                    break;
            }

            if (currentExpression == null)
                currentExpression = operatorExpression;
            else
                currentExpression = Expression.And(currentExpression, operatorExpression);

            return currentExpression;
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