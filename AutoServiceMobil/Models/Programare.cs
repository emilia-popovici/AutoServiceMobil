using SQLite;

namespace AutoServiceMobil.Models
{
    public class Programare
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string ServiceName { get; set; }

        public string MechanicName { get; set; }

        public DateTime Date { get; set; }

        public bool IsCompleted { get; set; }

        public bool HasReview { get; set; }
    }
}