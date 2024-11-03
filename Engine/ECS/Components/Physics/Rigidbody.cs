using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace LumiEngine;

public class Rigidbody : Component
{
    public Vector2 Velocity = Vector2.Zero;
    public float Mass { get; set; } = 1f;
    public bool UseGravity { get; set; } = true;

    public Dictionary<string, Action<GameObject>> CollisionActions { get; set; }
    public Action<GameObject> OnCollision { get; set; }

    public List<Type> CollisionIgnoreList { get; set; } = new List<Type>();

    public Rigidbody(float mass = 1f, bool useGravity = true)
    {
        Mass = mass;
        UseGravity = useGravity;

        CollisionActions = new Dictionary<string, Action<GameObject>>()
        {
            { "horizontal", other => {} },
            { "vertical", other => {} },
            { "top", other => {} },
            { "bottom", other => {} },
            { "left", other => {} },
            { "right", other => {} },
        };
    }

    public void ApplyForce(Vector2 force)
    {
        // F = ma -> a = F / m
        Vector2 acceleration = force / Mass;
        Velocity += acceleration;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (UseGravity)
        {
            ApplyForce(Config.GravityScale * Mass);
        }
        
        GameObject.Transform.Position.X += Velocity.X * (float)Config.Time.ElapsedGameTime.TotalSeconds;
        CheckCollision(Axis.Horizontal);
        
        GameObject.Transform.Position.Y += Velocity.Y * (float)Config.Time.ElapsedGameTime.TotalSeconds;
        CheckCollision(Axis.Vertical);
        
        Velocity *= 0.98f;
    }

    private void CheckCollision(Axis axis)
    {
        var gameObjects = new List<GameObject>(SceneManager.CurrentScene.GameObjects);
        foreach (var gameObject in gameObjects)
        {
            if (gameObject != GameObject)
            {
                foreach (var collider in GameObject.GetColliders())
                {
                    foreach (var otherCollider in gameObject.GetColliders())
                    {
                        if (collider.CheckCollision(otherCollider))
                        {
                            HandleCollision(collider, otherCollider, axis);

                            OnCollision?.Invoke(otherCollider.GameObject);
                        }
                    }
                }
            }
        }
    }
    
    private void HandleCollision(BoxCollider current, BoxCollider other, Axis axis)
    {
        bool isIgnoredType = CollisionIgnoreList.Contains(other.GameObject.GetType());
    
        if (axis == Axis.Horizontal)
        {
            if (current.GetBounds().Right >= other.GetBounds().Left && current.GetBounds().Left <= other.GetBounds().Left)
            {
                if (!isIgnoredType)
                    GameObject.Transform.Position.X = other.GameObject.Transform.Position.X - current.Size.X - current.Offset.X + other.Offset.X;
                
                other.CollisionSide = Sides.Left;
                CollisionActions["right"](other.GameObject);
            }

            if (current.GetBounds().Left <= other.GetBounds().Right && current.GetBounds().Right >= other.GetBounds().Right)
            {
                if (!isIgnoredType)
                    GameObject.Transform.Position.X = other.GameObject.Transform.Position.X + other.Size.X - current.Offset.X + other.Offset.X;
                
                other.CollisionSide = Sides.Right;
                CollisionActions["left"](other.GameObject);
            }
    
            if (!isIgnoredType)
                CollisionActions["horizontal"](other.GameObject);
        }
        else
        {
            if (current.GetBounds().Bottom >= other.GetBounds().Top && current.GetBounds().Top <= other.GetBounds().Top - 0.1f)
            {
                if (!isIgnoredType)
                    GameObject.Transform.Position.Y = other.GameObject.Transform.Position.Y - current.Size.Y - current.Offset.Y + other.Offset.Y - 0.1f;
                
                other.CollisionSide = Sides.Top;
                CollisionActions["bottom"](other.GameObject);
            }

            if (current.GetBounds().Top <= other.GetBounds().Bottom && current.GetBounds().Bottom >= other.GetBounds().Bottom)
            {
                if (!isIgnoredType)
                    GameObject.Transform.Position.Y = other.GameObject.Transform.Position.Y + other.Size.Y - current.Offset.Y + other.Offset.Y;
                
                other.CollisionSide = Sides.Bottom;
                CollisionActions["top"](other.GameObject);
            }

            if (!isIgnoredType)
            {
                Velocity.Y = 0f;
                CollisionActions["vertical"](other.GameObject);
            }
        }
    }

}