using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GE_Core.mapLoading
{
    /*TODO: Add a function that turns a CSV map file into rendered images
     * http://stackoverflow.com/questions/465172/merging-two-images-in-c-net
     */

    //Data structure to hold the data for the map to be rendered on the screen
    public struct mapStruct
    {
        public string mapName;
        public int tileWidth;
        public int tileHeight;
        public string mapCSVFile;
        public int[,] collisionArray;

        /* CSV FILE STRUCT
         * ===============
         * Contains integers to represent a predesigned tile
         * 1 = Grass
         * 2 = Wall
         * 3 = Path
         * 4 = Flowers
         * 5 = Wooden floor
         * 
         * 5x5 map will read like
         *      2, 3, 2, 2, 2
         *      2, 3, 3, 1, 2
         *      2, 4, 3, 4, 2
         *      2, 4, 3, 4, 2
         *      2, 2, 3, 2, 2
         */

        //using CSV file for map
        public mapStruct(string name, int width, int height, string mapFile, int[,] colArray)
        {
            mapName = name;
            tileWidth = width;
            tileHeight = height;
            mapCSVFile = mapFile;
            collisionArray = colArray;
        }
    }

    public class map
    {
        public void readMap(string fileLoc) {
            System.IO.StreamReader sr = new System.IO.StreamReader(fileLoc);
            var lines = new List<string[]>();
            int Row = 0;
            while (!sr.EndOfStream)
            {
                string[] Line = sr.ReadLine().Split(',');
                lines.Add(Line);
                Row++;
                Console.WriteLine(Row);     //print the array index currently being saved
            }

            var data = lines.ToArray();

            for (int i = 0; i < Row; i++) {
                Console.WriteLine(data[i]);
            }

        }
    }
}
