using Bloomstead.Bloomstead;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LumiEngine.UI;

public class Image : GameObject
{
    public Texture2D Texture { get; set; }

    public Image(Texture2D texture) : base()
    {
        Texture = texture;
        Init();
    }
    
    protected override void Init()
    {
        base.Init();
        
        Transform.Scale = new Vector2(Config.GameScale + 2f);

        if (Texture == null) return;
        
        AddComponent(new UIRenderer(Texture));
    }
}