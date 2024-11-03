using System;
using System.Numerics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace LumiEngine;

public class SpriteRenderer : Component
{
    private Texture2D _sprite = null;
    
    private bool _flip = false;
    private float _layerDepth = 0f;
    
    private Spritesheet _spritesheet = null;
    private int _spriteIndex = 0;
    
    public bool Flip
    {
        get => _flip;
        set => _flip = value;
    }

    public float LayerDepth
    {
        get => _layerDepth;
        set => _layerDepth = value;
    }

    public Spritesheet Spritesheet
    {
        get => _spritesheet;
        set => _spritesheet = value;
    }

    public int SpriteIndex
    {
        get => _spriteIndex;
        set => _spriteIndex = value;
    }
    
    public Vector2 SpriteOffset { get; set; } = Vector2.Zero;
    
    public SpriteRenderer(Texture2D sprite)
    {
        _sprite = sprite;
        _spritesheet = null;
    }

    public SpriteRenderer(Spritesheet spritesheet, int sprite)
    {
        _spritesheet = spritesheet;
        _spriteIndex = sprite;
        _sprite = null;
    }
    
    public override void OnDraw()
    {
        base.OnDraw();

        if (GameObject.HasComponent<Animator>()) return;
        
        if (_sprite != null)
        {
            DrawSingleSprite();
        }
        else if (_spritesheet != null && _spriteIndex >= 0 && _spriteIndex < _spritesheet.Sprites.Count)
        {
            DrawFromSpritesheet();
        }
    }

    private void DrawSingleSprite()
    {
        Config.Batch.Draw(
            _sprite,
            GameObject.Transform.Position - new Vector2(Config.CameraX, Config.CameraY) - SpriteOffset,
            null,
            Color.White,
            0f,
            Vector2.Zero,
            GameObject.Transform.Scale,
            _flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
            _layerDepth / 1000f);
    }

    private void DrawFromSpritesheet()
    {
        Config.Batch.Draw(
            _spritesheet.Texture,
            GameObject.Transform.Position - new Vector2(Config.CameraX, Config.CameraY) - SpriteOffset,
            _spritesheet.Sprites[_spriteIndex],
            Color.White,
            0f,
            Vector2.Zero,
            GameObject.Transform.Scale,
            _flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
            _layerDepth / 1000f);
    }
    
    public void DrawAnimation(Spritesheet spritesheet, Rectangle sprite)
    {
        Config.Batch.Draw(
            spritesheet.Texture,
            GameObject.Transform.Position - new Vector2(Config.CameraX, Config.CameraY) - SpriteOffset,
            sprite,
            Color.White,
            0f,
            Vector2.Zero,
            GameObject.Transform.Scale,
            _flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
            _layerDepth / 1000f);
    }
}