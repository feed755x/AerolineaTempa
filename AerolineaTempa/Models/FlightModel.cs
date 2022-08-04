﻿using System;
using AerolineaTempa.ViewModels.Base;

namespace AerolineaTempa.Models
{
    public class FlightModel : BaseViewModel
    {
        public string id { get; set; }
        public string icon { get; set; }
        public string aerolinea { get; set; }
        public string origen { get; set; }
        public string destino { get; set; }
        public string fechaSalida { get; set; }
        public string fechaLlegada { get; set; }
        public int asientosDisponibles { get; set; }
        public double precioAsiento { get; set; }
    }
}