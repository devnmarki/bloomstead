using System;
using System.Collections.Generic;
using LumiEngine;

namespace LumiEngine;

public class SceneManager
{
    private static Dictionary<string, Scene> _scenes = new Dictionary<string, Scene>();
    private static Scene _currentScene = null;

    public static Scene CurrentScene
    {
        get => _currentScene;
    }

    public static void AddScene(string sceneName, Scene scene)
    {
        _scenes.Add(sceneName, scene);
    }

    public static void RefreshCurrentScene()
    {
        if (_currentScene == null) return;
        
        _currentScene.GameObjects.Clear();
        _currentScene.Start();

        var gameObjects = new List<GameObject>(_currentScene.GameObjects);
        foreach (GameObject gameObject in gameObjects)
        {
            var components = new List<Component>(gameObject.Components);
            foreach (Component component in components)
            {
                component.OnStart();
            }
        }
    }

    public static void ChangeScene(string sceneName)
    {
        Scene newScene = _scenes[sceneName];
        if (newScene != null)
        {
            if (_currentScene != newScene)
            {
                _currentScene = newScene;
                RefreshCurrentScene();
            }
        }
        else
        {
            throw new Exception("Scene " + sceneName + " is not found!");
        }
    }

    public static void UpdateCurrentScene()
    {
        _currentScene?.Update();
    }

    public static void RenderCurrentScene()
    {
        _currentScene?.Render();
    }

    public static void RenderCurrentSceneUI()
    {
        _currentScene?.RenderUI();
    }
}