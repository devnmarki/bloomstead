using System.Collections.Generic;
using LumiEngine;
using LumiEngine;

namespace LumiEngine;

public class Scene
{
    private List<GameObject> _gameObjects = new List<GameObject>();

    public List<GameObject> GameObjects
    {
        get => _gameObjects;
    }
    
    public Scene()
    {
        
    }

    public void AddGameObject(GameObject gameObject)
    {
        _gameObjects.Add(gameObject);
    }

    public void RemoveGameObject(GameObject gameObject)
    {
        _gameObjects.Remove(gameObject);
    }
    
    public virtual void Start()
    {
        
    }

    public virtual void Update()
    {
        var gameObjectsCopy = new List<GameObject>(_gameObjects);
        foreach (GameObject gameObject in gameObjectsCopy)
        {
            var componentsCopy = new List<Component>(gameObject.Components);
            foreach (Component component in componentsCopy)
            {
                component.OnUpdate();
            }
        }
    }

    public virtual void Render()
    {
        var gameObjectCopy = new List<GameObject>(_gameObjects);
        foreach (GameObject gameObject in gameObjectCopy)
        {
            var componentsCopy = new List<Component>(gameObject.Components);
            foreach (Component component in componentsCopy)
            {
                component.OnDraw();
            }
        }
    }
}