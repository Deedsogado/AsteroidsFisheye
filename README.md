# AsteroidsFisheye

Clone of Asteroids I worked on for CS 3308 Data Structures and Programming. Most of the code was written before it was given to me. 
I re-wrote parts of it so that asteroids and the ship no longer wrap-around the edge of the screen. They appear to move slower near the edges of the screen, but actually the viewable universe is compressed on a psuedo-logarithmic scale. the farther away from the center of the screen, the more compressed together space is.  

The ship is always aiming not at where your mouse is hovering over the screen, but at where your mouse is "projected" into the universe. This is why when you hover the mouse near a corner of the screen, it appears to deflect to one side.  It's actually spot on. But, on a logarithmic scale, 1 pixel of screen-space has a potential margin of error of 100s of pixels in universe space, so it's a little hard to snipe asteroids and enemies that are on the edge of the window. 

When the ship moves too far away from the center of the screen, the screen will re-center around the ship, at a speed proportional to the speed of the ship. So, feel free to accelerate as fast as you want.

Even though the universe is "infinite" I didn't want to waste player's time trying to follow an asteroid for millions of miles. Instead, when an asteroid gets far away, it is teleported to just off-screen. 

##Controls 
Hover mouse Where you want to move and shoot. When you hover over an enemy, an aming reticle will apear which leads the target. 
Left lick mouse to engage thruster engines. 
Right click mouse to shoot lasers. 

'Tab' key to teleport short distance
'1' key to file missiles. They will follow the last object your reticle was on, and will instantly destroy it upon contact. 
'~' key (tilde) to turn on intertial dampeners. Basically, they are brakes. 
'Esc' key to open the pause menu, where you can find the upgrades menu. 

##Credits
Directed by Dr. David Beard
Lead Programmer - Rob Merrick
Assistant Programmer - Ted Delezene
Music Composed Rob Merrick
Image Processing - Ted Delezene, Rob Merrick, Thomas Veyrat, Qiao Wei
Sound Effects - Rob Merrick, Yoji Inagaki (Nintendo, Star Fox 64 copyright 1997)
Updated, modified and Jakeified - Jacob Lehmer
Universe Expanded and Curved by Ross Higley (Me)

![alt text](https://github.com/Deedsogado/AsteroidsFisheye/blob/master/Project7a.png "Screen shot of playing the game")
