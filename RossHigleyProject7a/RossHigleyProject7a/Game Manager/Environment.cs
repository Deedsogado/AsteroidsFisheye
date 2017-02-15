using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/**
 * Environment.cs
 * Autors: Rob Merrick
 * This class is used to create environmental game effects, 
 * particularly a parallaxed debris targetingReticule.
 */

namespace RossHigleyProject7a
{
    class Environment
    {
        
        //Variable declarations
        private List<Debris> debrisField;
        private MainWindow parentWindow;

        ///*******************************************************************
        ///<summary>Creates a new instance of the Environment class.</summary>
        ///*******************************************************************

        public Environment(MainWindow window)
        {
            parentWindow = window;
            debrisField = new List<Debris>();

            for(int i = 0; i < 100; i++)
                debrisField.Add(new Debris(parentWindow));

            ////Quickly simulate a few seconds of environmental screen updating so the debris targetingReticule is evenly distributed.
            //for(int i = 0; i < 200; i++)
            //    update();
        }

        ///**************************************************************
        ///<summary>Draws the entire environment to the screen.</summary>
        ///**************************************************************

        public void draw(PaintEventArgs e )
        {
            foreach(Debris debris in debrisField)
                debris.draw(e, false);
        }

        ///***********************************************************************************************
        ///<summary>Performs all actions that are required to update this instance of the class.</summary>
        ///***********************************************************************************************

        public void update()
        {
            foreach(Debris debris in debrisField)
            {
                debris.update();

                if(debris.isReadyToBeDestroyed())
                    debris.respawn();
            }
        }

    }
}