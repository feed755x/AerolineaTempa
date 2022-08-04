using System;
using System.Collections.Generic;
using AerolineaTempa.ViewModels.Home;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace AerolineaTempa.Views.Home
{
    public partial class HomeTempa : ContentPage
    {
        public HomeTempa()
        {
            InitializeComponent();
            BindingContext = Container.Current.Services.GetRequiredService<HomeTempaViewModel>();
        }
    }
}
