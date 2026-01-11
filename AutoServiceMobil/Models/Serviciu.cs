using SQLite;

namespace AutoServiceMobil.Models
{
    public class Serviciu
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Nume { get; set; } = string.Empty;

        [MaxLength(300)]
        public string Descriere { get; set; } = string.Empty;

        [MaxLength(50)]
        public string Durata { get; set; } = string.Empty;

        public decimal Pret { get; set; }
    }
}