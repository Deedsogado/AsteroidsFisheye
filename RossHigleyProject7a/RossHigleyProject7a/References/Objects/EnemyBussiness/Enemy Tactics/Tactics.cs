using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RossHigleyProject7a
{

    /*****
     * Jacob Lehmer
     * 4/012/15
     * This is the base class for the tactics
     * *****/

    abstract class Tactics
    {
        private EnemyShip enemyShipRef;
        public MainWindow parentWindow;

        /*****
         * This is the property for the enemy ship reference
         * *****/
        protected EnemyShip EnemyShipRef
        {
            get { return enemyShipRef; }
            set { enemyShipRef = value; }
        }

        //this is the constructor for the Tectics class, there is little here, this mainly becuse the different algortihms are so vastly different
        public Tactics(EnemyShip _eneref, MainWindow window)
        {
            EnemyShipRef = _eneref;
            parentWindow = window;
        }

        //this is the only thing that actually does anything
        public abstract void run();
    }
}
