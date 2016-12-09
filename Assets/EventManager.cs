using UnityEngine;
using System.Collections.Generic;

public delegate void EventHandler();
public delegate void EventHandlerArg(object obj);
public delegate void EventHandlerArgs(object[] objects);

#pragma warning disable

public class EventManager
{
    private static readonly Dictionary<string, EventHandler> eventHandlers = new Dictionary<string, EventHandler>();
    private static readonly Dictionary<string, EventHandlerArg> eventHandlersArg = new Dictionary<string, EventHandlerArg>();
    private static readonly Dictionary<string, EventHandlerArgs> eventHandlersArgs = new Dictionary<string, EventHandlerArgs>();

    public static void AddListener(string message, EventHandler handler)
    {
        if (eventHandlers.ContainsKey(message))
        {
            eventHandlers[message] += handler;
        }
        else
        {
            eventHandlers.Add(message, handler);
        }
    }

    public static void RemoveListener(string message, EventHandler handler)
    {
        if (eventHandlers.ContainsKey(message))
        {
            eventHandlers[message] -= handler;
        }
    }

    public static void AddListener(string message, EventHandlerArg handler)
    {
        if (eventHandlersArg.ContainsKey(message))
        {
            eventHandlersArg[message] += handler;
        }
        else
        {
            eventHandlersArg.Add(message, handler);
        }
    }

    public static void RemoveListener(string message, EventHandlerArg handler)
    {
        if (eventHandlersArg.ContainsKey(message))
        {
            eventHandlersArg[message] -= handler;
        }
        else
        {
            Debug.LogWarning("you tried to remove event named: " + message + " , but it's absent or it was removed.");
        }
    }

    public static void Invoke(string message)
    {
        if (eventHandlers.ContainsKey(message))
        {
            eventHandlers[message]();
        }
        else
        {
            Debug.LogWarning("you tried to call event named: " + message + " , but it's absent or it was removed.");
        }
    }

    public static void Invoke(string message, object argument)
    {
        if (eventHandlersArg.ContainsKey(message))
        {
            eventHandlersArg[message](argument);
        }
        else
        {
            Debug.LogWarning("you tried to call event named: " + message + " , but it's absent or it was removed.");
        }
    }
}