namespace TitanicExplorer.Business;

public class Passenger
{
    public int Pclass { get; set; }
    public bool Survived { get; set; }
    public string Name { get; set; } = string.Empty;
    public ESex Sex { get; set; }
    public int Age { get; set; }
    public int SibSp { get; set; }
    public int Parch { get; set; }
    public string Ticket { get; set; } = string.Empty;
    public decimal Fare { get; set; }
    public string Cabin { get; set; } = string.Empty;
    public string Embarked { get; set; } = string.Empty;
    public string Boat { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string HomeDest { get; set; } = string.Empty;
}