using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LumiEngine;

public static class Config
{
    public static SpriteBatch Batch { get; set; }
    public static ContentManager Content { get; set; }
    public static GraphicsDevice Graphics { get; set; }

    public static int WindowWidth { get; set; } = 768;
    public static int WindowHeight { get; set; } = 768;
    public static GameTime Time { get; set; }
    public static float GameScale { get; set; } = 3f;
    public static Vector2 GravityScale { get; set; } = new Vector2(0f, 30f);
    public static float CameraX { get; set; } = 0f;
    public static float CameraY { get; set; } = 0f;
    public static bool DebugMode { get; set; } = false;
    
    public static Texture2D PixelTexture { get; private set; }
    
    public static void LoadContent()
    {
        PixelTexture = new Texture2D(Graphics, 1, 1);
        PixelTexture.SetData(new[] { Color.White });
    }
}