
using System.Collections.Generic;

public static class SkinInt2String
{
    public static readonly Dictionary<string, string[]> SkinDic = new Dictionary<string, string[]>
    {
        {
            "Weapon", 
            new string[]{
                "ShortDagger",
                "RoyalLongsword",
                "BattleAxe",
                "BattleBow",
                "BlueWand",
            }
        },
        {
            "Armor", 
            new string[]{
                "PirateCostume",
                "Hellsing",
                "IronKnight",
            }
        }
    };
}