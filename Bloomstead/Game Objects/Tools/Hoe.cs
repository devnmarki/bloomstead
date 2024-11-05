using System;
using Bloomstead.Bloomstead.Components.Tools;
using LumiEngine;
using Microsoft.Xna.Framework;

namespace Bloomstead.Bloomstead.Game_Objects.Tools;

public class Hoe : GameObject
{
    protected override void Init()
    {
        base.Init();

        Tag = "tool";
        Name = "Hoe";
        
        Transform.Scale = new Vector2(Config.GameScale);
        
        AddComponent(new SpriteRenderer(Assets.Spritesheets.Tools.Hoe, 8));
        AddComponent(new HoeController(SceneManager.CurrentScene.FindGameObjectByTag("farmer") as Farmer));
    }
}