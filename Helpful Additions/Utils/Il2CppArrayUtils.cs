using Il2CppSystem.Collections.Generic;
using UnhollowerBaseLib;

namespace HelpfulAdditions.Utils {
    internal static class Il2CppArrayUtils {
        public static T[] Add<T>(this Il2CppArrayBase<T> array, params T[] values) {
            T[] result = new T[array.Length + values.Length];
            array.CopyTo(result, 0);
            values.CopyTo(result, array.Length);
            return result;
        }

        public static void Shuffle<T>(this List<T> list) {
            System.Random r = new();
            for (int i = list.Count - 1; i > 0; i--) {
                int j = r.Next(i + 1);
                (list[j], list[i]) = (list[i], list[j]);
            }
        }
    }
}
