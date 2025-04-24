using UnityEngine;
using System.Collections.Generic;

namespace TwoZeroFourEight
{
    public static class TileColors
    {
        private static readonly Dictionary<int, Color> tileColorMap = new Dictionary<int, Color>()
        {
            { 0,    new Color(0.80f, 0.80f, 0.80f, 1f) },  // Empty tile
            { 2,    new Color(0.93f, 0.89f, 0.85f, 1f) },  // #EEE4DA
            { 4,    new Color(0.93f, 0.88f, 0.78f, 1f) },  // #EDE0C8
            { 8,    new Color(0.95f, 0.69f, 0.47f, 1f) },  // #F2B179
            { 16,   new Color(0.96f, 0.58f, 0.39f, 1f) },  // #F59563
            { 32,   new Color(0.96f, 0.49f, 0.37f, 1f) },  // #F67C5F
            { 64,   new Color(0.96f, 0.37f, 0.23f, 1f) },  // #F65E3B
            { 128,  new Color(0.93f, 0.81f, 0.45f, 1f) },  // #EDCF72
            { 256,  new Color(0.93f, 0.80f, 0.38f, 1f) },  // #EDCC61
            { 512,  new Color(0.93f, 0.78f, 0.31f, 1f) },  // #EDC850
            { 1024, new Color(0.93f, 0.77f, 0.25f, 1f) },  // #EDC53F
            { 2048, new Color(0.93f, 0.76f, 0.18f, 1f) }   // #EDC22E
        };

        // Text colors
        private static readonly Color darkTextColor = new Color(0.47f, 0.43f, 0.39f, 1f);   // #776E65
        private static readonly Color lightTextColor = new Color(0.97f, 0.96f, 0.94f, 1f);  // #F9F6F2

        public static Color GetTileColor(int value)
        {
            return tileColorMap.ContainsKey(value) ? tileColorMap[value] : tileColorMap[2048];
        }

        public static Color GetTextColor(int value)
        {
            return value <= 4 ? darkTextColor : lightTextColor;
        }

        public static Color GridColor => new Color(0.72f, 0.67f, 0.63f, 1f);        // #BBADA0
    }
}