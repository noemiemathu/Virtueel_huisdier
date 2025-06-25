namespace Virtueel_Huisdier.Models;

public class Game
{
    public int id { get; set; }
    public string name { get; set; }
    public string dier { get; set; }
    public string gif { get; set; }
    public Status status { get; set; }

    public Game()
    {
        status = new Status()
        {
            gezondheid = 100,
            eten = 80,
            geluk = 50,
            energie = 60
        };
    }
}

public class Status
{
    public int gezondheid { get; set; }
    public int eten { get; set; }
    public int geluk { get; set; }
    public int energie { get; set; }
}