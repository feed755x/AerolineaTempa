using System;
using System.Threading.Tasks;

namespace AerolineaTempa.Interfaces
{
    public interface IMessageService
    {
        Task Alert(string msg, string title, string aceptText);
    }
}
