using LumiEngine;
using Microsoft.Xna.Framework;

namespace Bloomstead.Bloomstead;

public class InventoryController : Component
{
    private Inventory _inventory;
    
    private int _storage = 10;
    
    public override void OnStart()
    {
        base.OnStart();
        
        _inventory = GameObject as Inventory;

        if (_inventory == null) return;
        
        for (int i = 0; i < _storage; i++)
        {
            Vector2 nextPos = new Vector2(GameObject.Transform.Position.X + (i * (18f * Config.GameScale)), GameObject.Transform.Position.Y);
            InventorySlot slot = new InventorySlot(nextPos);
            
            _inventory.Slots.Add(slot);
            SceneManager.CurrentScene.AddUIElement(slot);
        }
    }
}