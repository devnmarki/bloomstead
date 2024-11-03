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
        public static Texture2D Character = Config.Content.Load<Texture2D>("sprites/characters/character_base_spritesheet");
    }
    
    public static class Spritesheets
    {
        public static Spritesheet Character = new Spritesheet(Textures.Character, 20, 4, new Vector2(32));
    }
}