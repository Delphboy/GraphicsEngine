/*MADE BY DELPHBOY GNU GPL LICENSE*/
/*HAVE FUN :)*/

//PROGRAMME STRUCTURE
/* DATA STRUCTURES
 * ===============
 * Direction ENUM
 * ToRender ENUM
 * NPCType
 * Sprite STRUCTURE
 * Map STRUCTURE
 */

/*
* VARIABLES
* ===============
* Game basics such as game title and form settings
* Character sprites
* Enemy 1 Spirtes
* Enemy 2 Sprites
* Enemy 3 Sprites
*/

/*
* FUNCTIONS
* ===============
* Form Initialise
 * Form Load
 * load game settings
 * Timer tick event
 * Controls
 * Render
 * AI
 * Memory Disposal
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicsEngine
{
    public enum dir { none, up, down, left, right};                                     //Dictates the direction the object is facing
    public enum ToRender { background, pause, generic, player, NPC1, NPC2, NPC3 };      //Dictates the object being rendered
    public enum NPCType { follower, wanderer };                                         //Dictates AI behaviour

    /* Sprite STRUCT
     * =====================
     * Holds the data needed for the sprite to be rendered and moved around.
     */
    
    public struct sprite {
        public ToRender type;
        public string[] imageArray;
        public Image spriteImage;
        public int spriteX;
        public int spriteY;
        public int spriteWidth;
        public int spriteHeight;

        public sprite(ToRender renderType, Image sImg, int X, int Y, int width, int height)
        {
            type = ToRender.generic;
            spriteImage = sImg;
            spriteX = X;
            spriteY = Y;
            spriteWidth = width;
            spriteHeight = height;
            imageArray = null;
        }

        public sprite(ToRender renderType, int X, int Y, int width, int height, string[] imgArray)
        {
            type = renderType;
            spriteImage = null;
            spriteX = X;
            spriteY = Y;
            spriteWidth = width;
            spriteHeight = height;
            imageArray = imgArray;
        }
    }

    //Data structure to hold the data for the map to be rendered on the screen
    public struct map {
        public string mapString;
        public int tileWidth;
        public int tileHeight;
        public Image mapImage;
        public int[,] collisionArray;
    }

    public partial class Form1 : Form
    {
        Graphics visual;

        string gameName = "testEngine1";
        FormBorderStyle style = FormBorderStyle.FixedSingle;
        bool canMax = false;
        bool canMin = false;
        int width = 800;
        int height = 600;

        //int X = 0;
        //int Y = 0;
        //int npc1X = 16;
        //int npc1Y = 16;

        Image backGound = Image.FromFile("C:\\Users\\Henry Senior\\Desktop\\bg.bmp");
        Image pauseScreen = Image.FromFile("C:\\Users\\Henry Senior\\Desktop\\pause.bmp");

        string spriteLocation = "C:\\Users\\Henry Senior\\Desktop\\";
        string[] playerImageFiles = new string[] { "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", };
        string[] NPC1ImageFiles = new string[] { "testNPC.bmp", "testNPC.bmp", "testNPC.bmp", "testNPC.bmp", "testNPC.bmp", "testNPC.bmp", "testNPC.bmp", "testNPC.bmp", "testNPC.bmp", "testNPC.bmp", "testNPC.bmp", };
        string[] NPC2ImageFiles = new string[] { "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", };
        string[] NPC3ImageFiles = new string[] { "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", };

        sprite player;
        sprite NPC1;

        //Initialise Form loading
        public Form1()
        {
            InitializeComponent();
            gameTimer.Start();
            gameTimer.Enabled = true;
            visual = CreateGraphics();

            //SET UP PLAYER
            player.type = ToRender.player;
            player.imageArray = playerImageFiles;
            player.spriteImage = Image.FromFile(spriteLocation + playerImageFiles[0]);
            player.spriteX = 16;
            player.spriteY = 16;
            player.spriteWidth = 16;
            player.spriteHeight = 16;

            //SET UP NPC1
            NPC1.type = ToRender.NPC1;
            NPC1.imageArray = NPC1ImageFiles;
            NPC1.spriteImage = Image.FromFile(spriteLocation + NPC1ImageFiles[0]);
            NPC1.spriteX = 16;
            NPC1.spriteY = 16;
            NPC1.spriteWidth = 16;
            NPC1.spriteHeight = 16;
        }

        //form load sub
        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Seperate X and Y ", "Error");
            loadGameSettings();
        }

        //load game settings
        public void loadGameSettings() {
            this.Text = gameName;
            //this.Width = width;
            //this.Height = height;
            this.FormBorderStyle = style;
            this.MaximizeBox = canMax;
            this.MinimizeBox = canMin;
            this.BackgroundImage = backGound;
        }

        //Timer event
        private void timerFire(object sender, EventArgs e)
        {
            //Uncomment line below for error
            NPC(NPC1, NPCType.wanderer, ToRender.NPC1);
        }

        //Controls - Key handling
        private void controls(object sender, KeyEventArgs e)
        {
            /*
             * UP
             * DOWN
             * LEFT
             * RIGHT
             * PAUSE
             * SPACE
             */

            if (gameTimer.Enabled == true)
                //Console.WriteLine(player.spriteX.ToString() + " : " + player.spriteY.ToString());
                switch (e.KeyCode) { 
                    case Keys.W:
                        moveSprite(player, dir.up, player.spriteX, player.spriteY - player.spriteHeight);
                        player.spriteY -= player.spriteHeight;
                        break;
                    case Keys.S:
                        moveSprite(player, dir.down, player.spriteX, player.spriteY + player.spriteHeight);
                        player.spriteY += player.spriteHeight;
                        break;
                    case Keys.A:
                        moveSprite(player, dir.left, player.spriteX - player.spriteWidth, player.spriteY);
                        player.spriteX -= player.spriteWidth;
                        break;
                    case Keys.D:
                        moveSprite(player, dir.right, player.spriteX + player.spriteWidth, player.spriteY);
                        player.spriteX += player.spriteWidth;
                        break;
                }

            if (e.KeyCode == Keys.Escape){
                if (gameTimer.Enabled == false)
                {
                    gameTimer.Enabled = true;
                    render(backGound, new Rectangle(0,0,width,height));
                    render(player.spriteImage, new Rectangle(player.spriteX, player.spriteY , player.spriteWidth, player.spriteHeight));
                    render(NPC1.spriteImage, new Rectangle(NPC1.spriteX, NPC1.spriteY, NPC1.spriteWidth, NPC1.spriteHeight));
                    this.Text = "Game Engine";
                }
                else
                {
                    gameTimer.Enabled = false;
                    this.Text = "PAUSED";
                    render(pauseScreen, new Rectangle(0, 0, width, height));
                }
            }

            if (e.KeyCode == Keys.Space)
            {
                //DO STUFF
            }
            
        }

        //Controls - Moving NPC
        private void moveSprite(sprite s, dir dir, int newX, int newY){
            /* Render background
             * Check direction
             * Cycle animation frames
             * Render new position
             */
            s.spriteX = newX;
            s.spriteY = newY;

            //animationCycle = 0 corresponds to the start image of that direction in the image array
            int animationCycle = 0;

            render(backGound, new Rectangle(0,0,this.width, this.height));

            Console.WriteLine(s.spriteX.ToString() + " : " + s.spriteY.ToString());

            switch (dir) { 
                case dir.up:
                    animationCycle = animationCycle > 2 ? animationCycle = 0 : animationCycle;
                    render(Image.FromFile(spriteLocation + s.imageArray[0 + animationCycle]), new Rectangle(s.spriteX, s.spriteY, s.spriteWidth, s.spriteHeight));
                    break;
                case dir.down:
                    animationCycle = animationCycle > 2 ? animationCycle = 0 : animationCycle;
                    render(Image.FromFile(spriteLocation + s.imageArray[0 + animationCycle]), new Rectangle(s.spriteX, s.spriteY, s.spriteWidth, s.spriteHeight));
                    break;
                case dir.left:
                    animationCycle = animationCycle > 2 ? animationCycle = 0 : animationCycle;
                    render(Image.FromFile(spriteLocation + s.imageArray[0 + animationCycle]), new Rectangle(s.spriteX, s.spriteY, s.spriteWidth, s.spriteHeight));
                    break;
                case dir.right:
                    animationCycle = animationCycle > 2 ? animationCycle = 0 : animationCycle;
                    render(Image.FromFile(spriteLocation + s.imageArray[0 + animationCycle]), new Rectangle(s.spriteX, s.spriteY, s.spriteWidth, s.spriteHeight));
                    break;
            }
        }

        //Render function - render images to the screen
        //TODO - Give the render procedure it's own thread to improve performance
        public void render(Image img, Rectangle size)
        {
            try
            {
                visual.DrawImage(img, size);
            }
            catch (Exception ex) {
                MessageBox.Show("The render function has tried to render an image and failed" + Environment.NewLine + size.X.ToString() + " : " + size.Y.ToString() + Environment.NewLine + ex.Message, "Error");
            }
        }

        //Control enemy AI (called on timer.Tick event)
        public void NPC(sprite NPCsprite, NPCType type, ToRender renderStyle) {
            switch (type) {
                case NPCType.follower:                   //Follow the player around
                        
                    break;
                case NPCType.wanderer:
                    /*Wander around the screen
                     * generate a random number between 0 and 3
                     * 0 = UP
                     * 1 = DOWN
                     * 2 = LEFT
                     * 3 = RIGHT
                     * check the number generated and move NPC in that 
                     */

                    Random rnd = new Random();
                    int rndInt = rnd.Next(0, 3);
                    switch (rndInt) { 
                        case 0:
                            NPCsprite.spriteY -= NPCsprite.spriteHeight;
                            moveSprite(NPC1, dir.up, NPCsprite.spriteX, NPCsprite.spriteY);
                            break;
                        case 1:
                            NPCsprite.spriteY += NPCsprite.spriteHeight;
                            moveSprite(NPC1, dir.up, NPCsprite.spriteX, NPCsprite.spriteY);
                            break;
                        case 2:
                            NPCsprite.spriteX -= NPCsprite.spriteWidth;
                            moveSprite(NPC1, dir.up, NPCsprite.spriteX, NPCsprite.spriteY);
                            break;
                        case 3:
                            NPCsprite.spriteX += NPCsprite.spriteWidth;
                            moveSprite(NPC1, dir.up, NPCsprite.spriteX, NPCsprite.spriteY);
                            break;
                    }
                    break;
            }
        }

        //Free memroy
        private void freeMemory(object sender, FormClosedEventArgs e)
        {
            //Dispose of all images used
            player.spriteImage = null;
            NPC1.spriteImage = null;
        }
               
    }
}
