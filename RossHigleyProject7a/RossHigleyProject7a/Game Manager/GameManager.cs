using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

/**
 * GameManager.cs
 * Autors: Rob Merrick
 * This class is used to manage all of the gampeplay items, 
 * including updating and drawing objects to the screen.
 */

namespace RossHigleyProject7a
{
    public class GameManager
    {

        //Variable declarations
        private static bool finishedTheGame;
        private static int livesLeft;
        private static Environment starField;
        private static List<Entity2D> entity2DList;
        private static List<Entity2D> newEntitiesToAdd;
        private static List<Entity2D> oldEntitiesToRemove;
        private static GUI gui;
        private static Sound music;
        private static Sound pauseSound;

        //Public variables;

   //     public static PointF canvasOrigin;
   //     public static Graphics canvas;
        public static Random randomNumberGenerator = new Random();
        public TargetingSystem targetingSystem;
        public PauseMenu pm;
        public MainWindow parentWindow;

        ///*******************************************************************
        ///<summary>Creates a new instance of the GameManager class.</summary>
        ///*******************************************************************

        public GameManager(MainWindow window)
        {
            //Empty
            parentWindow = window;
        }

        /*******
         * Jacob Lehmer
         * 4/15/15
         * This is a property list to be able to access the parts of the program
         * ******/

        //The entity list for the program
        public List<Entity2D> EntityList
        {
            get { return entity2DList; }
        }

        //The player ship
        public PlayerShip _PlayerShip
        {
            get { return parentWindow.ship; }
        }
      



        ///******************************************************************************************************************
        ///<summary>This method works like a constructor, but it must be called after the GameManager was created. Otherwise,
        ///many methods that depend on the Graphics object to paint on will fail.</summary>
        ///******************************************************************************************************************

        public void initialize()
        {
            finishedTheGame = false;
            livesLeft = 3;
            newEntitiesToAdd = new List<Entity2D>();
            oldEntitiesToRemove = new List<Entity2D>();

            gui = new GUI(this);
            Scores.initialize();
            Level.setCurrentLevel(1);
            music = new Sound("Music\\Game Music Looped.wav");
            music.playSound(true);
            music.setVolume(0.5);
            pauseSound = new Sound("");
            Voices.intitialize();
            reloadLevel();
            targetingSystem = new TargetingSystem(this, parentWindow.ship, parentWindow);
            pm = new PauseMenu(this);
            
        }

        ///*************************************************************************************************
        ///<summary>Clears the entire screen to black. Call this once prior to drawing each frame.</summary>
        ///*************************************************************************************************

        public void clearScreen(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
        }

        ///*************************************************************************************
        ///<summary>Draws every object on the screen. Call this once per frame update.</summary>
        ///*************************************************************************************

        public void drawFrame(PaintEventArgs e)
        {
            starField.draw(e);

            foreach(Entity2D entity in entity2DList)
            {
               entity.draw(e, true);
               // entity.draw(e, false);
            }
               

            SpecialEffects.drawAllSpecialEffects(e);
            gui.drawGUI(livesLeft, e);
            parentWindow.ship.justwarped = false;
        }

        ///********************************************************************************************
        ///<summary>Updates every object in the game. Call this prior to calling drawFrame().</summary>
        ///********************************************************************************************

        public void updateFrame()
        {
            starField.update();

            foreach(Entity2D entity in entity2DList)
                entity.update();

            updateEntity2DList();

            if(Scores.shouldAwardNewLife())
            {
                Voices.extraLifeAwarded.playSound(false);
                ++livesLeft;
                Scores.calculateNextNewLifeScore();
            }

            if(isEveryEnemyDefeated())
            {
                Voices.levelComplete.playSound(false);
                Level.setCurrentLevel(Level.getCurrentLevel() + 1);
                reloadLevel();
            }
            targetingSystem.PlayerShipRef = parentWindow.ship;
            if (parentWindow.ship.laserTimer > 0)
                parentWindow.ship.laserTimer--;
            if (parentWindow.ship.liveThroughStart > 0)
                parentWindow.ship.liveThroughStart--;
        }

        ///****************************************************
        ///<summary>Returns true if the game is over.</summary>
        ///****************************************************

        public bool isGameOver()
        {
            return finishedTheGame;
        }

        ///****************************************************************************************************************
        ///<summary>Updates the needed information to kill the player (take away a life) and reset the level. If the user's
        ///remaining lives drops below 1, the game over screen is displayed.</summary>
        ///****************************************************************************************************************

        public void killPlayer()
        {
            --livesLeft;

            if(livesLeft > 0)
            {
                Voices.yourShipHasBeenDestroyed.playSound(false);
                Thread.Sleep(3000);
                reloadLevel();
            }
            else
                gameOver(false,false);
        }

        ///********************************************************************************************************
        ///<summary>Returns a read-only list of 2D entities that are currently stored in the GameManager.</summary>
        ///********************************************************************************************************

        public static IReadOnlyCollection<Entity2D> getAll2DEntities()
        {
            return entity2DList.AsReadOnly();
        }

        ///**************************************************************************************************************
        ///<summary>Add the provided Entity2D to the GameManager's list so that it may be included on the next draw() and
        ///update() calls for the game.</summary>
        ///**************************************************************************************************************

        public static void add2DEntity(Entity2D entity)
        {
            newEntitiesToAdd.Add(entity);
        }

        ///***************************************************************************************************************
        ///<summary>Call this method if you need to remove any entity from the GameManager's entity list. This change will
        ///be reflected beginning on the next draw() and update() calls.</summary>
        ///***************************************************************************************************************

        public static void remove2DEntity(Entity2D entity)
        {
            oldEntitiesToRemove.Add(entity);
        }

        ///*******************************************************************************************************************
        ///<summary>If any entities were added or removed during the last update, the GameManager's entity2DList is updated to 
        ///reflect the new changes by calling this method.</summary>
        ///*******************************************************************************************************************

        private void updateEntity2DList()
        {
            foreach(Entity2D entity in oldEntitiesToRemove)
                entity2DList.Remove(entity);

            foreach(Entity2D entity in newEntitiesToAdd)
                entity2DList.Add(entity);

            oldEntitiesToRemove.Clear();
            newEntitiesToAdd.Clear();
        }

        ///**************************************************************************************************************
        ///<summary>Returns true if every entity that is of type Enemy has been removed from the entity2D list.</summary>
        ///**************************************************************************************************************

        private bool isEveryEnemyDefeated()
        {
            foreach(Entity2D entity in entity2DList)
                if(entity is Enemy)
                    return false;

            return true;
        }

        ///*******************************************************************************************************************
        ///<summary>Resets everything back to its initial state when the level began. Note that the order in which 2DEntities 
        ///are added to the GameManager's entity2DList is important. Entities will be drawn in the order they are added to the
        ///list. This means that the first entity added will appear below every other entity drawn on the screen.</summary>
        ///*******************************************************************************************************************

        private void reloadLevel()
        { 
            parentWindow.ship = new PlayerShip(parentWindow);
            starField = new Environment(parentWindow);

            entity2DList = new List<Entity2D>();
            
            parentWindow.camera = new Camera(parentWindow);



            LevelDifficultyAlgorithim.increaseDifficulty(Level.getCurrentLevel());
            for (int i = 0; i < Level.getInitialNumberOfLargeAsteroids(); i++)
                if (Settings.difficulty != Settings.Difficulty.ALIENAPOCOLAPSE) entity2DList.Add(new Asteroid(Asteroid.SIZE.LARGE, parentWindow));
                else entity2DList.Add(new EnemyShip(parentWindow));

            for(int i = 0; i < Level.getInitialNumberOfMediumAsteroids(); i++)
                if (Settings.difficulty != Settings.Difficulty.ALIENAPOCOLAPSE) entity2DList.Add(new Asteroid(Asteroid.SIZE.MEDIUM, parentWindow));
                else entity2DList.Add(new EnemyShip(true, parentWindow));

            for(int i = 0; i < Level.getInitialNumberOfSmallAsteroids(); i++)
                if (Settings.difficulty != Settings.Difficulty.ALIENAPOCOLAPSE) entity2DList.Add(new Asteroid(Asteroid.SIZE.SMALL, parentWindow));
                else { entity2DList.Add(new EnemyShip(parentWindow)); entity2DList.Add(new EnemyShip(true, parentWindow)); }

            for(int i = 0; i < Level.getInitialNumberOfLargeEnemyShips(); i++)
                if (Settings.difficulty != Settings.Difficulty.ALIENAPOCOLAPSE) entity2DList.Add(new EnemyShip(parentWindow));
               

            for(int i = 0; i < Level.getInitialNumberOfSmallEnemyShips(); i++)
                if (Settings.difficulty != Settings.Difficulty.ALIENAPOCOLAPSE) entity2DList.Add(new EnemyShip(true, parentWindow));
            
        //    entity2DList.Add(new CustomMouse(parentWindow));
            entity2DList.Add(parentWindow.ship);
            entity2DList.Add(parentWindow.camera);
            Enemy.setPlayerShip(parentWindow.ship);
            PlayerShip.targetDestroyed = true;

        }

        ///************************************************************************************************************
        ///<summary>Runs the Game-Over routine. During this process, all scores, levels, and other relevant static data
        ///is reset to its initial value.</summary>
        ///************************************************************************************************************
        
        public void gameOver(bool playerFinishedTheLastLevel, bool isFromMenu)
        {
            Scores.addPoints(1);
            Scores.addScoreToFile();
            //if (!isFromMenu)
            //{
         
                for (int i = 0; i < 100; i++)
                {
                    music.setVolume(Math.Max(0.5 - i / 100.0, 0));
                    System.Threading.Thread.Sleep(10);
                }
                music.stopSound();
            //}
            
            Level.setCurrentLevel(1);
            Scores.setScore(0);
            
            finishedTheGame = true;
            if (!isFromMenu)
            {
                Voices.gameOver.playSound(false);
                Thread.Sleep(3000);
            }
            
        }

        /****
         * Jacob Lehmer
         * 4/19/15
         * This will hyperspace the player
         * *******/
        public void hyperspaceThePlayer()
        {
            try
            {
                float tempX = randomNumberGenerator.Next(
                    (int)parentWindow.fisheye.WindowBounds.Width / -2, 
                    (int)parentWindow.fisheye.WindowBounds.Width / 2);
                float tempY = randomNumberGenerator.Next(
                  (int)parentWindow.fisheye.WindowBounds.Height / -2,
                  (int)parentWindow.fisheye.WindowBounds.Height / 2);

                parentWindow.ship.setPosition(
                   parentWindow.ship.getXLocation() + tempX, 
                   parentWindow.ship.getYLocation() + tempY, 
                    false);
                parentWindow.ship.setSpeed(
                    randomNumberGenerator.Next(-Settings.wormHoleStability, Settings.wormHoleStability), 
                    randomNumberGenerator.Next(-Settings.wormHoleStability, Settings.wormHoleStability));
                parentWindow.ship.setGraphic(new Graphic("Warp.png"));
                parentWindow.ship.justwarped = true;

                parentWindow.camera.setPosition(parentWindow.ship.getXLocation(), parentWindow.ship.getYLocation(), false);
                parentWindow.camera.setSpeed(parentWindow.ship.getXSpeed(), parentWindow.ship.getYSpeed());
            }
            catch (Exception)
            {
                
            }
        }

    }
}