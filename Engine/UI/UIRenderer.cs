using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LumiEngine.UI;

public class UIRenderer : Component
{
    private Texture2D _texture;
    private Spritesheet _spritesheet;
    private int _spriteIndex;
    
    private bool _flip = false;
    private float _layerDepth = 999f;

    private Color _currentColor = Color.White;
    
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
    
    public UIRenderer(Texture2D texture)
    {
        _texture = texture;
        _spritesheet = null;
        _spriteIndex = 0;
    }

    public UIRenderer(Spritesheet spritesheet, int spriteIndex)
    {
        _spritesheet = spritesheet;
        _texture = null;
        _spriteIndex = spriteIndex;
    }
    
    public override void OnDraw()
    {
        base.OnDraw();
        
        if (_texture != null)
        {
            DrawImage();
        }
        else if (_spritesheet != null)
        {
            DrawSpritesheetImage();
        }
    }

    private void DrawImage()
    {
        Config.Batch.Draw(
            _texture,
            GameObject.Transform.Position,
            null,
            _currentColor,
            0f,
            Vector2.Zero,
            GameObject.Transform.Scale,
            _flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
            _layerDepth / 1000f);
    }

    private void DrawSpritesheetImage()
    {
        Config.Batch.Draw(
            _spritesheet.Texture,
            GameObject.Transform.Position,
            _spritesheet.Sprites[_spriteIndex],
            _currentColor,
            0f,
            Vector2.Zero,
            GameObject.Transform.Scale,
            _flip ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
            _layerDepth / 1000f);
    }
}