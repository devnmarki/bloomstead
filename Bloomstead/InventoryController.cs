using System;
using System.Collections.Generic;
using LumiEngine;
using LumiEngine.UI;
using Microsoft.Xna.Framework;

namespace Bloomstead.Bloomstead;

public class InventoryController : Component
{
    private int _storage = 10;
    private List<Image> _slots = new List<Image>();
    
    public override void OnStart()
    {
        base.OnStart();
        
        for (int i = 0; i < _storage; i++)
        {
            Vector2 nextPos = new Vector2(GameObject.Transform.Position.X + (i * (18f * Config.GameScale)), GameObject.Transform.Position.Y);
            Image slotImage = new Image(Assets.Textures.UI.InventorySlot)
            {
                Transform =
                {
                    Position = nextPos
                }
            };
            
            _slots.Add(slotImage);
            SceneManager.CurrentScene.AddUIElement(slotImage);
        }
    }
}