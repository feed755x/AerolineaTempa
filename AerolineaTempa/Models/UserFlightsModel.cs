using System;
namespace AerolineaTempa.Models
{
    public class UserFlightsModel
    {
        public string id { get; set; }
        public string icon { get; set; }
        public string aerolinea { get; set; }
        public string origen { get; set; }
        public string destino { get; set; }
        public string fechaSalida { get; set; }
        public string fechaLlegada { get; set; }
        public int asientosComprados { get; set; }
        public double precioTotal { get; set; }
    }
}
