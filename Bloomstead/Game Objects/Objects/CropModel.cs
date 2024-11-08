using LumiEngine;

namespace Bloomstead.Bloomstead.Game_Objects.Objects;

public class CropModel
{
    public static class Models
    {
        public static readonly Crop ModelGarlic = new Crop("Garlic", Assets.Spritesheets.Crops.Garlic, new int[] { 1, 1, 1, 1, 1 }, 75, 60);
    }

    public class Crop
    {
        public string Name { get; }
        public Spritesheet Spritesheet { get; }
        public int[] GrowingDays { get; }
        public int BuyPrice { get; }
        public int SellPrice { get; }
        
        public Crop(string name, Spritesheet spritesheet, int[] growingDays, int buyPrice, int sellPrice)
        {
            Name = name;
            Spritesheet = spritesheet;
            GrowingDays = growingDays;
            BuyPrice = buyPrice;
            SellPrice = sellPrice;
        }
    }
}