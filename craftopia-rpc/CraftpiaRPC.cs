using System;
using BepInEx;

namespace CraftopiaRPC
{
    [BepInPlugin("net.mizle.craftopiarpc", "Craftopia RPC", "1.0.0.0")]
    [BepInProcess("Craftopia.exe")]
    public class CraftopiaRPC : BaseUnityPlugin
    {
        internal static DiscordRPC.RichPresence prsnc;

        void Awake()
        {
            Logger.LogInfo("Hello, world!");
            DiscordRPC.EventHandlers eventHandlers = default(DiscordRPC.EventHandlers);
            eventHandlers.readyCallback = (DiscordRPC.ReadyCallback)Delegate.Combine(eventHandlers.readyCallback, new DiscordRPC.ReadyCallback(ReadyCallback));
            eventHandlers.disconnectedCallback = (DiscordRPC.DisconnectedCallback)Delegate.Combine(eventHandlers.disconnectedCallback, new DiscordRPC.DisconnectedCallback(DisconnectedCallback));
            eventHandlers.errorCallback = (DiscordRPC.ErrorCallback)Delegate.Combine(eventHandlers.errorCallback, new DiscordRPC.ErrorCallback(ErrorCallback));
            eventHandlers.joinCallback = (DiscordRPC.JoinCallback)Delegate.Combine(eventHandlers.joinCallback, new DiscordRPC.JoinCallback(JoinCallback));
            eventHandlers.spectateCallback = (DiscordRPC.SpectateCallback)Delegate.Combine(eventHandlers.spectateCallback, new DiscordRPC.SpectateCallback(SpectateCallback));
            eventHandlers.requestCallback = (DiscordRPC.RequestCallback)Delegate.Combine(eventHandlers.requestCallback, new DiscordRPC.RequestCallback(RequestCallback));
            DiscordRPC.Initialize("752972693745172550", ref eventHandlers, true, "0612");
            prsnc = default(DiscordRPC.RichPresence);
            prsnc.largeImageKey = "logo";
            prsnc.largeImageText = "Craftopia by POCKET PAIR, Inc.";
            prsnc.state = "Main Menu";
            DiscordRPC.UpdatePresence(ref CraftopiaRPC.prsnc);
            ReadyCallback();
        }
        private static void RequestCallback(DiscordRPC.JoinRequest request)
        {
        }

        private static void SpectateCallback(string secret)
        {
        }

        private static void JoinCallback(string secret)
        {
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

    }
}
