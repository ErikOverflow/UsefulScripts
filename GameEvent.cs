using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Game Event", order = 1)]
[Serializable]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    public void Raise()
    {
        for(int i = listeners.Count -1; i >= 0; i--)
        {
            listeners[i].OnEventsRaised();
        }
    }

    public void Raise(GameObject go)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventsRaised(go);
            listeners[i].OnEventsRaised();
        }
    }

    public void Raise(int num)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnEventsRaised(num);
            listeners[i].OnEventsRaised();
        }
    }

    public void RegisterListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    public void UnregisterListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }
}