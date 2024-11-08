using LumiEngine;
using LumiEngine.UI;
using Microsoft.Xna.Framework;

namespace Bloomstead.Bloomstead;

public class InventorySlot : Image
{
    private Vector2 _position;

    public bool IsFull { get; set; } = false;
    
    public InventorySlot(Vector2 position) : base(Assets.Textures.UI.InventorySlot)
    {
        _position = position;
        Init();
    }
    
    protected override void Init()
    {
        base.Init();
        
        Transform.Position = _position;
    }
}