using System.Collections.Generic;
using UnityEngine;

namespace HelpfulAdditions {
    public static class CustomBloons {

        private static readonly Dictionary<string, CustomBloon> customBloons = new();

        internal static bool IsRegistered(string id) => customBloons.ContainsKey(id);

        internal static CustomBloon Get(string id) => customBloons[id];

        public static void AddCustomBloon(string bloonId, byte[] icon, byte[] edge, byte[] span, Vector2? iconSize = null) {
            CustomBloon customBloon = new(bloonId, icon, iconSize, edge, span);
            if (!IsRegistered(bloonId))
                customBloons.Add(bloonId, customBloon);
            else
                customBloons[bloonId] = customBloon;
        }

        // Must convert to byte[] in order for the Il2Cpp side to not garbage collect it
        public static void AddCustomBloon(string bloonId, Texture2D icon, Texture2D edge, Texture2D span, Vector2? iconSize = null) =>
            AddCustomBloon(bloonId, ImageConversion.EncodeToPNG(icon), ImageConversion.EncodeToPNG(edge), ImageConversion.EncodeToPNG(span), iconSize);

        public static void AddCustomBloon(string bloonId, Sprite icon, Sprite edge, Sprite span, Vector2? iconSize = null) =>
            AddCustomBloon(bloonId, icon.texture, edge.texture, span.texture, iconSize);
    }
    internal sealed class CustomBloon {
        public string Id { get; }

        public byte[] Icon { get; }

        public Vector2? Size { get; }

        public byte[] Edge { get; }

        public byte[] Span { get; }

        public CustomBloon(string id, byte[] icon, Vector2? size, byte[] edge, byte[] span) {
            Id = id;
            Icon = icon;
            Size = size;
            Edge = edge;
            Span = span;
        }
    }
}
