﻿using Frosty.Core;
using Frosty.Core.Controls.Editors;
using Frosty.Core.Misc;
using FrostySdk.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaunchPlatformPlugin.Options
{
    public enum LaunchPlatform
    {
        Origin,
        EADesktop,
        Steam,
        EpicGamesLauncher
    }

    public class FrostyPlatformDataEditor : FrostyCustomComboDataEditor<string, string>
    {
    }

    [DisplayName("Launch Options")]
    public class LaunchOptions : OptionsExtension
    {
        [Category("General")]
        [Description("Enables the platform launching system since it's not needed if only Origin is being used")]
        [DisplayName("Platform Launching Enabled")]
        [EbxFieldMeta(FrostySdk.IO.EbxFieldType.Boolean)]
        public bool PlatformLaunchingEnabled { get; set; } = false;

        [Category("General")]
        [Description("The specific platform Frosty should launch the game on")]
        [EbxFieldMeta(FrostySdk.IO.EbxFieldType.Struct)]
        [Editor(typeof(FrostyPlatformDataEditor))]
        [DependsOn("PlatformLaunchingEnabled")]
        public CustomComboData<string, string> Platform { get; set; }

        public override void Load()
        {
            List<string> platforms = Enum.GetNames(typeof(LaunchPlatform)).ToList();
            Platform = new CustomComboData<string, string>(platforms, platforms) { SelectedIndex = platforms.IndexOf(Config.Get<string>("Platform", "Origin", ConfigScope.Game))};

            PlatformLaunchingEnabled = Config.Get("PlatformLaunchingEnabled", false, ConfigScope.Game);
        }

        public override void Save()
        {
            Config.Add("Platform", Platform.SelectedName, ConfigScope.Game);
            Config.Add("PlatformLaunchingEnabled", PlatformLaunchingEnabled, ConfigScope.Game);
        }
    }
}
