using Bloomstead.Bloomstead.Game_Objects;
using LumiEngine;
using Microsoft.Xna.Framework;

namespace Bloomstead.Bloomstead.Components.Tools;

public class HoeController : Component
{
    private Farmer _farmer;
    
    public HoeController(Farmer farmer)
    {
        _farmer = farmer;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        
        Sync();
    }

    private void Sync()
    {
        Animator anim = _farmer.GetComponent<Animator>();
        
        SpriteRenderer hoeRenderer = GameObject.GetComponent<SpriteRenderer>();
        Vector2 hoeOffset = Vector2.Zero;
        
        if (anim.CurrentAnimation == anim.GetAnimation("gather_right"))
        {
            hoeRenderer.SpriteIndex = anim.CurrentAnimation.CurrentFrame == 0 ? 0 : 1;
            hoeOffset = new Vector2(anim.CurrentAnimation.CurrentFrame == 0 ? -11 : 7, anim.CurrentAnimation.CurrentFrame == 0 ? -14 : 3);
        }
        else if (anim.CurrentAnimation == anim.GetAnimation("gather_left"))
        {
            hoeRenderer.SpriteIndex = anim.CurrentAnimation.CurrentFrame == 0 ? 2 : 3;
            hoeOffset = new Vector2(anim.CurrentAnimation.CurrentFrame == 0 ? 6 : -15, anim.CurrentAnimation.CurrentFrame == 0 ? -14 : 4);
        }
        else if (anim.CurrentAnimation == anim.GetAnimation("gather_down"))
        {
            hoeRenderer.LayerDepth = anim.CurrentAnimation.CurrentFrame == 0 ? 120 : 0;
            hoeRenderer.SpriteIndex = anim.CurrentAnimation.CurrentFrame == 0 ? 4 : 5;
            hoeOffset = new Vector2(anim.CurrentAnimation.CurrentFrame == 0 ? -6 : -5, anim.CurrentAnimation.CurrentFrame == 0 ? -13 : 3);
        }
        else if (anim.CurrentAnimation == anim.GetAnimation("gather_up"))
        {
            hoeRenderer.LayerDepth = 120;
            hoeRenderer.SpriteIndex = anim.CurrentAnimation.CurrentFrame == 0 ? 6 : 7;
            hoeOffset = new Vector2(0, anim.CurrentAnimation.CurrentFrame == 0 ? -11 : 0);
        }
        
        GameObject.Transform.Position = _farmer.Transform.Position + hoeOffset * Config.GameScale;
    }
}