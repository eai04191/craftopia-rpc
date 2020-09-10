using System;
using BepInEx;
using Oc;
using UnityEngine;

namespace CraftopiaRPC
{
    [BepInPlugin("net.mizle.craftopiarpc", "Craftopia RPC", "1.0.0.0")]
    [BepInProcess("Craftopia.exe")]
    public class CraftopiaRPC : BaseUnityPlugin
    {
        internal static DiscordRPC.RichPresence prsnc;
        private float SendInterval;
        private float CoolDown;

        void Awake()
        {
            SendInterval = 2;
            CoolDown = SendInterval;

            DiscordRPC.EventHandlers eventHandlers = default;
            eventHandlers.readyCallback = (DiscordRPC.ReadyCallback)Delegate.Combine(eventHandlers.readyCallback, new DiscordRPC.ReadyCallback(ReadyCallback));
            eventHandlers.disconnectedCallback = (DiscordRPC.DisconnectedCallback)Delegate.Combine(eventHandlers.disconnectedCallback, new DiscordRPC.DisconnectedCallback(DisconnectedCallback));
            eventHandlers.errorCallback = (DiscordRPC.ErrorCallback)Delegate.Combine(eventHandlers.errorCallback, new DiscordRPC.ErrorCallback(ErrorCallback));

            DiscordRPC.Initialize("752972693745172550", ref eventHandlers, true, "0612");
            prsnc = default;
            prsnc.largeImageKey = "logo";
            prsnc.largeImageText = "Craftopia by POCKET PAIR, Inc.";
            prsnc.state = "Main Menu";
            DiscordRPC.UpdatePresence(ref prsnc);
            ReadyCallback();
        }

        private static void ErrorCallback(int errorCode, string message)
        {
            UnityEngine.Debug.Log("ErrorCallback: " + errorCode + " " + message);
        }

        private static void DisconnectedCallback(int errorCode, string message)
        {
            UnityEngine.Debug.Log("DisconnectedCallback: " + errorCode + " " + message);
        }

        private static void ReadyCallback()
        {
            UnityEngine.Debug.Log("Running");
        }

        void Update()
        {
            if (CoolDown > 0)
                CoolDown -= Time.deltaTime;
            else {
                CoolDown = SendInterval;
                SendStatus();
            }
        }
        void SendStatus()
        {
            if (OcGameMng.Inst) {
                State CurrentState = new State();
                Logger.LogInfo($"{CurrentState.PlayerName} Lv.{CurrentState.PlayerLevel} {CurrentState.FieldState} 島Lv.{CurrentState.MapLevel} {CurrentState.PlayState}");
            } else {
                Logger.LogInfo("Not in Game");
            }
        }

    }
}
