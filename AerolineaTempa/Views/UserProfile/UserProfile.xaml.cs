using System;
using System.Collections.Generic;
using AerolineaTempa.ViewModels.UserProfile;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;

namespace AerolineaTempa.Views.UserProfile
{
    public partial class UserProfile : ContentPage
    {
        public UserProfile()
        {
            InitializeComponent();
            BindingContext = Container.Current.Services.GetRequiredService<UserProfileViewModel>();
        }
    }
}
