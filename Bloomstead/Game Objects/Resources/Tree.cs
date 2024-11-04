using LumiEngine;
using Microsoft.Xna.Framework;

namespace Bloomstead.Bloomstead.Game_Objects.Resources;

public class Tree : GameObject
{
    protected override void Init()
    {
        base.Init();

        Tag = "resource";
        Name = "Tree";

        Transform.Scale = new Vector2(Config.GameScale);
        
        AddComponent(new SpriteRenderer(Assets.Textures.Resources.Tree) { SpriteOffset = new Vector2(8 * Config.GameScale, 20 * Config.GameScale) });
    }
}