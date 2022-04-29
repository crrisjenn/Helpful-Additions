using Assets.Scripts.Unity.Menu;
using Assets.Scripts.Unity.UI_New.ChallengeEditor;
using Assets.Scripts.Unity.UI_New.Settings;
using HarmonyLib;
using HelpfulAdditions.Properties;
using UnhollowerRuntimeLib;
using UnityEngine;
using UnityEngine.UI;

namespace HelpfulAdditions {
    [HarmonyPatch]
    internal static class SettingsMenu {
        private const string helpfulAdditionsCode = "Helpful Additions Setting";

        [HarmonyPatch(typeof(SettingsScreen), nameof(SettingsScreen.Open))]
        [HarmonyPostfix]
        public static void AddSettingsButton(ref SettingsScreen __instance) {
            GameObject settingsButton = Object.Instantiate(__instance.logoutBtn.transform.parent.gameObject, __instance.hotkeysBtn.transform.parent.parent);
            settingsButton.name = "HelpfulAdditionsSettingsButton";
            NK_TextMeshProUGUI text = settingsButton.GetComponentInChildren<NK_TextMeshProUGUI>();
            text.localizeKey = "";
            text.text = "Helpful Additions";
            Button button = settingsButton.GetComponentInChildren<Button>();
            button.onClick.AddListener(new System.Action(() => MenuManager.instance.OpenMenu("ExtraSettingsUI", helpfulAdditionsCode)));
            // settingsButton has an image and GetComponentInChildren checks there for some readon
            Image image = settingsButton.GetComponentsInChildren<Image>()[1];
            image.sprite = Textures.SettingsIcon;
        }

        [HarmonyPatch(typeof(ExtraSettingsScreen), nameof(ExtraSettingsScreen.Open))]
        [HarmonyPostfix]
        public static void AddSettings(ref ExtraSettingsScreen __instance, Il2CppSystem.Object menuData) {
            if (menuData is not null && menuData.Equals(helpfulAdditionsCode)) {
                VerticalLayoutGroup vlg = __instance.bigBloons.transform.parent.GetComponent<VerticalLayoutGroup>();
                vlg.childControlWidth = true;
                vlg.childControlHeight = true;

                for (int i = 0; i < __instance.bigBloons.transform.parent.childCount; i++)
                    __instance.bigBloons.transform.parent.GetChild(i).gameObject.SetActive(false);

                RectTransform panelRect = __instance.bigBloons.GetComponent<RectTransform>();

                GameObject mainPanel = new("MainPanel");
                ScrollRect mainPanelScroll = mainPanel.AddComponent<ScrollRect>();
                mainPanelScroll.horizontal = false;
                mainPanelScroll.scrollSensitivity = 150;
                LayoutElement mainPanelLayout = mainPanel.AddComponent<LayoutElement>();
                mainPanelLayout.preferredWidth = panelRect.sizeDelta.x;
                mainPanelLayout.preferredHeight = panelRect.sizeDelta.y * 6.5f;
                mainPanel.transform.parent = vlg.transform;
                RectTransform mainPanelRect = mainPanel.GetComponent<RectTransform>();
                mainPanelRect.localScale = Vector3.one;
                mainPanelScroll.viewport = mainPanelRect;

                GameObject content = new("Content", new Il2CppSystem.Type[] { Il2CppType.Of<RectTransform>() });
                // Used to be able to scroll on spaces
                content.AddComponent<Image>().sprite = Textures.Blank;
                VerticalLayoutGroup contentGroup = content.AddComponent<VerticalLayoutGroup>();
                contentGroup.childControlWidth = false;
                contentGroup.childControlHeight = false;
                contentGroup.childForceExpandWidth = false;
                contentGroup.childForceExpandHeight = false;
                contentGroup.childAlignment = TextAnchor.MiddleCenter;
                contentGroup.spacing = 40;
                content.transform.parent = mainPanel.transform;
                RectTransform contentRect = content.GetComponent<RectTransform>();
                contentRect.localScale = Vector3.one;
                contentRect.sizeDelta = new Vector2(__instance.bigBloons.GetComponent<RectTransform>().sizeDelta.x, contentRect.sizeDelta.y);
                mainPanelScroll.content = contentRect;

                AddExtraSettingsPanel(__instance,
                                      content,
                                      "DestroyAllProjectilesPanel",
                                      "Destroy All Projectiles Button",
                                      () => Settings.DeleteAllProjectilesOn,
                                      isOn => Settings.DeleteAllProjectilesOn = isOn,
                                      Textures.DeleteAllProjectilesSettingsIcon);

                AddExtraSettingsPanel(__instance,
                                      content,
                                      "SinglePlayerCoopPanel",
                                      "Single Player Coop",
                                      () => Settings.SinglePlayerCoopOn,
                                      isOn => Settings.SinglePlayerCoopOn = isOn,
                                      Textures.SinglePlayerCoopSettingsIcon);

                AddExtraSettingsPanel(__instance,
                                      content,
                                      "PowersInSandboxPanel",
                                      "Powers In Sandbox",
                                      () => Settings.PowersInSandboxOn,
                                      isOn => Settings.PowersInSandboxOn = isOn,
                                      Textures.PowersInSandboxSettingsIcon);

                ExtraSettingsPanel roundInfoScreenPanel = AddExtraSettingsPanel(__instance,
                                                                                content,
                                                                                "RoundInfoScreenPanel",
                                                                                "Round Info Screen",
                                                                                () => Settings.RoundInfoScreenOn,
                                                                                isOn => Settings.RoundInfoScreenOn = isOn,
                                                                                Textures.RoundInfoScreenSettingsIcon);
                ExtraSettingsPanel showBloonIdsPanel = AddExtraSettingsPanel(__instance,
                                                                             content,
                                                                             "ShowBloonIdsPanel",
                                                                             "Show Bloon Ids",
                                                                             () => Settings.ShowBloonIdsOn,
                                                                             isOn => Settings.ShowBloonIdsOn = isOn,
                                                                             null);
                ExtraSettingsPanel showBossBloonsPanel = AddExtraSettingsPanel(__instance,
                                                                               content,
                                                                               "ShowBossBloonsPanel",
                                                                               "Show Boss Bloons",
                                                                               () => Settings.ShowBossBloonsOn,
                                                                               isOn => Settings.ShowBossBloonsOn = isOn,
                                                                               null);
                GroupExtraSettingsPanels(content, roundInfoScreenPanel, showBloonIdsPanel, showBossBloonsPanel);

                AddExtraSettingsPanel(__instance,
                                      content,
                                      "RoundSetSwitcherPanel",
                                      "Sandbox Round Set Switcher",
                                      () => Settings.RoundSetSwitcherOn,
                                      isOn => Settings.RoundSetSwitcherOn = isOn,
                                      Textures.RoundSetSwitcherSettingsIcon);

                AddExtraSettingsPanel(__instance,
                                      content,
                                      "BlonsInEditorPanel",
                                      "Show Hidden Maps In Challenge Editor",
                                      () => Settings.BlonsInEditorOn,
                                      isOn => Settings.BlonsInEditorOn = isOn,
                                      Textures.BlonsInEditorSettingsIcon);

                mainPanelScroll.normalizedPosition = new Vector2(0, 1);
            }
        }

        private static ExtraSettingsPanel AddExtraSettingsPanel(ExtraSettingsScreen __instance, GameObject parent, string name, string text, System.Func<bool> getSetting, System.Action<bool> setSetting, Sprite icon) {
            ExtraSettingsPanel panel = Object.Instantiate(__instance.bigBloons, parent.transform);
            panel.gameObject.SetActive(true);
            panel.name = name;
            panel.SetAnimator(getSetting());
            panel.toggle.isOn = getSetting();
            panel.toggle.onValueChanged.AddListener(setSetting);
            NK_TextMeshProUGUI txt = panel.GetComponentInChildren<NK_TextMeshProUGUI>();
            txt.localizeKey = "";
            txt.text = text;
            Image image = GetExtraSettingsPanelIcon(panel);
            image.sprite = icon;
            panel.GetComponent<SolidShadow>().enabled = false;

            RectTransform parentRect = parent.GetComponentInChildren<RectTransform>();
            parentRect.sizeDelta += new Vector2(0, panel.GetComponent<LayoutElement>().preferredHeight);

            return panel;
        }

        // panel itself has an image and GetComponentInChildren checks there for some reason
        private static Image GetExtraSettingsPanelIcon(ExtraSettingsPanel panel) => panel.transform.GetChild(1).GetComponent<Image>();

        private static void GroupExtraSettingsPanels(GameObject parent, ExtraSettingsPanel main, params ExtraSettingsPanel[] subSettings) {
            GameObject group = new($"{main.name}Group");
            LayoutElement groupLayout = group.AddComponent<LayoutElement>();
            LayoutElement mainLayout = main.GetComponent<LayoutElement>();
            groupLayout.minWidth = mainLayout.preferredWidth;
            groupLayout.minHeight = mainLayout.preferredHeight * (1 + (.75f * subSettings.Length));
            VerticalLayoutGroup vlg = group.AddComponent<VerticalLayoutGroup>();
            vlg.childAlignment = TextAnchor.MiddleCenter;
            vlg.childForceExpandWidth = false;
            vlg.childForceExpandHeight = false;
            group.transform.parent = parent.transform;
            group.transform.localScale = Vector3.one;
            main.transform.parent = group.transform;
            RectTransform groupRect = group.GetComponent<RectTransform>();
            groupRect.sizeDelta = new Vector2(groupLayout.minWidth, groupLayout.minHeight);

            RectTransform parentRect = parent.GetComponent<RectTransform>();
            parentRect.sizeDelta -= new Vector2(0, mainLayout.preferredHeight * (subSettings.Length + 1));
            parentRect.sizeDelta += new Vector2(0, groupLayout.minHeight);

            GameObject subGroup = new($"{main.name}Subgroup");
            Image subGroupImage = subGroup.AddComponent<Image>();
            Image mainBack = main.panel.GetComponent<Image>();
            subGroupImage.sprite = mainBack.sprite;
            subGroupImage.type = mainBack.type;
            subGroupImage.material = mainBack.material;
            LayoutElement subGroupLayout = subGroup.AddComponent<LayoutElement>();
            subGroupLayout.preferredWidth = mainLayout.preferredWidth * .75f;
            subGroupLayout.preferredHeight = mainLayout.preferredHeight * (.75f * subSettings.Length);
            VerticalLayoutGroup subvlg = subGroup.AddComponent<VerticalLayoutGroup>();
            subvlg.childAlignment = TextAnchor.MiddleCenter;
            subvlg.childForceExpandWidth = false;
            subvlg.childForceExpandHeight = false;
            subGroup.transform.parent = group.transform;
            subGroup.transform.localScale = Vector3.one;
            for (int i = 0; i < subSettings.Length; i++) {
                subSettings[i].GetComponent<Image>().enabled = false;
                GetExtraSettingsPanelIcon(subSettings[i]).enabled = false;
                subSettings[i].transform.parent = subGroup.transform;
                subSettings[i].transform.localScale = Vector3.one * .75f;
            }
        }
    }
}