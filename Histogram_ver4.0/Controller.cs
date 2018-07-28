using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Histogram_ver4._0
{
    class Controller : IObservable
    {
        IList<IObserver> list = new List<IObserver>();
        public void AddObserver(IObserver o)
        {
            list.Add(o);
        }

        public void RemoveObserver(IObserver o)
        {
            list.Remove(o);
        }

        public void NotifyObservers(IState state)
        {
            foreach (IObserver obs in list)
            {
                obs.Update(state);
            }
        }
    }
}
