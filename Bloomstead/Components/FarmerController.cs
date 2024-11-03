using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Bloomstead.Bloomstead.Game_Objects;
using Bloomstead.Bloomstead.Game_Objects.Tools;
using LumiEngine;
using LumiEngine.Input;
using LumiEngine.LevelEditor;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Bloomstead.Bloomstead.Components;

public class FarmerController : Component
{
    // Components
    private Rigidbody _rb;
    private Animator _anim;
    
    // Movement
    private Vector2 _input = Vector2.Zero;
    private float _moveSpeed = 300f;
    private Directions _dir = Directions.Down;

    // Hitbox
    private Hitbox _hitbox;
    private Vector2 _hitboxPos;
    private TilemapManager _tilemapManager;
    
    // Farming
    private ButtonState _previousLeftMouseButtonState = ButtonState.Released;
    private List<Soil> _soilTiles = new List<Soil>();
    private bool _isGathering = false;
    private Hoe _hoe;

    public FarmerController(TilemapManager tilemapManager)
    {
        _tilemapManager = tilemapManager;
    }
    
    public override void OnStart()
    {
        base.OnStart();
        
        _rb = GameObject.GetComponent<Rigidbody>();
        _anim = GameObject.GetComponent<Animator>();
        
        _anim.PlayAnimation("idle_down");

        _hitbox = new Hitbox();
        SceneManager.CurrentScene.AddGameObject(_hitbox);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        HandleInputs();
        Move();
        HandleHitbox();
        HandleAnimations();

        if (_isGathering)
        {
            _rb.Velocity = Vector2.Zero;
        }
        else
        {
            if (_hoe != null)
                SceneManager.CurrentScene.RemoveGameObject(_hoe);
        }
    }

    private void HandleInputs()
    {
        KeyboardHandler.GetState();
        
        if (KeyboardHandler.IsDown(Keys.W))
        {
            _input.Y = -1f;
            _dir = Directions.Up;
        }
        else if (KeyboardHandler.IsDown(Keys.S))
        {
            _input.Y = 1f;
            _dir = Directions.Down;
        }
        else
        {
            _input.Y = 0f;
        }

        if (KeyboardHandler.IsDown(Keys.A))
        {
            _input.X = -1f;
            _dir = Directions.Left;
        }
        else if (KeyboardHandler.IsDown(Keys.D))
        {
            _input.X = 1f;
            _dir = Directions.Right;
        }
        else
        {
            _input.X = 0f;
        }
        
        MouseState currentMouseState = Mouse.GetState();

        if (_previousLeftMouseButtonState == ButtonState.Released && currentMouseState.LeftButton == ButtonState.Pressed)
        {
            if (!_hitbox.Valid) return;
            
            CreateSoil();
        }

        _previousLeftMouseButtonState = currentMouseState.LeftButton;
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

    private void HandleHitbox()
    {
        float hitboxRange = 31 * Config.GameScale;

        float mousePosX = Mouse.GetState().X;
        float mousePosY = Mouse.GetState().Y;

        Vector2 worldMousePos = new Vector2(
            mousePosX + Config.CameraX,
            mousePosY + Config.CameraY
        );

        Vector2 farmerPosition = GameObject.Transform.Position;
        float distance = Vector2.Distance(farmerPosition, worldMousePos);

        Vector2 snappedPosition = new Vector2(
            (float)Math.Floor(worldMousePos.X / (16 * Config.GameScale)) * (16 * Config.GameScale),
            (float)Math.Floor(worldMousePos.Y / (16 * Config.GameScale)) * (16 * Config.GameScale)
        );

        Rectangle validArea = new Rectangle(
            (int)(farmerPosition.X - hitboxRange),
            (int)(farmerPosition.Y - hitboxRange), 
            (int)(hitboxRange * 2),            
            (int)(hitboxRange * 2)                  
        );

        _hitboxPos = snappedPosition;
        _hitbox.Transform.Position = _hitboxPos;
        
        if (validArea.Contains(snappedPosition) && distance <= hitboxRange)
        {
            _hitbox.Valid = true;
        }
        else
        {
            _hitbox.Valid = false;
        }

        _hitbox.GetComponent<SpriteRenderer>().SpriteIndex = _hitbox.Valid ? 0 : 1;
    }

    private void CreateSoil()
    {
        bool soilExists = _soilTiles.Exists(soil => soil.Transform.Position == _hitbox.Transform.Position);

        if (!soilExists)
        {
            Soil soil = new Soil
            {
                Transform =
                {
                    Position = _hitbox.Transform.Position
                }
            };

            _soilTiles.Add(soil);
            SceneManager.CurrentScene.AddGameObject(soil);

            _isGathering = true;

            _hoe = new Hoe() { Transform = { Position = GameObject.Transform.Position } };
            SceneManager.CurrentScene.AddGameObject(_hoe);
            
            HandleGatherAnimations();
        }
    }

    private void HandleAnimations()
    {
        if (_isGathering)
        {
            if (_anim.IsCurrentAnimationFinsihed())
            {
                _isGathering = false;
            }
            else
            {
                return;
            }
        }
        
        HandleIdleAnimations();
        HandleWalkAnimations();
    }

    private void HandleIdleAnimations()
    {
        if (_input.X != 0 || _input.Y != 0) return;

        switch (_dir)
        {
            case Directions.Up:
                _anim.PlayAnimation("idle_up");
                break;
            case Directions.Down:
                _anim.PlayAnimation("idle_down");
                break;
            case Directions.Left:
                _anim.PlayAnimation("idle_left");
                break;
            case Directions.Right:
                _anim.PlayAnimation("idle_right");
                break;
            default:
                _anim.PlayAnimation("idle_down");
                break;
        }
    }
    
    private void HandleWalkAnimations()
    {
        if (_input is { X: 0, Y: 0 }) return;

        switch (_dir)
        {
            case Directions.Up:
                _anim.PlayAnimation("walk_up");
                break;
            case Directions.Down:
                _anim.PlayAnimation("walk_down");
                break;
            case Directions.Left:
                _anim.PlayAnimation("walk_left");
                break;
            case Directions.Right:
                _anim.PlayAnimation("walk_right");
                break;
            default:
                _anim.PlayAnimation("walk_down");
                break;
        }
    }

    private void HandleGatherAnimations()
    {
        Vector2 directionToHitbox = _hitbox.Transform.Position - GameObject.Transform.Position;

        if (Math.Abs(directionToHitbox.X) > Math.Abs(directionToHitbox.Y))
        {
            _anim.PlayAnimation(directionToHitbox.X >= 0 ? "gather_right" : "gather_left");
        }
        else
        {
            _anim.PlayAnimation(directionToHitbox.Y > 0 ? "gather_down" : "gather_up");
        }
    }
}