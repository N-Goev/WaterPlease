using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventBus
{
    private static readonly Dictionary<Type, List<Delegate>> subscribers = new();

    public static void Add<T>(Action<T> handler) where T : IGameEvent
    {
        Type type = typeof(T);

        if(!subscribers.TryGetValue(type, out List<Delegate> list))
        {
            list = new();
            subscribers[type] = list;
        }
        list.Add(handler);
    }

    public static void Remove<T>(Action<T> handler) where T : IGameEvent
    {
        Type type = typeof(T);

        if (subscribers.TryGetValue(type, out List<Delegate> list))
        {
            list.Remove(handler);
            if(list.Count <= 0)
            {
                subscribers.Remove(type);
            }
        }
        
    }

    public static void Invoke<T>(T everything) where T : IGameEvent
    {
        Type type = typeof(T);

        if (subscribers.TryGetValue(type, out List<Delegate> list))
        {
            Delegate[] copy = list.ToArray();
            foreach (var d in copy)
            {
                ((Action<T>)d)(everything);
            }
        }
    }
}
