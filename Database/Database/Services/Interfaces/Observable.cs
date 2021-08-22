using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Services.Interfaces
{
    public abstract class Observable
    {
        protected List<IObserver> Observers { get; set; } = new List<IObserver>();
        public void AddObserver(IObserver observer) 
        {
            Observers.Add(observer);
        }
        public void RemoverObserver(IObserver observer) 
        {
            Observers.Remove(observer);
        }
        public void NotifyObserver() 
        {
            foreach (var observer in Observers)
                observer.Execute();
        }
    }
}
