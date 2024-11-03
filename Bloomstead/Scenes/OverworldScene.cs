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
        
        _tilemapManager = new TilemapManager(Assets.Maps.Overworld, Assets.Tilesets.Overworld);
        _tilemapManager.LoadGameObjects();
    }

    public override void Update()
    {
        base.Update();

        if (FindGameObjectByTag("farmer") is Farmer farmer)
        {
            Config.CameraX += (farmer.Transform.Position.X - Config.CameraX - (Config.WindowWidth / 2f) - (16 * Config.GameScale / 2f));
            Config.CameraY += (farmer.Transform.Position.Y - Config.CameraY - (Config.WindowHeight / 2f) - (16 * Config.GameScale / 2f));
        }
    }

    public override void Render()
    {
        base.Render();
        
        _tilemapManager.Draw(Vector2.Zero, 0.75f);
    }
}