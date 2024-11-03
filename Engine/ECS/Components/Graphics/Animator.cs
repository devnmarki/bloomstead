using System;
using System.Collections.Generic;

namespace LumiEngine;

public class Animator : Component
{
    private SpriteRenderer _sr;

    public Dictionary<string, Animation> Animations { get; set; } = new Dictionary<string, Animation>();

    public Animation CurrentAnimation { get; set; }
    
    public override void OnStart()
    {
        base.OnStart();

        _sr = GameObject.GetComponent<SpriteRenderer>();
    }

    public void AddAnimation(string animationName, Animation animation)
    {
        Animations.Add(animationName, animation);
    }

    public void PlayAnimation(string animationName)
    {
        Animation newAnimation = Animations[animationName];
        if (CurrentAnimation != newAnimation)
        {
            CurrentAnimation = newAnimation;
            CurrentAnimation?.Reset();
        }
    }

    public bool IsCurrentAnimationFinsihed()
    {
        return CurrentAnimation != null && CurrentAnimation.HasFinished;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        CurrentAnimation?.Play();
    }

    public override void OnDraw()
    {
        base.OnDraw();

        if (CurrentAnimation == null) return;
        
        if (GameObject.HasComponent<SpriteRenderer>())
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>();
            sr.DrawAnimation(CurrentAnimation.Spritesheet, CurrentAnimation.Spritesheet.Sprites[CurrentAnimation.Frames[CurrentAnimation.CurrentFrame]]);
        }
    }
    
    public Animation GetAnimation(string animationName)
    {
        return Animations[animationName];
    }
}