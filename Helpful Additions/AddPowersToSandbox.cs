﻿using Assets.Scripts.Unity.UI_New.InGame;
using Assets.Scripts.Unity.UI_New.InGame.BloonMenu;
using Assets.Scripts.Unity.UI_New.InGame.RightMenu;
using Assets.Scripts.Unity.UI_New.InGame.RightMenu.Powers;
using HarmonyLib;
using HelpfulAdditions.Properties;

namespace HelpfulAdditions {
    [HarmonyPatch]
    internal static class AddPowersToSandbox {
        private static bool IsSandboxAndPowersOn() => (InGame.instance?.bridge?.IsSandboxMode() ?? false) && Settings.PowersInSandboxOn;

        [HarmonyPatch(typeof(RightMenu), nameof(RightMenu.SetPowersInteractable))]
        [HarmonyPrefix]
        public static bool MakePowersAvailable(ref bool interactable) {
            if (IsSandboxAndPowersOn())
                interactable = true;
            return true;
        }

        [HarmonyPatch(typeof(BloonMenu), nameof(BloonMenu.Initialise))]
        [HarmonyPostfix]
        public static void SetInitialState() {
            if (IsSandboxAndPowersOn()) {
                ShopMenu.instance.GetComponentInChildren<BloonMenuToggle>(true).gameObject.SetActive(false);
                RightMenu.instance.powerBtn.gameObject.SetActive(true);
            }
        }

        [HarmonyPatch(typeof(BloonMenuToggle), nameof(BloonMenuToggle.ToggleBloonMenu))]
        [HarmonyPostfix]
        public static void ShowBloonMenu() {
            if (IsSandboxAndPowersOn() && InGame.instance.BloonMenu.showInternal) {
                ShopMenu.instance.GetComponentInChildren<BloonMenuToggle>(true).gameObject.SetActive(false);
                RightMenu.instance.powerBtn.gameObject.SetActive(true);
            }
        }

        [HarmonyPatch(typeof(PowersMenu), nameof(PowersMenu.Open))]
        [HarmonyPostfix]
        public static void ShowPowersMenu() {
            if (IsSandboxAndPowersOn()) {
                InGame.instance.BloonMenu.showInternal = false;
                ShopMenu.instance.GetComponentInChildren<BloonMenuToggle>(true).gameObject.SetActive(false);
                RightMenu.instance.powerBtn.gameObject.SetActive(true);
                powerIsBeingUsed = false;
            }
        }

        [HarmonyPatch(typeof(PowersMenu), nameof(PowersMenu.Close))]
        [HarmonyPostfix]
        public static void ShowTowerMenu() {
            if (IsSandboxAndPowersOn() && !powerIsBeingUsed) {
                ShopMenu.instance.GetComponentInChildren<BloonMenuToggle>(true).gameObject.SetActive(true);
                RightMenu.instance.powerBtn.gameObject.SetActive(false);
            }
        }

        [HarmonyPatch(typeof(PowersMenu), nameof(PowersMenu.Initialise))]
        [HarmonyPrefix]
        public static void InitialiseSandboxPowersMenuPrefix(ref PowersMenu __instance) {
            if (IsSandboxAndPowersOn()) {
                __instance.instasDisabled = true;
                __instance.dontUsePlayerInventory = true;
            }
        }

        [HarmonyPatch(typeof(ReadonlyInGameData), nameof(ReadonlyInGameData.ArePowersAllowed))]
        [HarmonyPostfix]
        public static void AllowPowersInSandbox(ref bool __result) {
            if (IsSandboxAndPowersOn())
                __result = true;
        }

        private static bool powerIsBeingUsed = false;
        [HarmonyPatch(typeof(PowersMenu), nameof(PowersMenu.StartPowerPlacement))]
        [HarmonyPostfix]
        public static void WhenPowerIsBeingUsed() {
            if (IsSandboxAndPowersOn())
                powerIsBeingUsed = true;
        }

        [HarmonyPatch(typeof(StandardPowerButton), nameof(StandardPowerButton.UpdatePowerDisplay))]
        [HarmonyPostfix]
        public static void DontShowPowerCountIcon(ref StandardPowerButton __instance) {
            if (IsSandboxAndPowersOn())
                __instance.powerCountIcon.SetActive(false);
        }

        [HarmonyPatch(typeof(StandardPowerButton), nameof(StandardPowerButton.UpdateUseCount))]
        [HarmonyPostfix]
        public static void DontShowPowerCountText(ref StandardPowerButton __instance) {
            if (IsSandboxAndPowersOn())
                __instance.powerCountText.gameObject.SetActive(false);
        }

        [HarmonyPatch(typeof(PowersMenu), nameof(PowersMenu.PowerUseSuccess))]
        [HarmonyPrefix]
        public static bool DontLosePowers() {
            // If in sandbox, don't allow for player profile to change when power used
            return !IsSandboxAndPowersOn();
        }
    }
}
