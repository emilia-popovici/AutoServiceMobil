using SQLite;

namespace AutoServiceMobil.Models
{
    public class Review
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int UserId { get; set; }
        public int MecanicId { get; set; }

        public int Rating { get; set; } // 1-5

        [MaxLength(300)]
        public string Comentariu { get; set; } = string.Empty;
    }
}