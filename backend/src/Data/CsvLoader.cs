using System.Globalization;
using TitanicExplorer.Business;

namespace TitanicExplorer.Data;

public class CsvLoader
{
    public static List<Passenger> LoadPassengers(string filePath)
    {
        var passengers = new List<Passenger>();
        var lines = File.ReadAllLines(filePath);

        for (int i = 1; i < lines.Length; i++) // ignorar cabeçalho
        {
            var fields = lines[i].Split(';');

            var passenger = new Passenger
            {
                Pclass = int.Parse(fields[0]),
                Survived = fields[1] == "1",
                Name = fields[2],
                Sex = fields[3] == "male" ? ESex.Male : ESex.Female,
                Age = int.TryParse(fields[4], out var ageVal) ? ageVal : 0,
                SibSp = int.Parse(fields[5]),
                Parch = int.Parse(fields[6]),
                Ticket = fields[7],
                Fare = decimal.TryParse(fields[8].Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out var fareVal) ? fareVal : 0,
                Cabin = fields[9],
                Embarked = fields[10],
                Boat = fields[11],
                Body = fields[12],
                HomeDest = fields[13]
            };

            passengers.Add(passenger);
        }

        return passengers;
    }
}