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
        private State CurrentState;
        private State PreviousState;
        private long PreviousStateTimestamp;
        private float SendInterval;
        private float CoolDown;

        void Awake()
        {
            // Check currrent state every 5 seconds.
            SendInterval = 5;
            CoolDown = SendInterval;

            DiscordRPC.EventHandlers eventHandlers = default;
            eventHandlers.readyCallback = (DiscordRPC.ReadyCallback)Delegate.Combine(eventHandlers.readyCallback, new DiscordRPC.ReadyCallback(ReadyCallback));
            eventHandlers.disconnectedCallback = (DiscordRPC.DisconnectedCallback)Delegate.Combine(eventHandlers.disconnectedCallback, new DiscordRPC.DisconnectedCallback(DisconnectedCallback));
            eventHandlers.errorCallback = (DiscordRPC.ErrorCallback)Delegate.Combine(eventHandlers.errorCallback, new DiscordRPC.ErrorCallback(ErrorCallback));

            DiscordRPC.Initialize("752972693745172550", ref eventHandlers, true, "0612");
            prsnc = default;
            SetStatus();
            ReadyCallback();
        }

        private static void ErrorCallback(int errorCode, string message)
        {
            UnityEngine.Debug.Log($"ErrorCallback: {errorCode}: {message}");
        }

        private static void DisconnectedCallback(int errorCode, string message)
        {
            UnityEngine.Debug.Log($"DisconnectedCallback: {errorCode}: {message}");
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
                CheckStatus();
            }
        }

        void CheckStatus()
        {
            if (!OcGameMng.Inst) {
                Logger.LogInfo("Not in Game");
                SetStatus();
                return;
            }

            CurrentState = new State();
            if (!CurrentState.IsSameState(PreviousState)) {
                Logger.LogInfo("State has Changed!");
                Logger.LogInfo($"{CurrentState.PlayerName} Lv.{CurrentState.PlayerLevel} {CurrentState.FieldState} Map Lv.{CurrentState.MapLevel} ID:{CurrentState.MapID} {CurrentState.PlayState} ({CurrentState.PlayerCount} / {CurrentState.PlayerCountMax})");
                SetStatus(CurrentState);
                PreviousState = CurrentState.Clone();
            } else {
                Logger.LogInfo("State is not Change. Keep Going!");
            }
        }

        void SetStatus(State State)
        {
            long Timestamp = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
            if (State.IsSameStateExceptPlayerCount(PreviousState)) {
                Logger.LogInfo("Only the number of PlayerCount. Use previous Timestamp.");
                Timestamp = PreviousStateTimestamp;
            }
            Logger.LogInfo(Timestamp);

            prsnc.state = $"Playing {State.PlayState}";
            prsnc.details = $"{State.PlayerName} Lv.{State.PlayerLevel} in Lv.{State.MapLevel} island";
            prsnc.startTimestamp = Timestamp;
            prsnc.largeImageKey = State.IsDungeon ? State.IsCombatDungeon ? "dungeon2" : "dungeon1" : "overworld";
            prsnc.largeImageText = State.FieldState;
            prsnc.smallImageKey = "logo";
            prsnc.smallImageText = "Craftopia by POCKET PAIR, Inc.";
            if (State.PlayState != "Solo") {
                prsnc.partySize = State.PlayerCount;
                prsnc.partyMax = State.PlayerCountMax;
            }
            DiscordRPC.UpdatePresence(ref prsnc);
            PreviousStateTimestamp = Timestamp;
        }

        static void SetStatus()
        {
            prsnc.state = "Main Menu";
            prsnc.details = null;
            prsnc.startTimestamp = 0;
            prsnc.largeImageKey = "logo_big";
            prsnc.largeImageText = "Craftopia by POCKET PAIR, Inc.";
            prsnc.smallImageKey = null;
            prsnc.smallImageText = null;
            prsnc.partySize = 0;
            prsnc.partyMax = 0;
            DiscordRPC.UpdatePresence(ref prsnc);
        }

    }
}
