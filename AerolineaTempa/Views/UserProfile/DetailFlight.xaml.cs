using System;
using System.Collections.Generic;
using AerolineaTempa.ViewModels.UserProfile;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace AerolineaTempa.Views.UserProfile
{
    public partial class DetailFlight : ContentPage
    {
        public DetailFlight()
        {
            InitializeComponent();
            BindingContext = Container.Current.Services.GetRequiredService<DetailFlightViewModel>();
        }
    }
}
