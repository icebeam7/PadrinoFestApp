using SQLite;

namespace PadrinoFestApp.Models
{
    [Table("eventos")]
    public class Evento
    {
        [PrimaryKey, AutoIncrement]
        public int EventoId { get; set; }

        public string NombreDelEvento { get; set; }
        public string FechaDelEvento { get; set; }

        public Evento()
        {

        }

    }
}
