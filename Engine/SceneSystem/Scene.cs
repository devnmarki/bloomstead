using System;
using System.Collections.Generic;
using LumiEngine;
using LumiEngine;

namespace LumiEngine;

public class Scene
{
    private List<GameObject> _gameObjects = new List<GameObject>();
    private List<GameObject> _uiElements = new List<GameObject>();

    public List<GameObject> GameObjects
    {
        get => _gameObjects;
    }

    public List<GameObject> UIElements
    {
        get => _uiElements;
    }
    
    public void AddGameObject(GameObject gameObject)
    {
        _gameObjects.Add(gameObject);
    }
    
    public void AddUIElement(GameObject element)
    {
        _uiElements.Add(element);
    }

    public void RemoveGameObject(GameObject gameObject)
    {
        _gameObjects.Remove(gameObject);
    }
    
    public void RemoveUIElement(GameObject element)
    {
        _uiElements.Remove(element);
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
        GameObjects.Sort((a, b) => a.Transform.Position.Y.CompareTo(b.Transform.Position.Y));
        
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

    public void RenderUI()
    {
        var uiElements = new List<GameObject>(_uiElements);
        foreach (GameObject uiElement in uiElements)
        {
            var componentsCopy = new List<Component>(uiElement.Components);
            foreach (Component component in componentsCopy)
            {
                component.OnDraw();
            }
        }
    }
    
    public GameObject FindGameObjectByTag(string tag)
    {
        GameObject target = null;
        foreach (GameObject gameObject in _gameObjects)
        {
            if (gameObject.Tag == tag)
            {
                target = gameObject;
            }
        }

        if (target == null)
            Console.WriteLine("Couldn't find game object with tag " + tag);
        
        return target;
    }

    public List<GameObject> FindGameObjectsByTag(string tag)
    {
        List<GameObject> targets = new List<GameObject>();
        foreach (GameObject gameObject in _gameObjects)
        {
            if (gameObject.Tag == tag)
                targets.Add(gameObject);
        }

        return targets;
    }

    public GameObject FindGameObjectByName(string name)
    {
        GameObject target = null;
        foreach (GameObject gameObject in _gameObjects)
        {
            if (gameObject.Name == name)
            {
                target = gameObject;
            }
        }
        
        if (target == null)
            Console.WriteLine("Couldn't find game object with name " + name);

        return target;
    }
    
    public List<GameObject> FindGameObjectsByName(string name)
    {
        List<GameObject> targets = new List<GameObject>();
        foreach (GameObject gameObject in _gameObjects)
        {
            if (gameObject.Name == name)
                targets.Add(gameObject);
        }

        return targets;
    }
}