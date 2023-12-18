﻿using BepInEx;
using UnityEngine;
using TMPro;
using GameNetcodeStuff;
using HarmonyLib;
using System.Collections.Generic;
using System;
using Unity.Netcode;
using System.Text.RegularExpressions;
using System.Reflection;
using BepInEx.Logging;
using System.Text;

namespace PlayerMapNumbers
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource StaticLogger;
        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            StaticLogger = Logger;
            Harmony HarmonyInstance = new Harmony(PluginInfo.PLUGIN_GUID);
            Logger.LogInfo($"Attempting to patch with Harmony!");
            try
            {
                HarmonyInstance.PatchAll();
                Logger.LogInfo($"Patching success!");
            }
            catch (Exception ex)
            {
                StaticLogger.LogError("Failed to patch: " + ex?.ToString());
            }


        }
        public static void AddPlayerNumber(UnityEngine.GameObject player, int number)
        {
            Plugin.StaticLogger.LogInfo($"Adding index {number}");
            var parent = player.transform.Find("Misc").Find("MapDot").gameObject;
            GameObject labelObject = parent.transform.Find("PlayerNumberLabel")?.gameObject;
            TextMeshPro textRef;
            if (labelObject == null)
            {
                labelObject = new GameObject();
                labelObject.transform.SetParent(parent.transform, false);
                labelObject.transform.SetLocalPositionAndRotation(new Vector3(0, 0.5f, 0), Quaternion.Euler(new Vector3(90, 0, 0)));
                labelObject.transform.localScale = Vector3.one / 2.0f;
                labelObject.layer = parent.layer;
                labelObject.name = "PlayerNumberLabel";
                textRef = labelObject.AddComponent<TextMeshPro>();
                textRef.alignment = TextAlignmentOptions.Center;
                textRef.autoSizeTextContainer = true;
                textRef.maxVisibleLines = 1;
                textRef.maxVisibleWords = 1;
            }
            else
            {
                textRef = labelObject.transform.GetComponent<TextMeshPro>();
            }
            textRef.text = ( 1 + number ).ToString();
        }

        static public void UpdateNumbers()
        {
            if (StartOfRound.Instance?.mapScreen == null)
            {
                return;
            }
            for (int index = 0; index < StartOfRound.Instance.mapScreen.radarTargets.Count; ++index)
            {
                var transAndName = StartOfRound.Instance.mapScreen.radarTargets[index];
                if (transAndName.transform != null)
                {
                    AddPlayerNumber(transAndName.transform.gameObject, index);
                }
            }
        }

    }

    [HarmonyPatch(typeof(ManualCameraRenderer), "Awake")]
    public static class ManualCameraRendererAwakePatch
    {
        public static void Postfix(ManualCameraRenderer __instance)
        {
            Plugin.StaticLogger.LogInfo("ManualCameraRendererAwakePatch patch run");
            NetworkManager networkManager = __instance.NetworkManager;
            if ((UnityEngine.Object)networkManager == (UnityEngine.Object)null || !networkManager.IsListening)
                return;
            Plugin.UpdateNumbers();
        }
    }

    [HarmonyPatch(typeof(ManualCameraRenderer), "RemoveTargetFromRadar")]
    public static class ManualCameraRendererRemoveTargetFromRadarPatch
    {
        public static void Postfix(ManualCameraRenderer __instance, Transform removeTransform)
        {
            Plugin.StaticLogger.LogInfo("ManualCameraRendererRemoveTargetFromRadarPatch patch run");
            Plugin.UpdateNumbers();
        }
    }

    [HarmonyPatch(typeof(ManualCameraRenderer), "AddTransformAsTargetToRadar")]
    public static class ManualCameraRendererAddTransformAsTargetToRadarPatch
    {
        public static void Postfix(ManualCameraRenderer __instance, Transform newTargetTransform, string targetName, bool isNonPlayer)
        {
            Plugin.StaticLogger.LogInfo("ManualCameraRendererAddTransformAsTargetToRadar patch run");
            Plugin.UpdateNumbers();
        }
    }

    [HarmonyPatch(typeof(Terminal), "ParsePlayerSentence")]
    public static class TerminalParsePlayerSentencePatch
    {
        static private string RemovePunctuation(string s)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (char c in s)
            {
                if (!char.IsPunctuation(c))
                    stringBuilder.Append(c);
            }
            return stringBuilder.ToString().ToLower();
        }
        public static void Postfix(Terminal __instance, ref TerminalNode __result)
        {
            Plugin.StaticLogger.LogInfo("TerminalParsePlayerSentence patch run");
            // 10, 11, 12 parse errors
            if (__result == __instance.terminalNodes.specialNodes[10]) {
                Plugin.StaticLogger.LogInfo("Extended Parse");
                string str1 = RemovePunctuation(__instance.screenText.text.Substring(__instance.screenText.text.Length - __instance.textAdded));
                string[] strArray = str1.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                int outputNum;
                if ( strArray.Length == 1 && int.TryParse( strArray[0], out outputNum ) )
                {
                    Plugin.StaticLogger.LogInfo("Number Found");
                    int playerIndex = outputNum - 1;
                    if ( playerIndex < StartOfRound.Instance.mapScreen.radarTargets.Count )
                    {
                        Plugin.StaticLogger.LogInfo("Valid Number");
                        var controller = StartOfRound.Instance.mapScreen.radarTargets[playerIndex].transform.gameObject.GetComponent<PlayerControllerB>();
                        if ( controller != null &&
                             !controller.isPlayerControlled && !controller.isPlayerDead && controller.redirectToEnemy == null )
                        {
                            return;
                        }
                        StartOfRound.Instance.mapScreen.SwitchRadarTargetAndSync(playerIndex);
                        Plugin.StaticLogger.LogInfo("Updated Target");
                        __result = __instance.terminalNodes.specialNodes[20];
                    }
                }
            }
        }
    }

}