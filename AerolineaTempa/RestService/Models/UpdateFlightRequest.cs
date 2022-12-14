using System;
namespace AerolineaTempa.RestService.Models
{
    public class UpdateFlightRequest
    {
        public string aerolinea { get; set; }
        public string origen { get; set; }
        public string destino { get; set; }
        public string fechaSalida { get; set; }
        public string fechaLlegada { get; set; }
        public string horaSalida { get; set; }
        public string horaLlegada { get; set; }
        public int asientosDisponibles { get; set; }
        public double precioAsiento { get; set; }
    }
}
