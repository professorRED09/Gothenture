using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    private List<IObserver> ObserverList = new List<IObserver>(); // for storing observers

    public void AddObserver(IObserver observer)
    {
        ObserverList.Add(observer); // add observer to the list
    }

    public void RemoveObserver(IObserver observer)
    {
        ObserverList.Remove(observer); // remove observer from the list
    }

    public void NotifyObserver(PlayerAction action) // Send a signal to all observers in the list
                                                    // when a specific situation occurs.

    {
        ObserverList.ForEach((ObserverList) =>
        {
            ObserverList.OnNotify(action);
        });
    }
}
