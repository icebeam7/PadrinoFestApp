using SQLite;

namespace PadrinoFestApp.Models
{
    [Table("speakers")]
    public class Speaker
    {
        [PrimaryKey, AutoIncrement]
        public int SpeakerId { get; set; }
        public string NombreDelSpeaker { get; set; }
        public int EventoId { get; set; }

        public Speaker()
        {

        }
    }
}
