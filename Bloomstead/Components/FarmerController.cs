using System;
using LumiEngine;
using LumiEngine.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Bloomstead.Bloomstead.Components;

public class FarmerController : Component
{
    // Components
    private Rigidbody _rb;
    
    private Vector2 _input = Vector2.Zero;
    private float _moveSpeed = 300f;
    
    public override void OnStart()
    {
        base.OnStart();
        
        _rb = GameObject.GetComponent<Rigidbody>();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        
        HandleInputs();
        Move();
    }

    private void HandleInputs()
    {
        KeyboardHandler.GetState();
        
        if (KeyboardHandler.IsDown(Keys.W))
            _input.Y = -1f;
        else if (KeyboardHandler.IsDown(Keys.S))
            _input.Y = 1f;
        else
            _input.Y = 0f;
        
        if (KeyboardHandler.IsDown(Keys.A))
            _input.X = -1f;
        else if (KeyboardHandler.IsDown(Keys.D))
            _input.X = 1f;
        else
            _input.X = 0f;
    }
    
    private void Move()
    {
        if (_input.X != 0 && _input.Y != 0)
        {
            _input.X *= 0.7f;
            _input.Y *= 0.7f;
        }
        
        _rb.Velocity = new Vector2(_input.X * _moveSpeed, _input.Y * _moveSpeed);
    }
}