using SQLite;

namespace AutoServiceMobil.Models;

public class Mecanic
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public string Nume { get; set; }
    public string Prenume { get; set; }
    public double Rating { get; set; }
    public string PozaUrl { get; set; }

    public string NumeComplet => $"{Nume} {Prenume}";
    public string RatingText => $"Rating: {Rating}/5";
}