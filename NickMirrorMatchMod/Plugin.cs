﻿using System;
using BepInEx;
using BepInEx.Configuration;
using Nick;
using System.Collections.Generic;
using HarmonyLib;
using System.Reflection;
using System.IO;
using UnityEngine;

namespace NickMirrorMatchMod
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        internal static Plugin Instance;
        static ConfigEntry<Color> tintColor1;
        static ConfigEntry<Color> tintColor2;
        static ConfigEntry<Color> tintColor3;

        private void Awake()
        {
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_NAME} is loaded!");

            if (Instance)
            {
                DestroyImmediate(this);
                return;
            }
            Instance = this;

            var config = this.Config;
            tintColor1 = config.Bind<Color>("Colors", "TintColor1", new Color(0, 0, 1, 0.66f));
            tintColor2 = config.Bind<Color>("Colors", "TintColor2", new Color(1, 0, 0, 0.66f));
            tintColor3 = config.Bind<Color>("Colors", "TintColor3", new Color(0, 1, 1, 0.66f));

            config.SettingChanged += OnConfigSettingChanged;

            // Harmony patches
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
        }

        static void OnConfigSettingChanged(object sender, EventArgs args)
        {
            Debug.Log($"{PluginInfo.PLUGIN_NAME} OnConfigSettingChanged");
            Plugin.Instance?.Config?.Reload();
        }
    }
}
