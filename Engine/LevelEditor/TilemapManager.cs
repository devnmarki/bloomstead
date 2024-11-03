using System;
using System.Collections.Generic;
using System.Linq;
using LumiEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TiledSharp;

namespace LumiEngine.LevelEditor;

public class TilemapManager
{
    TmxMap map;
    Texture2D tileset;

    int tileWidth;
    int tileHeight;
    int tilesetTilesWide;
    int tilesetTilesHigh;

    public static Dictionary<string, Func<GameObject>> GameObjects { get; set; } = new Dictionary<string, Func<GameObject>>();
    
    public TilemapManager(TmxMap map, Texture2D tileset)
    {
        this.map = map;
        this.tileset = tileset;
        tileWidth = map.Tilesets[0].TileWidth;
        tileHeight = map.Tilesets[0].TileHeight;
        tilesetTilesWide = tileset.Width / tileWidth;
    }

    public void Draw(Vector2 startPosition, float layerDepth = 0f)
    {
        foreach (var layer in map.Layers)
        {
            for (int i = 0; i < layer.Tiles.Count; i++)
            {
                int gid = layer.Tiles[i].Gid;

                if (gid != 0)
                {
                    int tileFrame = gid - 1;
                    int column = tileFrame % tilesetTilesWide;
                    int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

                    float x = startPosition.X + (i % map.Width) * tileWidth * Config.GameScale;
                    float y = startPosition.Y + (float)Math.Floor(i / (double)map.Width) * tileHeight * Config.GameScale;

                    Rectangle tilesetRect = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);

                    Config.Batch.Draw(tileset, 
                        new Rectangle((int)x - (int)Config.CameraX, (int)y - (int)Config.CameraY, (int)(tileWidth * Config.GameScale), (int)(tileHeight * Config.GameScale)), 
                        tilesetRect, 
                        Color.White,
                        0f,
                        Vector2.Zero, 
                        SpriteEffects.None,
                        layerDepth
                        );
                }
            }
        }
    }
    
    public void Draw(string layerName, Vector2 startPosition, float layerDepth = 0f)
    {
        var layer = map.Layers[layerName];
        
        for (int i = 0; i < layer.Tiles.Count; i++)
        {
            int gid = layer.Tiles[i].Gid;

            if (gid != 0)
            {
                int tileFrame = gid - 1;
                int column = tileFrame % tilesetTilesWide;
                int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

                float x = startPosition.X + (i % map.Width) * tileWidth * Config.GameScale;
                float y = startPosition.Y + (float)Math.Floor(i / (double)map.Width) * tileHeight * Config.GameScale;

                Rectangle tilesetRect = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);

                Config.Batch.Draw(tileset, 
                    new Rectangle((int)x - (int)Config.CameraX, (int)y - (int)Config.CameraY, (int)(tileWidth * Config.GameScale), (int)(tileHeight * Config.GameScale)), 
                    tilesetRect, 
                    Color.White,
                    0f,
                    Vector2.Zero, 
                    SpriteEffects.None,
                    layerDepth
                );
            }
        }
    }

    public List<TmxLayerTile> GetTiles(string layerName)
    {
        var layer = map.Layers[layerName];

        return layer.Tiles.Where(tile => tile.Gid != 0).ToList();
    }
    
    public void CreateColliders(Vector2 startPosition)
    {
        foreach (var obj in map.ObjectGroups["Collision"].Objects)
        {
            double x = startPosition.X + obj.X * Config.GameScale;
            double y = startPosition.Y + obj.Y * Config.GameScale;
            double width = obj.Width * Config.GameScale;
            double height = obj.Height * Config.GameScale;

            GameObject collider = new GameObject();
            collider.Transform.Position.X = (float)x;
            collider.Transform.Position.Y = (float)y;
            collider.AddComponent(new BoxCollider(new Vector2((float)width, (float)height)));
            SceneManager.CurrentScene.AddGameObject(collider);
        }
    }

    public static void AddGameObjectToLoad(string name, Func<GameObject> gameObject)
    {
        GameObjects.Add(name, gameObject);
    }
    
    public void LoadGameObjects()
    {
        if (map.ObjectGroups.TryGetValue("Game Objects", out var gameObjectsLayer))
        {
            foreach (var obj in gameObjectsLayer.Objects)
            {
                if (GameObjects.TryGetValue(obj.Name, out var factory))
                {
                    GameObject go = factory();

                    go.MapObject = obj;
                    go.Transform.Position = new Vector2((float)obj.X * Config.GameScale, (float)obj.Y * Config.GameScale);
                    
                    SceneManager.CurrentScene.AddGameObject(go);
                }
            }
        }
    }
}