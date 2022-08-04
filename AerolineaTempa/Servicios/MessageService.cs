using System;
using System.Threading.Tasks;
using AerolineaTempa.Interfaces;
using Xamarin.Forms;

namespace AerolineaTempa.Servicios
{
    public class MessageService : IMessageService
    {
        public async Task Alert(string msg, string title, string aceptText)
        {
            if (Application.Current.MainPage is Page page)
            {
                await page.DisplayAlert(title, msg, aceptText);
            }

            await Task.CompletedTask;
        }
    }
}
