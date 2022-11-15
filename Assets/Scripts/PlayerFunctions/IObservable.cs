using System.Collections.Generic;

public interface IObservable 
{
    List<IObserver> Observers { get; }

    void Subscribe(IObserver observer);
    void Unsubscribe(IObserver observer);

    void NotifyAll();
}
