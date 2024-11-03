using LumiEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TiledSharp;

namespace Bloomstead.Bloomstead;

public static class Assets
{
    public static class Tilesets
    {
        public static Texture2D Overworld { get; set; } = Config.Content.Load<Texture2D>("sprites/tilesets/overworld_tileset");
    }

    public static class Maps
    {
        public static TmxMap Overworld { get; set; } = new TmxMap("../../../Content/levels/default_map.tmx");
    }

    public static class Textures
    {
        public static readonly Texture2D Character = Config.Content.Load<Texture2D>("sprites/characters/character_base_spritesheet");
        public static readonly Texture2D Hitbox = Config.Content.Load<Texture2D>("sprites/hitbox_spritesheet");
        public static readonly Texture2D Soil = Config.Content.Load<Texture2D>("sprites/objects/soil_spritesheet");
        public static readonly Texture2D Hoe = Config.Content.Load<Texture2D>("sprites/tools/how_spritesheet");
    }
    
    public static class Spritesheets
    {
        public static readonly Spritesheet Character = new Spritesheet(Textures.Character, 20, 4, new Vector2(32));
        public static readonly Spritesheet Hitbox = new Spritesheet(Textures.Hitbox, 1, 2, new Vector2(16));
        public static readonly Spritesheet Soil = new Spritesheet(Textures.Soil, 1, 2, new Vector2(16));

        public static class Tools
        {
            public static readonly Spritesheet Hoe = new Spritesheet(Textures.Hoe, 4, 2, new Vector2(16));
        }
    }
}