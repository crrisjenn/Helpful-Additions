using Assets.Scripts.Models.Bloons;
using System;
using System.Linq;

namespace HelpfulAdditions.Utils {
    internal static class BloonUtils {
        private static string[] NormalBloonBaseTypes { get; } = {
            "Red",
            "Blue",
            "Green",
            "Yellow",
            "Pink",
            "Purple",
            "Black",
            "White",
            "Lead",
            "Zebra",
            "Rainbow",
            "Ceramic"
        };

        private static string[] MoabBloonBaseTypes { get; } = {
            "Moab",
            "Bfb",
            "Zomg",
            "Ddt",
            "Bad"
        };

        private static string[] BossBloonBaseTypes { get; } = {
            "Bloonarius",
            "Lych",
            "Vortex"
        };

        public static void GetBloonPath(BloonModel bloon, out string path, out string basePath) {
            if (bloon is not null) {
                if (bloon.baseId.Equals("TestBloon"))
                    basePath = path = "Bloons.Bloons.Test.Test";
                else if (bloon.baseId.StartsWith("Golden"))
                    GetBloonPath(bloon, "Bloons", GetGoldenType, out path, out basePath);
                else if (NormalBloonBaseTypes.Contains(bloon.baseId))
                    GetBloonPath(bloon, "Bloons", GetNormalBloonType, out path, out basePath);
                else if (MoabBloonBaseTypes.Contains(bloon.baseId))
                    GetBloonPath(bloon, "Moabs", GetMoabBloonType, out path, out basePath);
                else if (BossBloonBaseTypes.Contains(bloon.baseId))
                    GetBloonPath(bloon, "Bosses", GetBossBloonType, out path, out basePath);
                else
                    path = basePath = null;
            } else
                path = basePath = null;
        }

        private delegate string TypeDelegate(BloonModel bloon, out string newBaseId, out bool overrideBasePath);
        private static void GetBloonPath(BloonModel bloon, string type, TypeDelegate getType, out string path, out string basePath) {
            string bloonType = getType(bloon, out string newBaseId, out bool overrideBasePath);
            string baseId = newBaseId ?? bloon.baseId;
            basePath = $"Bloons.{type}.{baseId}.";
            path = basePath + bloonType;
            if (overrideBasePath)
                basePath = path;
            basePath += baseId;
            path += baseId;
        }

        public static int GetBossTier(BloonModel bloon) {
            // distance from char 0 gets digit value
            return bloon.id[^1] - '0';
        }

        private static string GetNormalBloonType(BloonModel bloon, out string newBaseId, out bool overrideBasePath) {
            newBaseId = null;
            overrideBasePath = false;
            string bloonType = "";
            if (bloon.isGrow)
                bloonType = "Regrow" + bloonType;
            if (bloon.isCamo)
                bloonType = "Camo" + bloonType;
            if (bloon.isFortified)
                bloonType = "Fortified" + bloonType;
            return bloonType;
        }

        private static string GetMoabBloonType(BloonModel moab, out string newBaseId, out bool overrideBasePath) {
            newBaseId = null;
            overrideBasePath = false;
            if (moab.isFortified)
                return "Fortified";
            return "";
        }

        private static string GetGoldenType(BloonModel golden, out string newBaseId, out bool overrideBasePath) {
            newBaseId = "Golden";
            overrideBasePath = false;
            string bloonType = "";
            if (golden.bloonProperties.HasFlag(BloonProperties.Black) && golden.bloonProperties.HasFlag(BloonProperties.White))
                bloonType = "Zebra" + bloonType;
            if (golden.bloonProperties.HasFlag(BloonProperties.Purple))
                bloonType = "Purple" + bloonType;
            if (golden.bloonProperties.HasFlag(BloonProperties.Lead))
                bloonType = "Lead" + bloonType;
            if (golden.isCamo)
                bloonType = "Camo" + bloonType;
            if (golden.isFortified)
                bloonType = "Fortified" + bloonType;
            return bloonType;
        }

        private static string GetBossBloonType(BloonModel boss, out string newBaseId, out bool overrideBasePath) {
            newBaseId = null;
            overrideBasePath = true;
            if (boss.id.Contains("Elite")) {
                return "Elite";
            }
            return "";
        }
    }
}
