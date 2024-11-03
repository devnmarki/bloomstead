using LumiEngine;
using Microsoft.Xna.Framework;

namespace Bloomstead.Bloomstead.Game_Objects;

public class Hitbox : GameObject
{
    public bool Valid { get; set; } = true;
    
    protected override void Init()
    {
        base.Init();

        Tag = "hitbox";
        Name = "Hitbox";

        Transform.Scale = new Vector2(Config.GameScale);
        
        AddComponent(new SpriteRenderer(Assets.Spritesheets.Hitbox, 0) { LayerDepth = 101 });
    }
}