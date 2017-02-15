/*
 * Ross Higley  11/16/16
 This file contains the TraderInventory class, which is contains a list of 
 items in stock, wanted goods, and their associated prices. It also provides 
 methods to buy and sell these items, and display the inventory as a formatted
 string. 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RossHigleyProject7a.References.Objects.NPCs
{
    class TraderInventory
    {
        /// <summary>
        /// Ross Higley     11/16/16
        /// Constructor. Creates new Inventory with random items and prices for buying and selling. 
        /// </summary>
        public TraderInventory()
        {
               
        }

        /// <summary>
        /// Ross Higley     11/16/16
        /// Transfers the quantity of items from the Trader to the playerShip,
        /// deducting that price from the Player's gold. 
        /// </summary>
        /// <param name="ID">Id of the item, from Item. </param>
        public void sellItemToPlayer(ItemID ID, int quantity, PlayerShip player)
        {

        }

        /// <summary>
        /// Ross Higley     11/16/16
        /// Transfers the quantity of items from the playerShip to the Trader, adding 
        /// the price to the player's gold. 
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="quantity"></param>
        /// <param name="player"></param>
        public void buyItemFromPlayer(ItemID ID, int quantity, PlayerShip player)
        {

        }


        /// <summary>
        /// Ross Higley     11/16/16
        /// Unique Identifiers for items that can be bought or sold. 
        /// Keep it consistant with Player's Cargo. 
        /// May relocate this under a different class, like Items. 
        /// </summary>
        public enum ItemID
        {
            Wheat, Gold, Iron, Q36
        }

    }
}
