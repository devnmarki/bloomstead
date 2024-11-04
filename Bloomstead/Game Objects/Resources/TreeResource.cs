using System;
using LumiEngine;
using Microsoft.Xna.Framework;

namespace Bloomstead.Bloomstead.Game_Objects.Resources;

public class TreeResource : Resource
{
    protected override void Init()
    {
        base.Init();

        Name = "Tree";

        Health = 5;
        
        AddComponent(new SpriteRenderer(Assets.Textures.Resources.Tree) { SpriteOffset = new Vector2(8 * Config.GameScale, 20 * Config.GameScale) });
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        
        Console.WriteLine("Tree destroyed");
        DestroyGameObject(this);
    }
}