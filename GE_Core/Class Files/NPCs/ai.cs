using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace GE_Core.NPCs
{
    public class ai
    {
        public void setupFollowerAI(string[] imgArray, string name, int startX, int startY, int width, int height, int health) {
            NPCbase follower = new NPCbase();
            follower.imageArray = imgArray;
            follower.spriteX = startX;
            follower.spriteY = startY;
            follower.spriteWidth = width;
            follower.spriteHeight = height;
            follower.spriteName = name;
            follower.spriteHealth = health;
        }
    }
}
