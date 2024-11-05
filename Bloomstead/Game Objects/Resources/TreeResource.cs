using System;
using Bloomstead.Bloomstead.Game_Objects.Items;
using LumiEngine;
using Microsoft.Xna.Framework;

namespace Bloomstead.Bloomstead.Game_Objects.Resources;

public class TreeResource : Resource
{
    protected override void Init()
    {
        base.Init();

        Name = "Tree";

        Health = 5;
        
        AddComponent(new SpriteRenderer(Assets.Textures.Resources.Tree) { LayerDepth = 100, SpriteOffset = new Vector2(8 * Config.GameScale, 20 * Config.GameScale) });
        AddComponent(new BoxCollider(new Vector2(8 * Config.GameScale, 6 * Config.GameScale), new Vector2(4 * Config.GameScale, 4 * Config.GameScale)));
        AddComponent(new TreeShakeEffect());
    }

    public override void OnDamage(int amount)
    {
        base.OnDamage(amount);
        
        GetComponent<TreeShakeEffect>().StartShake();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        
        Instantiate(new Item(ItemModel.Models.ModelLog), Transform.Position + new Vector2(4f * (Config.GameScale - 1f), 4f * (Config.GameScale - 1f)));
        
        DestroyGameObject(this);
    }
}