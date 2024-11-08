using System.Collections.Generic;
using LumiEngine;
using LumiEngine.UI;

namespace Bloomstead.Bloomstead;

public class Inventory : GameObject
{
    protected override void Init()
    {
        base.Init();

        Tag = "inventory";
        Name = "Inventory";
        
        AddComponent(new InventoryController());
    }
}