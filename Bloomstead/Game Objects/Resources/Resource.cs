using System.Collections.Generic;
using Bloomstead.Bloomstead.Game_Objects.Items;
using LumiEngine;
using Microsoft.Xna.Framework;

namespace Bloomstead.Bloomstead.Game_Objects.Resources;

public class Resource : GameObject
{
    public int Health { get; set; } = 1;
    
    protected List<Item> DropLoot { get; } = new List<Item>();
    
    protected override void Init()
    {
        base.Init();

        Tag = "resource";
        
        Transform.Scale = new Vector2(Config.GameScale);
    }

    public virtual void OnDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
            OnDestroy();
    }
    
    protected virtual void OnDestroy()
    {
        
    }
}