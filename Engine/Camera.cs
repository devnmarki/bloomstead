using Microsoft.Xna.Framework;

namespace LumiEngine;

public class Camera
{
    private Vector2 _position = Vector2.Zero;

    public Vector2 Position
    {
        get => _position;
        set => _position = value;
    }

    
    public Matrix GetTransformation()
    {
        return Matrix.CreateTranslation(-Position.X, -Position.Y, 0f);
    }
}