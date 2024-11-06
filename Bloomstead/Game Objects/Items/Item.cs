using LumiEngine;
using Microsoft.Xna.Framework;

namespace Bloomstead.Bloomstead.Game_Objects.Items;

public class Item : GameObject
{
    public ItemModel.Item _model;

    public Item(ItemModel.Item model) : base()
    {
        _model = model;
        Init();
    }
    
    protected override void Init()
    {
        base.Init();

        if (_model == null) return;
        
        Name = _model.Name;
        
        Transform.Scale = new Vector2(Config.GameScale - 1f);
        
        AddComponent(new SpriteRenderer(_model.Spritesheet, _model.Sprite) { LayerDepth = Globals.Layers.SoilTiles });
        AddComponent(new BoxCollider(new Vector2(8f * (Config.GameScale - 1f)), new Vector2(4f * Config.GameScale)));
    }
}