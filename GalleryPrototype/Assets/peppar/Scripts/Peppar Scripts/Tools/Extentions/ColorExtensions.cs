using UnityEngine;

namespace peppar
{
    public static class ColorExtensions
    {
        public static string ColorToHex(this Color color)
        {
            return "#" + color.ColorToCode() + "ff";
        }

        public static string ColorToCode(this Color color)
        {
            return ((Color32)color).r.ToString("X2") + ((Color32)color).g.ToString("X2") + ((Color32)color).b.ToString("X2");
        }

        public static Color Pink(this Color color)
        {
            return new Color(1, 0, 1);
        }
    }
}
