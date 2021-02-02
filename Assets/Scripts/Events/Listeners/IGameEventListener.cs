
/*NOTE: 
 *This code is based on Dapper Dino's tutorials :)
 */

namespace GCUWebGame.Events
{
        //any event listener has a raise function taking same type as listener
        public interface IGameEventListener<T>
        {
            void OnEventRaised(T item);
        }
}
