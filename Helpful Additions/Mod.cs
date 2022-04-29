using Assets.Scripts.Models;
using Assets.Scripts.Models.Rounds;
using HarmonyLib;
using HelpfulAdditions.Utils;
using MelonLoader;
using System.Collections.Generic;

[assembly: MelonInfo(typeof(HelpfulAdditions.Mod), "Helpful Additions", "2.0.0", "Baydock")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace HelpfulAdditions {
    [HarmonyPatch]
    public sealed class Mod : MelonMod {
        public static MelonLogger.Instance Logger { get; private set; }

        public override void OnApplicationStart() {
            Logger = LoggerInstance;
        }

#if DEBUG
        [HarmonyPatch(typeof(GameModelLoader), nameof(GameModelLoader.Load))]
        [HarmonyPostfix]
        public static void AddTestRoundSet(ref GameModel __result) {
            List<RoundModel> allBloons = new();

            for (int i = 0; i < __result.bloons.Length; i++)
                allBloons.Add(new RoundModel("", new(1) { [0] = new BloonGroupModel("", __result.bloons[i].id, 0, 1, 1) }));

            __result.roundSets = __result.roundSets.Add(new RoundSetModel("AllBloonsRoundSet", allBloons.ToArray()));
        }
#endif
    }
}
