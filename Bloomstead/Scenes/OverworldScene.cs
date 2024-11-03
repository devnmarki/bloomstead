using Bloomstead.Bloomstead;
using LumiEngine;
using LumiEngine.LevelEditor;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TiledSharp;
// ReSharper disable CheckNamespace

namespace Bloomstead;

public class OverworldScene : Scene
{
    private TilemapManager _tilemapManager;
    
    public override void Start()
    {
        base.Start();
        
        _tilemapManager = new TilemapManager(Assets.Maps.Overworld, Assets.Tilesets.Overworld);
    }

    public override void Render()
    {
        base.Render();
        
        _tilemapManager.Draw(Vector2.Zero, 0.99f);
    }
}