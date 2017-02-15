using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * Enemy.cs
 * Autors: Rob Merrick
 * This class has common properties that all enemies in the 
 * game should share. It is an abstract class and there cannot
 * be derived directly. All game entities which are inherently
 * an enemy (such as an asteroid, an enemy ship, a boss, etc.)
 * should inherit from this clas.
 */

namespace RossHigleyProject7a
{
    abstract class Enemy : Entity2D
    {

        //Variable Declarations
        protected SpecialEffects blowUpAnimation;

        //Static Variables
        public static PlayerShip playerShip;

        ///**********************************************************************************************************************
        ///<summary>Creates a new instance of the Enemy class. This is an abstract class and cannot be called directly.</summary>
        ///**********************************************************************************************************************

        public Enemy(MainWindow window): base(window)
        {
            //Empty
        }

        ///*************************************************************************************************
        ///<summary>Sets the playerShip instance associated with all instances of the Enemy class.</summary>
        ///*************************************************************************************************

        public static void setPlayerShip(PlayerShip ship)
        {
            playerShip = ship;
        }

        ///*****************************************************************************************************************
        ///<summary>Updates the player ship and this enemy based upon the current state of the game. Items updated include
        ///checking for playerShip and enemy collisions, checking for playerShip lasers and enemy collisions, etc.</summary>
        ///*****************************************************************************************************************

        protected virtual void updateWithRespectToPlayer()
        {
            if (blowUpAnimation == null)
            {
                if(playerShip.isCollidingWith(this))
                    playerShip.destroy();

                foreach(Entity2D laser in GameManager.getAll2DEntities())
                {
                    if(laser is Laser && ((Laser) laser).isFromPlayerShip() && laser.onlyonecollision == false && laser.isCollidingWith(this) )
                    {
                        if(TargetingSystem.targetedItem == this) PlayerShip.targetDestroyed = true;
                        laser.destroy();
                        this.destroy();
                        
                    }
                }
            }
            else
            {
                int currentFrame = blowUpAnimation.drawExplosion(getXLocation(), getYLocation(), getRotation(), getScaleX(), getScaleY());
                
                if(currentFrame >= SpecialEffects.EXPLOSION_KEY_FRAME)
                    this.setEntityIsShown(false);

                if(currentFrame >= SpecialEffects.EXPLOSION_FRAME_COUNT - 1)
                    GameManager.remove2DEntity(this);
            }
        }

        ///**************************************************************************************************************
        ///<summary>This is the default action to take when the enemy object is destroyed. Calling this method causes the
        ///enemy object to blow up, award points (based on the derived class's awardPoints() method), and then remove
        ///itself from the GameManager's rendering list.</summary>
        ///**************************************************************************************************************

        public override void destroy()
        {
            if(blowUpAnimation == null)
            {
                isDestroyed = true;
                blowUpAnimation = new SpecialEffects(parentWindow);
                blowUpAnimation.setUpExplosion(false);
                awardPoints();
            }
        }

        //This method should be required for all enemies.
        protected abstract void awardPoints();

    }
}