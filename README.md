# GraphicsEngine
### C# Graphics engine using GDI

I took the messy code from the LD31 game I made and turned it into an engine. Based of GDI and GDI+ Nothing too fancy.

The aim is that the engine will hold all the files and do all the logic for the majority of the game. Just change the file references and have fun!

The solution has 3 projects:

- **GE_Core** - The core engine code outside of the rendering. All the major game logic will take place here

- **GE_Console** - The code for the "cheat" console. Allows developers to easily change variables without recompiling

- **GraphicsEngine** - An example game project and the render code


### Still to do:

1. Map reading from a CSV

2. ~~"Cheat" console~~

3. Move from procedural code to a more class based object orientation code. Especially within the GE_Core code base.


###Maps

The maps can be created using CSV where each integer value will represent a pre-designed tile. For example:

Contains integers to represent a predesigned tile
1 = Grass

2 = Wall

3 = Path

4 = Flowers

5 = Wooden floor


5x5 map will read like:

     2, 3, 2, 2, 2
     
     2, 3, 3, 1, 2
     
     2, 4, 3, 4, 2
     
     2, 4, 3, 4, 2
     
     2, 2, 3, 2, 2

