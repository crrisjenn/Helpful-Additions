/***
 * This file may be copied without needing to disclose the source it was copied to.
 *
 * This file is under the Helpful Additions project.
 * Helpful Additions is licensed under the GNU General Public License Version 3.
 ***/

using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ModInterop {
    public static class HelpfulAdditions {
        static HelpfulAdditions() {
            // Gets all loaded assemblies
            Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
            // Find Helpful Addtions
            Assembly helpfulAdditions = assemblies.FirstOrDefault(assembly => assembly.GetName().Name.Equals("Helpful Additions"));
            // Get the Mod class in Helpful Addtions
            System.Type mod = helpfulAdditions?.GetType("HelpfulAdditions.CustomBloons");
            // Get the methods from Helpful Additions

            // Add custom bloon method
            AddCustomBloonFromBytesMethod = mod?.GetMethod("AddCustomBloon", new System.Type[] {
                typeof(string), typeof(byte[]), typeof(byte[]), typeof(byte[]), typeof(Vector2?)
            });
            AddCustomBloonFromTextureMethod = mod?.GetMethod("AddCustomBloon", new System.Type[] {
                typeof(string), typeof(Texture2D), typeof(Texture2D), typeof(Texture2D), typeof(Vector2?)
            });
            AddCustomBloonFromSpriteMethod = mod?.GetMethod("AddCustomBloon", new System.Type[] {
                typeof(string), typeof(Sprite), typeof(Sprite), typeof(Sprite), typeof(Vector2?)
            });
        }

        private static readonly MethodInfo AddCustomBloonFromBytesMethod;
        /// <summary>
        /// This function allows other modders to add custom bloon graphics, such that they show up within my mod.
        /// Reflection is necessary to access this method from another mod.
        /// </summary>
        /// <param name="bloonId">The id of the custom bloon being added</param>
        /// <param name="icon">The icon of the custom bloon being added as bytes from an image file</param>
        /// <param name="edge">The graphic for the ends of a timespan for the custom bloon as bytes from an image file</param>
        /// <param name="span">The graphic for the span of a timespan for the custom bloon as bytes from an image file</param>
        /// <param name="iconSize">The size of the icon in Unity, where the image size is the original, and 200 is the maximum recommended size</param>
        public static void AddCustomBloon(string bloonId, byte[] icon, byte[] edge, byte[] span, Vector2? iconSize = null) =>
            AddCustomBloonFromBytesMethod?.Invoke(null, new object[] { bloonId, icon, edge, span, iconSize });

        private static readonly MethodInfo AddCustomBloonFromTextureMethod;
        /// <summary>
        /// This function allows other modders to add custom bloon graphics, such that they show up within my mod.
        /// Reflection is necessary to access this method from another mod.
        /// </summary>
        /// <param name="bloonId">The id of the custom bloon being added</param>
        /// <param name="icon">The icon of the custom bloon being added as a Texture2D</param>
        /// <param name="edge">The graphic for the ends of a timespan for the custom bloon as a Texture2D</param>
        /// <param name="span">The graphic for the span of a timespan for the custom bloon as a Texture2D</param>
        /// <param name="iconSize">The size of the icon in Unity, where the image size is the original, and 200 is the maximum recommended size</param>
        public static void AddCustomBloon(string bloonId, Texture2D icon, Texture2D edge, Texture2D span, Vector2? iconSize = null) =>
            AddCustomBloonFromTextureMethod?.Invoke(null, new object[] { bloonId, icon, edge, span, iconSize });

        private static readonly MethodInfo AddCustomBloonFromSpriteMethod;
        /// <summary>
        /// This function allows other modders to add custom bloon graphics, such that they show up within my mod.
        /// Reflection is necessary to access this method from another mod.
        /// </summary>
        /// <param name="bloonId">The id of the custom bloon being added</param>
        /// <param name="icon">The icon of the custom bloon being added as a Sprite</param>
        /// <param name="edge">The graphic for the ends of a timespan for the custom bloon as a Sprite</param>
        /// <param name="span">The graphic for the span of a timespan for the custom bloon as a Sprite</param>
        /// <param name="iconSize">The size of the icon in Unity, where the image size is the original, and 200 is the maximum recommended size</param>
        public static void AddCustomBloon(string bloonId, Sprite icon, Sprite edge, Sprite span, Vector2? iconSize = null) =>
            AddCustomBloonFromSpriteMethod?.Invoke(null, new object[] { bloonId, icon, edge, span, iconSize });
    }
}
