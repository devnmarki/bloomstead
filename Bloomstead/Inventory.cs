using System.Collections.Generic;
using Bloomstead.Bloomstead.Game_Objects.Items;
using LumiEngine;
using LumiEngine.UI;

namespace Bloomstead.Bloomstead;

public class Inventory : GameObject
{
    public List<Item> Items { get; set; } = new List<Item>();
    public List<InventorySlot> Slots { get; set; } = new List<InventorySlot>();
    
    protected override void Init()
    {
        base.Init();

        Tag = "inventory";
        Name = "Inventory";
        
        AddComponent(new InventoryController());
    }
}