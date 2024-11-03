using Bloomstead.Bloomstead.Components;
using LumiEngine;
using LumiEngine.LevelEditor;
using Microsoft.Xna.Framework;

namespace Bloomstead.Bloomstead.Game_Objects;

public class Farmer : GameObject
{
    public TilemapManager TilemapManager { get; set; }
    
    protected override void Init()
    {
        base.Init();

        Tag = "farmer";
        Name = "Farmer";
        
        Transform.Scale = new Vector2(Config.GameScale);
        
        AddComponents();
        LoadAnimations();
    }

    private void AddComponents()
    {
        AddComponent(new SpriteRenderer(Assets.Spritesheets.Character, 8) { LayerDepth = Globals.Layers.Characters, SpriteOffset = new Vector2(11 * Config.GameScale, 9 * Config.GameScale)});
        AddComponent(new Animator());
        AddComponent(new BoxCollider(new Vector2(7 * Config.GameScale, 6 * Config.GameScale), new Vector2(1 * Config.GameScale, 7 * Config.GameScale)));
        AddComponent(new Rigidbody() { UseGravity = false });
        AddComponent(new FarmerController(TilemapManager));
    }

    private void LoadAnimations()
    {
        var anim = GetComponent<Animator>();
        
        anim.AddAnimation("idle_right", new Animation(Assets.Spritesheets.Character, new int[] { 0, 1, 2 }, 0.15f));
        anim.AddAnimation("idle_left", new Animation(Assets.Spritesheets.Character, new int[] { 4, 5, 6 }, 0.15f));
        anim.AddAnimation("idle_down", new Animation(Assets.Spritesheets.Character, new int[] { 8, 9, 10 }, 0.15f));
        anim.AddAnimation("idle_up", new Animation(Assets.Spritesheets.Character, new int[] { 12, 13, 14 }, 0.15f));
        
        anim.AddAnimation("walk_right", new Animation(Assets.Spritesheets.Character, new int[] { 16, 17, 18, 19 }, 0.15f));
        anim.AddAnimation("walk_left", new Animation(Assets.Spritesheets.Character, new int[] { 20, 21, 22, 23 }, 0.15f));
        anim.AddAnimation("walk_down", new Animation(Assets.Spritesheets.Character, new int[] { 24, 25, 26, 27 }, 0.15f));  
        anim.AddAnimation("walk_up", new Animation(Assets.Spritesheets.Character, new int[] { 28, 29, 30, 31 }, 0.15f));
    }
}