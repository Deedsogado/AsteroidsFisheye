/* 
 Ross Higley    11/16/16
 This File contains the Maneuvers class, which is responsible for all tricky math 
 required to perform the BurnAndFlip and DodgeAsteroids maneuvers, which will pilot
 the ship automatically to a given location. (usually a trader's ship). 

 Each manuever will have many overloaded methods. 

 */

using RossHigleyProject7a.References.Objects.NPCs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossHigleyProject7a.References.Objects.PlayerBussiness
{
    class Maneuvers
    {

        /// <summary>
        /// Ross Higley     11/16/16
        /// Moves ship to this location, and stops it when it arrives. 
        /// </summary>
        /// <param name="xCoord"></param>
        /// <param name="yCoord"></param>
        public static void burnAndFlip(float DestinationXCoord, float DestinationYCoord, PlayerShip player)
        {

        }

        /// <summary>
        /// Ross Higley     11/16/16
        /// Moves ship to this trader, and docks when it arrives. 
        /// </summary>
        /// <param name="xCoord"></param>
        /// <param name="yCoord"></param>
        public static void burnAndFlip(Trader trader, PlayerShip player)
        {

        }
        /// <summary>
        /// Ross higley     11/16/16
        /// Calculates amount of Illudium Q36 it will take to perform a
        /// BurnAndFlip to the specified location. 
        /// </summary>
        /// <returns></returns>
        public static double estimateFuelConsumptionForBurnAndFlip(float DestinationXCoord, float DestinationYCoord, PlayerShip player)
        {
            return 0.0;
        }


        /// <summary>
        /// Ross higley     11/16/16
        /// Calculates amount of Illudium Q36 it will take to perform a
        /// BurnAndFlip to the specified location. 
        /// </summary>
        /// <returns></returns>
        public static double estimateFuelConsumptionForBurnAndFlip(Trader trader, PlayerShip player)
        {
            return 0.0;
        }

        public static void dodgeAsteroids()
        {

        }


    }
}
