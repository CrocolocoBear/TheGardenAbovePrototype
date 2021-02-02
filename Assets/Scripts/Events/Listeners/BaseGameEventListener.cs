using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/*NOTE: 
 *This code is based on Dapper Dino's tutorials :)
 */

namespace GCUWebGame.Events
{
    /*needs to be attached to objects to reference scripts, so when event is called it will act on it.
      T=Type, E=Event, UER=Unity Event Response*/
    public abstract class BaseGameEventListener<T, E, UER> : MonoBehaviour,
        IGameEventListener<T> where E : BaseGameEvent<T> where UER : UnityEvent<T>
    {
        [SerializeField] private E gameEvent;
        public E GameEvent { get { return gameEvent; } set { gameEvent = value; } }

        [SerializeField] private UER unityEventResponse;

        //listeners should only listen when in scene
        private void OnEnable()
        {
            if (gameEvent == null) { return; }

            GameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            if (gameEvent == null) { return; }

            GameEvent.UnregisterListener(this);
        }

        public void OnEventRaised(T item)
        {
            //if there is an UER, invoke
            if (unityEventResponse != null)
            {
                unityEventResponse.Invoke(item);
            }
        }
    }
}