using System;
using System.Collections.Generic;
using AerolineaTempa.ViewModels.Flight;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace AerolineaTempa.Views.Flight
{
    public partial class BuyFlight : ContentPage
    {
        public BuyFlight()
        {
            InitializeComponent();
            BindingContext = Container.Current.Services.GetRequiredService<BuyFlightViewModel>();
        }
    }
}
