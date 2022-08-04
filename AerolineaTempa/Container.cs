using System;
namespace AerolineaTempa
{
    public class Container
    {
        public static Container Current { get; } = new Container();
        public IServiceProvider Services { get; set; }

        private Container()
        {
        }
    }
}
