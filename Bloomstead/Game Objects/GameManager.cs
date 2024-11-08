using LumiEngine;
using Microsoft.Xna.Framework;

namespace Bloomstead.Bloomstead.Game_Objects;

public class GameManager : GameObject
{
    protected override void Init()
    {
        base.Init();

        Tag = "manager";
        
        AddComponent(new DayNightCycle());

        float inventoryX = Config.WindowWidth / 2f - 10f * (18f * Config.GameScale) / 2f;
        float inventoryY = Config.WindowHeight - (32f * Config.GameScale);
        SceneManager.CurrentScene.AddGameObject(new Inventory { Transform = { Position = new Vector2(inventoryX, inventoryY) }});
    }
}