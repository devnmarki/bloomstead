using System;
using LumiEngine;
using Microsoft.Xna.Framework;

namespace Bloomstead.Bloomstead.Game_Objects.Resources;

public class TreeShakeEffect : Component
{
    private Vector2 _originalPosition;
    private float _shakeIntensity = 2f;
    private float _shakeFrequency = 10f;
    private float _shakeDuration = 0.3f;
    private float _timer;
    private bool _isShaking;

    public void StartShake()
    {
        _originalPosition = GameObject.Transform.Position;
        _timer = _shakeDuration;
        _isShaking = true;
    }
    
    public override void OnUpdate()
    {
        base.OnUpdate();

        if (_isShaking)
        {
            _timer -= (float)Config.Time.ElapsedGameTime.TotalSeconds;
            if (_timer > 0)
            {
                float shakeOffset = (float)Math.Sin(_timer * _shakeFrequency) * _shakeIntensity;
                GameObject.Transform.Position = _originalPosition + new Vector2(shakeOffset, 0);
            }
            else
            {
                GameObject.Transform.Position = _originalPosition;
                _isShaking = false;
            }
        }
    }
}