/* 
 Ross Higley 11/16/16
 This file contains the Fuel Gauge class, which is based on the amount of illudium Q36 in the 
 Player's Cargo. Each laser fire and engine thrust depletes a little bit of fuel. The tank capacity 
 is set by the largest amount of fuel the player has had in it. when the player buys more fuel, if
 she buys more than the gauge's capacity, the gauge will adapt. 

 Note: the guage is based on PlayerShip's cargo. changes here will be affected there, and vice-versa. 

  
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossHigleyProject7a.UI
{
    class FuelGauge
    {
        /// <summary>
        /// Ross Higley     11/16/16
        /// Constructor. Creates new Fuel Gauge, with full but small capacity. 
        /// </summary>
        public FuelGauge()
        {

        }

        /// <summary>
        /// Ross Higley     11/16/16
        /// Removes the amount of fuel from the gauge and cargo. 
        /// </summary>
        /// <param name="amount"></param>
        public void consumeFuel(int amount)
        {

        }

        /// <summary>
        /// Ross Higley     11/16/16
        /// Adjusts the size of the gauge. 
        /// </summary>
        /// <param name="amount"></param>
        public void updateMaxCapacity(int amount )
        {

        }

    }
}
