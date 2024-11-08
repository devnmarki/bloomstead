using LumiEngine;
using Microsoft.Xna.Framework;

namespace Bloomstead.Bloomstead.Game_Objects.Objects;

public class Crop : GameObject
{
    private CropModel.Crop _model;

    public Crop(CropModel.Crop model) : base()
    {
        _model = model;
        Init();
    }

    protected override void Init()
    {
        base.Init();

        if (_model == null) return;
        
        Tag = "crops";
        Name = _model.Name;
        
        Transform.Scale = new Vector2(Config.GameScale);
        
        AddComponent(new SpriteRenderer(_model.Spritesheet, 0) { LayerDepth = Globals.Layers.Characters });
    }
}