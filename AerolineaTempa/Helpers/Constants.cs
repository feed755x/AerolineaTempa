using System;
namespace AerolineaTempa.Helpers
{
    public static class Constants
    {
        #region URL BASE
        public const string CONTS_URL_BASE_SERVICES = "https://e0e6-2806-2f0-92a1-a7e2-8b7e-b95e-c5d4-d03b.ngrok.io/hydra/api/";
        #endregion

        #region URL Controller
        public const string CONTS_CONTROLLER_ALL_FLIGHTS = "allflights_get";
        public const string CONTS_CONTROLLER_FLIGHTS = "flights_get";
        public const string CONTS_CONTROLLER_TRIPS_GET = "trips_get";

        public const string CONTS_CONTROLLER_UPDATE_FLIGHT = "update_flight/";
        public const string CONTS_CONTROLLER_DELETE_FLIGHT = "delete_flight/";
        public const string CONTS_CONTROLLER_ADD_TRIP = "add_trip";
        public const string CONTS_CONTROLLER_ADD_FLIGHT = "add_flight";
        public const string CONTS_CONTROLLER_DELETE_TRIP = "delete_trip/";
        #endregion
    }
}
