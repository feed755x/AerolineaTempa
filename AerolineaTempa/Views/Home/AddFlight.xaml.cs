using System;
using System.Collections.Generic;
using AerolineaTempa.ViewModels.Home;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace AerolineaTempa.Views.Home
{
    public partial class AddFlight : ContentPage
    {
        public AddFlight()
        {
            InitializeComponent();
            BindingContext = Container.Current.Services.GetRequiredService<AddFlightViewModel>();
        }
    }
}
