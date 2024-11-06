using LumiEngine;

namespace Bloomstead.Bloomstead.Game_Objects;

public class GameManager : GameObject
{
    protected override void Init()
    {
        base.Init();

        Tag = "manager";
        
        AddComponent(new DayNightCycle());
    }
}