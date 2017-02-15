/* Ross Higley  11/16/16
 * This file contains the UpgradeRequestQueue class. When a user selects an upgrade
 * from the upgradeSystem, instead of being applied instantly, the request will be 
 * placed in a queue here. When the player's ship docks with a Trader that has the 
 * specified upgrades, those upgrades in the queue will then be installed, in the 
 * order they were received, or until the player runs out of money. 
 * 
 * This will likely be implemented as a searchable queue of method delegates. 
 * 
 * 
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossHigleyProject7a.References.Objects.NPCs
{
    class UpgradeRequestQueue
    {
        /// <summary>
        /// Ross Higley 11/16/16
        /// Constructor. Creates new upgrade request queue.
        /// </summary>
        public UpgradeRequestQueue()
        {

        }

        /// <summary>
        /// Ross Higley     11/16/16
        /// Adds the upgrade to the queue. 
        /// </summary>
        public void AddToQueue(Delegate del )
        {

        }

        /// <summary>
        /// Ross Higley     11/16/16
        /// removes the upgrades from the queue, like 
        /// if the player changes their mind. 
        /// </summary>
        /// <param name="del"></param>
        public void removeFromQueue(Delegate del)
        {

        }

        /// <summary>
        /// Ross Higley     11/16/16
        /// performs all the upgrades that the trader has available. 
        /// </summary>
        /// <param name="trader"></param>
        /// <param name="player"></param>
        public void performUpgradesFromTrader(Trader trader, PlayerShip player)
        {

        }
    }
}
