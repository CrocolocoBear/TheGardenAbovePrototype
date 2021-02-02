using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*NOTE: 
 *This code is based on Dapper Dino's tutorials :)
 */

namespace GCUWebGame.Events
{
    //T = when inherited, any type/data can be passed through
    public abstract class BaseGameEvent<T> : ScriptableObject
    {
        private readonly List<IGameEventListener<T>> eventListeners = new List<IGameEventListener<T>>();
    
        public void Raise(T item)
        {
            //looping backwards prevents encountering something missing/incorrect indexing, due to potential destroyed objects from an event
            for (int i = eventListeners.Count - 1; i >= 0; i--)
            {
                eventListeners[i].OnEventRaised(item);
            }
        }

        public void RegisterListener(IGameEventListener<T> listener)
        {
            //if already in list, don't re-register
            if (!eventListeners.Contains(listener))
            {
                eventListeners.Add(listener);
            }
        }

        public void UnregisterListener(IGameEventListener<T> listener)
        {
            //if found, remove
            if (eventListeners.Contains(listener))
            {
                eventListeners.Remove(listener);
            }
        }
    }
}
