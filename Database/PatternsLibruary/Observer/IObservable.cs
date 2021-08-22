using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatternsLibruary.Observer
{
    public interface IObservable
    {
        public void AddObserver(IObserver obj);
        public void NotifyObservers();
        public void RemoveObserver(IObserver obj);
    }
}
