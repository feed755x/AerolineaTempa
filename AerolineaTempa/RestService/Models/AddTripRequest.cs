using System;
namespace AerolineaTempa.RestService.Models
{
    public class AddTripRequest
    {
        public int idPasajero { get; set; }
        public int idVuelo { get; set; }
        public int asientosComprados { get; set; }
        public int precioTotal { get; set; }
        public int isCanceled { get; set; }
    }
}
