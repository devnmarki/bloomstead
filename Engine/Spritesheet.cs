using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LumiEngine;

public class Spritesheet
{
    public Texture2D Texture { get; set; }
    public int Rows { get; set; }
    public int Columns { get; set; }
    public Vector2 SpriteSize { get; set; }
    public List<Rectangle> Sprites = new List<Rectangle>();
    
    public Spritesheet(Texture2D texture, int rows, int columns, Vector2 spriteSize)
    {
        Texture = texture;
        Rows = rows;
        Columns = columns;
        SpriteSize = spriteSize;
        Load(rows, columns, spriteSize);
    }
    
    private void Load(int rows, int columns, Vector2 frameSize)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                // Calculate the position of each sprite frame based on row and column indices
                int xPos = j * (int)frameSize.X;
                int yPos = i * (int)frameSize.Y;

                // Add the rectangle for each frame to the Sprites list
                Sprites.Add(new Rectangle(xPos, yPos, (int)frameSize.X, (int)frameSize.Y));
            }
        }
    }
}