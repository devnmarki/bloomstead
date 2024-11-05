using System;
using Bloomstead.Bloomstead;
using Bloomstead.Bloomstead.Game_Objects;
using LumiEngine;
using LumiEngine.LevelEditor;
using Microsoft.Xna.Framework;

namespace Bloomstead;

public class OverworldScene : Scene
{
    private TilemapManager _tilemapManager;
    
    public override void Start()
    {
        base.Start();
        
        _tilemapManager = new TilemapManager(Assets.Maps.Overworld, Assets.Textures.Tilesets.Overworld);
        _tilemapManager.LoadGameObjects();
        _tilemapManager.CreateColliders(Vector2.Zero);
        
        if (FindGameObjectByTag("farmer") is Farmer farmer)
            farmer.TilemapManager = _tilemapManager;
    }

    public override void Update()
    {
        base.Update();
        
        if (FindGameObjectByTag("farmer") is Farmer farmer)
        {
            Config.CameraX += (farmer.Transform.Position.X - Config.CameraX - (Config.WindowWidth / 2f) - (4 * Config.GameScale / 2f));
            Config.CameraY += (farmer.Transform.Position.Y - Config.CameraY - (Config.WindowHeight / 2f) - (4 * Config.GameScale / 2f));
        }
        
        if (Config.CameraX <= 0)
            Config.CameraX = 0;
        else if (Config.CameraX >= Assets.Maps.Overworld.Width * Config.GameScale + (Config.WindowWidth / 19f))
            Config.CameraX = Assets.Maps.Overworld.Width * Config.GameScale + (Config.WindowWidth / 19f);
        
        if (Config.CameraY <= 0)
            Config.CameraY = 0;
        else if (Config.CameraY >= Assets.Maps.Overworld.Height * Config.GameScale + (Config.WindowHeight / 4f))
            Config.CameraY = (Assets.Maps.Overworld.Height * Config.GameScale) + (Config.WindowHeight / 4f);
    }

    public override void Render()
    {
        base.Render();
        
        _tilemapManager.Draw("Tiles", Vector2.Zero, 1f);
    }
}