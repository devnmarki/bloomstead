using LumiEngine;
using Microsoft.Xna.Framework;

namespace Bloomstead.Bloomstead;

public class DayNightCycle : Component
{
    private const float FullDayInSeconds = 480f;
    private float _currentTimeOfDay = 0f;

    private float _morningEndTime = FullDayInSeconds * 0.25f; // Morning ends at 25% of the day
    private float _noonEndTime = FullDayInSeconds * 0.5f;     // Noon ends at 50% of the day
    private float _eveningEndTime = FullDayInSeconds * 0.65f; // Evening ends at 75% of the day
    
    private Color _noonColor = Color.White;              
    private Color _eveningColor = new Color(255, 183, 76); 
    private Color _nightColor = new Color(50, 50, 100);   

    public Color CurrentColor { get; private set; }

    private void UpdateDayCycle(float deltaTime)
    {
        _currentTimeOfDay += deltaTime;

        if (_currentTimeOfDay >= FullDayInSeconds)
        {
            Globals.CurrentDay++;
            _currentTimeOfDay = 0f;
        }

        UpdateDayPhaseColor();
    }

    private void UpdateDayPhaseColor()
    {
        if (IsMorning())
        {
            float t = _currentTimeOfDay / _morningEndTime;
            CurrentColor = Color.Lerp(_noonColor, _noonColor, t);
        }
        else if (IsNoon())
        {
            float t = (_currentTimeOfDay - _morningEndTime) / (_noonEndTime - _morningEndTime);
            CurrentColor = Color.Lerp(_noonColor, _noonColor, t);
        }
        else if (IsEvening())
        {
            float t = (_currentTimeOfDay - _noonEndTime) / (_eveningEndTime - _noonEndTime);
            CurrentColor = Color.Lerp(_noonColor, _eveningColor, t);
        }
        else
        {
            float t = (_currentTimeOfDay - _eveningEndTime) / (FullDayInSeconds - _eveningEndTime);
            CurrentColor = Color.Lerp(_eveningColor, _nightColor, t);
        }
    }

    private bool IsMorning() => _currentTimeOfDay < _morningEndTime;
    private bool IsNoon() => _currentTimeOfDay >= _morningEndTime && _currentTimeOfDay < _noonEndTime;
    private bool IsEvening() => _currentTimeOfDay >= _noonEndTime && _currentTimeOfDay < _eveningEndTime;
    private bool IsNight() => _currentTimeOfDay >= _eveningEndTime;

    public override void OnUpdate()
    {
        UpdateDayCycle((float)Config.Time.ElapsedGameTime.TotalSeconds);
    }
}