using System;
using Bloomstead.Bloomstead.Game_Objects.Items;
using LumiEngine;
using Microsoft.Xna.Framework;

namespace Bloomstead.Bloomstead.Game_Objects.Resources;

public class TreeResource : Resource
{
    private int _maxDropCount = 5;
    
    protected override void Init()
    {
        base.Init();

        Name = "Tree";

        Health = 5;
        
        AddComponent(new SpriteRenderer(Assets.Textures.Resources.Tree) { LayerDepth = 100, SpriteOffset = new Vector2(8 * Config.GameScale, 20 * Config.GameScale) });
        AddComponent(new BoxCollider(new Vector2(8 * Config.GameScale, 6 * Config.GameScale), new Vector2(4 * Config.GameScale, 4 * Config.GameScale)));
        AddComponent(new TreeShakeEffect());
        
        DropLoot.Add(new Item(ItemModel.Models.ModelLog));
        DropLoot.Add(new Item(ItemModel.Models.ModelFiber));
    }

    public override void OnDamage(int amount)
    {
        base.OnDamage(amount);
        
        GetComponent<TreeShakeEffect>().StartShake();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        
        Random rnd = new Random();
        
        int dropCount = rnd.Next(1, _maxDropCount);

        for (int i = 0; i < dropCount; i++)
        {
            double dropX = rnd.NextDouble() * 15f;
            double dropY = rnd.NextDouble() * 15f;
            int itemIndex = rnd.Next(0, DropLoot.Count);

            Instantiate(DropLoot[itemIndex], Transform.Position + new Vector2((float)dropX * (Config.GameScale - 1f), (float)dropY * (Config.GameScale - 1f)));
        }
        
        DestroyGameObject(this);
    }
}