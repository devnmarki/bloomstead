using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LumiEngine.UI;

public class UIRenderer : Component
{
    private Texture2D _texture = null;
    
    private bool _flip = false;
    private float _layerDepth = 0f;

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
    }
    
    public override void OnDraw()
    {
        base.OnDraw();
        
        if (_texture != null)
        {
            DrawImage();
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
}