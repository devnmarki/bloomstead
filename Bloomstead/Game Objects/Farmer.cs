using Bloomstead.Bloomstead.Components;
using LumiEngine;
using Microsoft.Xna.Framework;

namespace Bloomstead.Bloomstead.Game_Objects;

public class Farmer : GameObject
{
    protected override void Init()
    {
        base.Init();

        Transform.Scale = new Vector2(Config.GameScale);
        
        AddComponent(new SpriteRenderer(Assets.Spritesheets.Character, 8) { LayerDepth = Globals.Layers.Characters, SpriteOffset = new Vector2(11 * Config.GameScale, 9 * Config.GameScale)});
        AddComponent(new BoxCollider(new Vector2(7 * Config.GameScale, 6 * Config.GameScale), new Vector2(1 * Config.GameScale, 7 * Config.GameScale)));
        AddComponent(new Rigidbody() { UseGravity = false });
        AddComponent(new FarmerController());
    }
}