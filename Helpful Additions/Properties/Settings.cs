using MelonLoader;

namespace HelpfulAdditions.Properties {
    internal static class Settings {
        private static MelonPreferences_Category Category { get; } = MelonPreferences.CreateCategory("HelpfulAdditionsSettings");

        private static void SetValueAndSave<T>(MelonPreferences_Entry<T> entry, T value) {
            entry.Value = value;
            Category.SaveToFile(false);
        }

        private static MelonPreferences_Entry<bool> PowersInSandboxOnEntry { get; } = Category.CreateEntry(nameof(PowersInSandboxOn), true);
        public static bool PowersInSandboxOn {
            get => PowersInSandboxOnEntry.Value;
            set => SetValueAndSave(PowersInSandboxOnEntry, value);
        }

        private static MelonPreferences_Entry<bool> BlonsInEditorOnEntry { get; } = Category.CreateEntry(nameof(BlonsInEditorOn), true);
        public static bool BlonsInEditorOn {
            get => BlonsInEditorOnEntry.Value;
            set => SetValueAndSave(BlonsInEditorOnEntry, value);
        }

        private static MelonPreferences_Entry<bool> DeleteAllProjectilesOnEntry { get; } = Category.CreateEntry(nameof(DeleteAllProjectilesOn), true);
        public static bool DeleteAllProjectilesOn {
            get => DeleteAllProjectilesOnEntry.Value;
            set => SetValueAndSave(DeleteAllProjectilesOnEntry, value);
        }

        private static MelonPreferences_Entry<bool> ShuffleJukeboxOnEntry { get; } = Category.CreateEntry(nameof(ShuffleJukeboxOn), true);
        public static bool ShuffleJukeboxOn {
            get => ShuffleJukeboxOnEntry.Value;
            set => SetValueAndSave(ShuffleJukeboxOnEntry, value);
        }

        private static MelonPreferences_Entry<bool> RoundSetSwitcherOnEntry { get; } = Category.CreateEntry(nameof(RoundSetSwitcherOn), true);
        public static bool RoundSetSwitcherOn {
            get => RoundSetSwitcherOnEntry.Value;
            set => SetValueAndSave(RoundSetSwitcherOnEntry, value);
        }

        private static MelonPreferences_Entry<bool> SinglePlayerCoopOnEntry { get; } = Category.CreateEntry(nameof(SinglePlayerCoopOn), true);
        public static bool SinglePlayerCoopOn {
            get => SinglePlayerCoopOnEntry.Value;
            set => SetValueAndSave(SinglePlayerCoopOnEntry, value);
        }

        private static MelonPreferences_Entry<bool> RoundInfoScreenOnEntry { get; } = Category.CreateEntry(nameof(RoundInfoScreenOn), true);
        public static bool RoundInfoScreenOn {
            get => RoundInfoScreenOnEntry.Value;
            set => SetValueAndSave(RoundInfoScreenOnEntry, value);
        }

        private static MelonPreferences_Entry<bool> ShowBloonIdsOnEntry { get; } = Category.CreateEntry(nameof(ShowBloonIdsOn), false);
        public static bool ShowBloonIdsOn {
            get => ShowBloonIdsOnEntry.Value;
            set => SetValueAndSave(ShowBloonIdsOnEntry, value);
        }

        private static MelonPreferences_Entry<bool> ShowBossBloonsOnEntry { get; } = Category.CreateEntry(nameof(ShowBossBloonsOn), true);
        public static bool ShowBossBloonsOn {
            get => ShowBossBloonsOnEntry.Value;
            set => SetValueAndSave(ShowBossBloonsOnEntry, value);
        }
    }
}
