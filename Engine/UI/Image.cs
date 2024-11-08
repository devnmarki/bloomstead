using Bloomstead.Bloomstead;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LumiEngine.UI;

public class Image : GameObject
{
    public Texture2D Texture { get; set; }
    public Spritesheet Spritesheet { get; set; }
    public int SpriteIndex { get; set; }

    public Image(Texture2D texture) : base()
    {
        Texture = texture;
        Spritesheet = null;
        SpriteIndex = 0;
        Init();
    }
    
    public Image(Spritesheet spritesheet, int spriteIndex) : base()
    {
        Texture = spritesheet.Texture;
        Spritesheet = spritesheet;
        SpriteIndex = spriteIndex;
        Init();
    }
    
    protected override void Init()
    {
        base.Init();
        
        Transform.Scale = new Vector2(Config.GameScale + 2f);

        if (Texture != null)
        {
            AddComponent(new UIRenderer(Texture));
        } 
        else if (Spritesheet != null)
        {
            AddComponent(new UIRenderer(Spritesheet, SpriteIndex));
        }
    }
}