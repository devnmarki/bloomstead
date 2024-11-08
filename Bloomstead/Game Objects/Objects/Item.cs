using LumiEngine;
using Microsoft.Xna.Framework;

namespace Bloomstead.Bloomstead.Game_Objects.Items;

public class Item : GameObject
{
    public ItemModel.Item Model { get; }
    public bool IsPickedUp { get; set; } = false;

    public Item(ItemModel.Item model) : base()
    {
        Model = model;
        Init();
    }
    
    protected override void Init()
    {
        base.Init();

        if (Model == null) return;
        
        Name = Model.Name;
        
        Transform.Scale = new Vector2(Config.GameScale - 1f);
        
        AddComponent(new SpriteRenderer(Model.Spritesheet, Model.Sprite) { LayerDepth = Globals.Layers.SoilTiles });
        AddComponent(new BoxCollider(new Vector2(8f * (Config.GameScale - 1f)), new Vector2(4f * Config.GameScale)));
    }
}