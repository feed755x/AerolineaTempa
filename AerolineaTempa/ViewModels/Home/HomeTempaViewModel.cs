using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using AerolineaTempa.Interfaces;
using AerolineaTempa.Models;
using AerolineaTempa.ViewModels.Base;
using Microsoft.Extensions.DependencyInjection;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace AerolineaTempa.ViewModels.Home
{
    public class HomeTempaViewModel : BaseViewModel
    {

        public HomeTempaViewModel(INavigationService navigationService)
        {
            _navegationService = navigationService;
            ListFlights.Clear();
            ListFlights.Add(new FlightModel { icon = "ic_flight_world.png", aerolinea = "Volaris", origen = "CDMX", destino = "Acapulco", fechaSalida = "11:15 21 Ago", fechaLlegada = "03:30 22 Ago", asientosDisponibles = 32 });
            ListFlights.Add(new FlightModel { icon = "ic_flight_world.png", aerolinea = "Volaris", origen = "CDMX", destino = "Acapulco", fechaSalida = "11:15 21 Ago", fechaLlegada = "03:30 22 Ago", asientosDisponibles = 32 });
            ListFlights.Add(new FlightModel { icon = "ic_flight_world.png", aerolinea = "Volaris", origen = "CDMX", destino = "Acapulco", fechaSalida = "11:15 21 Ago", fechaLlegada = "03:30 22 Ago", asientosDisponibles = 32 });
            ListFlights.Add(new FlightModel { icon = "ic_flight_world.png", aerolinea = "Volaris", origen = "CDMX", destino = "Acapulco", fechaSalida = "11:15 21 Ago", fechaLlegada = "03:30 22 Ago", asientosDisponibles = 32 });
            ListFlights.Add(new FlightModel { icon = "ic_flight_world.png", aerolinea = "Volaris", origen = "CDMX", destino = "Acapulco", fechaSalida = "11:15 21 Ago", fechaLlegada = "03:30 22 Ago", asientosDisponibles = 0 });
        }

        #region Services
        private INavigationService _navegationService;
        #endregion

        #region Properties
        private ObservableCollection<FlightModel> _listFlights = new ObservableCollection<FlightModel>();

        public ObservableCollection<FlightModel> ListFlights
        {
            get => _listFlights;
            set
            {
                _listFlights = value;
                OnPropertyChanged();
            }
        }

        private FlightModel _selectedFlight;

        public FlightModel SelectedFlight
        {
            get => _selectedFlight;
            set
            {
                _selectedFlight = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public ICommand AddItemCommand
        {
            get
            {
                return new AsyncCommand(AddItem);
            }
        }

        public ICommand EditItemCommand
        {
            get
            {
                return new AsyncCommand(EditItem);
            }
        }

        public ICommand DeleteItemCommand
        {
            get
            {
                return new AsyncCommand(DeleteItem);
            }
        }
        #endregion

        #region Methods
        public async Task AddItem()
        {
            var vmBuy = Container.Current.Services.GetRequiredService<AddFlightViewModel>();

            vmBuy.Aerolinea = "";
            vmBuy.Origen = "";
            vmBuy.FechaSalida = "";
            vmBuy.HoraSalida = "";
            vmBuy.FechaLlegada = "";
            vmBuy.HoraLlegada = "";
            vmBuy.Destino = "";
            vmBuy.AsientosDisponibles = 0;
            vmBuy.PrecioAsiento = 0;

            await _navegationService.NavigateModalAsync("AddFlight");
        }

        public async Task EditItem()
        {
            if (SelectedFlight != null)
            {
                var vmBuy = Container.Current.Services.GetRequiredService<EditFlightViewModel>();

                vmBuy.Aerolinea = SelectedFlight.aerolinea;
                vmBuy.Origen = SelectedFlight.origen;
                vmBuy.FechaSalida = SelectedFlight.fechaSalida;
                vmBuy.FechaLlegada = SelectedFlight.fechaLlegada;
                vmBuy.Destino = SelectedFlight.destino;
                vmBuy.AsientosDisponibles = SelectedFlight.asientosDisponibles;
                vmBuy.PrecioAsiento = SelectedFlight.precioAsiento;

                await _navegationService.NavigateModalAsync("EditFlight");
                SelectedFlight = null;
            }
        }

        public async Task DeleteItem()
        {

        }
        #endregion
    }
}
