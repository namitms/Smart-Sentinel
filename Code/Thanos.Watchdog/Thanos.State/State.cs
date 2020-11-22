using Thanos.Models;

namespace Thanos.WatchDog.State
{
    public class State
    {
        private static State onlyObject;
        private State()
        {
            EdgeConfiguration = null;
        }

        public static State Instance
        {
            get
            {
                if (onlyObject == null)
                {
                    onlyObject = new State();
                }
                return onlyObject;
            }
        }

        public EdgeConfiguration EdgeConfiguration { get; set; }

    }
}
