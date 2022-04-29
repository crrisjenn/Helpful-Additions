using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace HelpfulAdditions.Properties {
    internal static class Resources {
        private static readonly Assembly thisAssembly = Assembly.GetExecutingAssembly();
        private static readonly string assemblyName = thisAssembly.GetName().Name.Replace(" ", "");
        private static readonly string[] resourceNames = thisAssembly.GetManifestResourceNames();

        private static byte[] GetResource(string resourceName) {
            string fullName = $"{assemblyName}.Resources.{resourceName}";

            if (!resourceNames.Contains(fullName))
                return null;

            using MemoryStream resourceStream = new();
            try {
                thisAssembly.GetManifestResourceStream(fullName).CopyTo(resourceStream);
                return resourceStream.ToArray();
            } catch {
                return null;
            }
        }

        private static Texture2D LoadTextureFromBytes(byte[] data) {
            if (data is null)
                return null;

            Texture2D tex = new(0, 0) { wrapMode = TextureWrapMode.Clamp };
            bool success = ImageConversion.LoadImage(tex, data);

            if (!success)
                return null;

            return tex;
        }

        private static Sprite LoadSpriteFromTexture(Texture2D tex) {
            if (tex is null)
                return null;

            return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        }

        public static Sprite LoadSprite(string resourceName) {
            byte[] data = GetResource(resourceName + ".png");
            Texture2D tex = LoadTextureFromBytes(data);
            return LoadSpriteFromTexture(tex);
        }

        public static Sprite LoadSprite(byte[] data) {
            Texture2D tex = LoadTextureFromBytes(data);
            return LoadSpriteFromTexture(tex);
        }
    }
}
