using LumiEngine;

namespace Bloomstead.Bloomstead.Game_Objects.Items;

public static class ItemModel
{
    public enum ItemType
    {
        Material,
        Consumable,
        Tool,
    }

    public static class Models
    {
        public static readonly Item ModelLog = new Item("Oak Log", ItemType.Material, Assets.Spritesheets.Items.Logs);
    }

    public class Item
    {
        public string Name { get; }
        public ItemType Type { get; }
        public Spritesheet Spritesheet { get; }
        public int Sprite { get; }
        public bool Stackable { get; }

        public Item(string name, ItemType type, Spritesheet spritesheet, int sprite = 0, bool stackable = true)
        {
            Name = name;
            Type = type;
            Spritesheet = spritesheet;
            Sprite = sprite;
            Stackable = stackable;
        }
    }
}