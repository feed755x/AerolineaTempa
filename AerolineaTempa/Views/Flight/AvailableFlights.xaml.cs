using System;
using System.Collections.Generic;
using AerolineaTempa.ViewModels.Flight;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace AerolineaTempa.Views.Flight
{
    public partial class AvailableFlights : ContentPage
    {
        public AvailableFlights()
        {
            InitializeComponent();
            BindingContext = Container.Current.Services.GetRequiredService<AvailableFlightsViewModel>();
        }
    }
}
