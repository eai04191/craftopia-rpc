using System;
using BepInEx;
using Oc;
using Oc.Maps;
using Oc.Dungeon;
using UnityEngine;
using UniRx;

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
                OcServerState ServerState = SingletonMonoBehaviour<OcNetMng>.Inst.ServerState;
                bool IsHost = SingletonMonoBehaviour<OcNetMng>.Inst.IsHost;
                int PlayerCountMax = OcNetMng.MAX_PLAYER_NUM;
                int PlayerCount = SingletonMonoBehaviour<OcNetMng>.Inst.ConnectPlayerNum;
                string PlayState = ServerState == OcServerState.Run ? IsHost
                    ? $"Multiplayer (Host) ({PlayerCount}/{PlayerCountMax})"
                    : $"Multiplayer (Guest) ({PlayerCount}/{PlayerCountMax})"
                    : "Solo";

                int DungeonID = OcPlMaster.Inst.CurrentDungeonId;
                OcDungeonSo Dungeon = SingletonMonoBehaviour<OcDungeonManager>.Inst.GetDungeonData(DungeonID);
                string DungeonName = Dungeon.DungeonName;
                string FieldState = DungeonID == -1 ? "in Overworld" : $"in Dungeon ({DungeonName})";

                string PlayerName = SingletonMonoBehaviour<OcPlCharacterManager>.Inst.SelectedCharaMakeData.Name;
                int PlayerLevel = OcPlMaster.Inst.PlLevelCtrl.Level.Value;
                int MapLevel = SingletonMonoBehaviour<OcWorldManager>.Inst.WorldLevel_Map;
                Logger.LogInfo($"{PlayerName} Lv.{PlayerLevel} {FieldState} 島Lv.{MapLevel} {PlayState}");
            } else {
                Logger.LogInfo("Not in Game");
            }
        }

    }
}
