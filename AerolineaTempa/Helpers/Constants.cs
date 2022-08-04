using System;
namespace AerolineaTempa.Helpers
{
    public static class Constants
    {
        #region URL BASE
        public const string CONTS_URL_BASE_SERVICES = "http://34.170.16.83/hydra/api/";
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
