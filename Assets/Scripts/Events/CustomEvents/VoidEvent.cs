using UnityEngine;

/*NOTE: 
 *This code is based on Dapper Dino's tutorials :)
 */

namespace GCUWebGame.Events
{
    //can alter; making own event "thing" so one can actually be created in game
    [CreateAssetMenu(fileName = "New Void Event", menuName = "Game Events/Void Event")]

    /*do this because parameter is needed & don't want to pass a random type through,
       SO own type was made (with no data/properties)..*/
    public class VoidEvent : BaseGameEvent<Void>
    {
        public void Raise() => Raise(new Void());
    }
}
