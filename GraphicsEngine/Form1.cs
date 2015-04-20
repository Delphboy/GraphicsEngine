/*MADE BY DELPHBOY GNU GPL LICENSE*/
/*HAVE FUN :)*/

/////////////////////////////////////////////////////////////////////////////////////////

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
 * Map Loading
 * Render
 * AI
 * Overlay Rendering
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

using GE_Core;
using GE_Console;

namespace GraphicsEngine
{
    public enum dir { none, up, down, left, right};                                     //Dictates the direction the object is facing
    public enum ToRender { background, pause, generic, player, NPC1, NPC2, NPC3 };      //Dictates the object being rendered
    public enum NPCType { follower, wanderer };                                         //Dictates AI behaviour
    public enum overlay { none, one, two}                                               //Dictates the overlay in use

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

        //character specifics
        public string spriteName;
        public int spriteHealth;

        public sprite(ToRender renderType, Image sImg, int X, int Y, int width, int height, string name, int health)
        {
            type = ToRender.generic;
            spriteImage = sImg;
            spriteX = X;
            spriteY = Y;
            spriteWidth = width;
            spriteHeight = height;
            imageArray = null;
            spriteName = name;
            spriteHealth = health;
        }

        public sprite(ToRender renderType, int X, int Y, int width, int height, string[] imgArray, string name, int health)
        {
            type = renderType;
            spriteImage = null;
            spriteX = X;
            spriteY = Y;
            spriteWidth = width;
            spriteHeight = height;
            imageArray = imgArray;
            spriteName = name;
            spriteHealth = health;
        }
    }
    

    public partial class Form1 : Form
    {
        GE_Console.CMD_Console cmd;

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
        Image overlayOneBG = Image.FromFile("C:\\Users\\Henry Senior\\Desktop\\UI.bmp");    //800x250 bmp

        overlay selectedOverlay = overlay.one;

        string spriteLocation = "C:\\Users\\Henry Senior\\Desktop\\";
        string[] playerImageFiles = new string[] { "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", };
        string[] NPC1ImageFiles = new string[] { "testNPC.bmp", "testNPC.bmp", "testNPC.bmp", "testNPC.bmp", "testNPC.bmp", "testNPC.bmp", "testNPC.bmp", "testNPC.bmp", "testNPC.bmp", "testNPC.bmp", "testNPC.bmp", };
        string[] NPC2ImageFiles = new string[] { "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", };
        string[] NPC3ImageFiles = new string[] { "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", "testSprite.bmp", };

        //Ensure these are disposed in the freeMemory() void at the bottom of the programme
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
            player.spriteName = "Delphboy";
            player.spriteHealth = 100;

            //SET UP NPC1
            NPC1.type = ToRender.NPC1;
            NPC1.imageArray = NPC1ImageFiles;
            NPC1.spriteImage = Image.FromFile(spriteLocation + NPC1ImageFiles[0]);
            NPC1.spriteX = 16;
            NPC1.spriteY = 16;
            NPC1.spriteWidth = 16;
            NPC1.spriteHeight = 16;
            NPC1.spriteName = "Delphboy";
            NPC1.spriteHealth = 100;
        }

        //form load sub
        private void Form1_Load(object sender, EventArgs e)
        {
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
            //NPC(NPC1, NPCType.wanderer, ToRender.NPC1);
        }

        //Controls - Key handling
        private void controls(object sender, KeyEventArgs e)
        {
            /* UP
             * DOWN
             * LEFT
             * RIGHT
             * PAUSE
             * SPACE
             */

            if (gameTimer.Enabled == true)
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
                    case Keys.PageUp:
                        MessageBox.Show("open");
                        cmd = new GE_Console.CMD_Console();
                        
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
                //MessageBox.Show("test key");
                //"C:\\Users\\Henry Senior\\Desktop\\testMap.csv"
                GE_Core.mapLoading.map mapClass = new GE_Core.mapLoading.map();
                mapClass.readMap("C:\\Users\\Henry Senior\\Desktop\\testMap.csv");
            }
            
        }

        //Load Map
        public void mapReader(string mapFile) {
            string mapData = "";             //Complete map file
            string headerData = "";          //Map size in tiles
            string mainData = "";            //Map data
            string footerData = "";          //NPC data

            char[] delimiterChars = { '|', ',', '.', ':', '\t' };
            string[] mapDataArray = mapData.Split(delimiterChars);

            mapData = System.IO.File.ReadAllText(mapFile);

            //headerData is first few lines
            

            //mainData is the map details


            //footerData gives NPC data
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
                overlayRender(selectedOverlay); //render UI overlay over the image being rendered
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
                            moveSprite(NPC1, dir.down, NPCsprite.spriteX, NPCsprite.spriteY);
                            break;
                        case 2:
                            NPCsprite.spriteX -= NPCsprite.spriteWidth;
                            moveSprite(NPC1, dir.left, NPCsprite.spriteX, NPCsprite.spriteY);
                            break;
                        case 3:
                            NPCsprite.spriteX += NPCsprite.spriteWidth;
                            moveSprite(NPC1, dir.right, NPCsprite.spriteX, NPCsprite.spriteY);
                            break;
                        default:
                            moveSprite(NPC1, dir.none, NPCsprite.spriteX, NPCsprite.spriteY);
                            break;
                    }
                    break;
            }
        }

        /*  -----OVERLAYS-----  */
        /*Rendered using the visual object rather than through the "render" function to prevent prevent the stack from dying*/

        //RENDER OVERLAY
        public void overlayRender(overlay overlay) {
            switch (overlay) { 
                case overlay.one:
                    overlayOne();
                    break;
                case overlay.two:
                    overlayTwo();
                    break;
                default:
                    //no overlay is selected so don't do anything
                    break;
            }
        }

        //overlay 1 (DOOM STYLE)
        public void overlayOne() { 
            visual.DrawImage(overlayOneBG, new Rectangle(-10, 450, 800, 250));
            visual.DrawImage(Image.FromFile(spriteLocation + player.imageArray[0]), new Rectangle(375, 475, 50, 50));
            visual.DrawString(player.spriteName, new Font("Tahoma", 24), Brushes.White, new PointF(10, 460));
            visual.DrawString("Health: " + player.spriteHealth.ToString(), new Font("Tahoma", 24), Brushes.White, new PointF(600, 460));
        }

        //overlay 2(personal style)
        public void overlayTwo()
        {
            visual.DrawString("Health: " + Convert.ToString(player.spriteHealth), new Font("Arial", 24), Brushes.CornflowerBlue, new Rectangle(10/*400*/, 10, 300, 50)); 
        }

        /*  -----END UI CODE-----  */

        //Free memroy
        private void freeMemory(object sender, FormClosedEventArgs e)
        {
            //Dispose of all images used
            player.spriteImage = null;
            NPC1.spriteImage = null;
        }
               
    }
}
