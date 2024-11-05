using LumiEngine;
using Microsoft.Xna.Framework;

namespace Bloomstead.Bloomstead.Game_Objects;

public class Soil : GameObject
{
    protected override void Init()
    {
        base.Init();

        Tag = "tiles";
        Name = "Soil";
        
        Transform.Scale = new Vector2(Config.GameScale);
        
        AddComponent(new SpriteRenderer(Assets.Spritesheets.Soil, 1) { LayerDepth = Globals.Layers.SoilTiles });
    }
}