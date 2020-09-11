using Oc;
using Oc.Maps;
using Oc.Dungeon;

namespace CraftopiaRPC
{
    class State
    {
        private OcServerState ServerState;
        private bool IsHost;
        public int PlayerCountMax;
        public int PlayerCount;
        public string PlayState;
        private int DungeonID;
        public bool IsDungeon;
        private OcDungeonSo DungeonData;
        public bool IsCombatDungeon;
        public string FieldState;
        public string PlayerName;
        public int PlayerLevel;
        public int MapID;
        public int MapLevel;

        public State()
        {
            ServerState = SingletonMonoBehaviour<OcNetMng>.Inst.ServerState;
            IsHost = SingletonMonoBehaviour<OcNetMng>.Inst.IsHost;
            PlayerCountMax = OcNetMng.MAX_PLAYER_NUM;
            PlayerCount = SingletonMonoBehaviour<OcNetMng>.Inst.ConnectPlayerNum;
            PlayState = ServerState == OcServerState.Run ? IsHost
                ? "Multiplayer as Host"
                : "Multiplayer as Guest"
                : "Solo";
            DungeonID = OcPlMaster.Inst.CurrentDungeonId;
            IsDungeon = DungeonID != -1;
            DungeonData = SingletonMonoBehaviour<OcDungeonManager>.Inst.GetDungeonData(DungeonID);
            IsCombatDungeon = IsDungeon ? DungeonData.isCombatDungeon : false;
            FieldState = IsDungeon
                ? SingletonMonoBehaviour<OcDungeonManager>.Inst.GetDungeonData(DungeonID).DungeonName
                : "Overworld";
            PlayerName = SingletonMonoBehaviour<OcPlCharacterManager>.Inst.SelectedCharaMakeData.Name;
            PlayerLevel = OcPlMaster.Inst.PlLevelCtrl.Level.Value;
            MapID = SingletonMonoBehaviour<OcWorldManager>.Inst.CurrentIsland.ID;
            MapLevel = SingletonMonoBehaviour<OcWorldManager>.Inst.WorldLevel_Map;
        }

        public bool IsSameState(object obj)
        {
            return obj is State state &&
                   ServerState == state.ServerState &&
                   IsHost == state.IsHost &&
                   PlayerCountMax == state.PlayerCountMax &&
                   PlayerCount == state.PlayerCount &&
                   PlayState == state.PlayState &&
                   DungeonID == state.DungeonID &&
                   IsDungeon == state.IsDungeon &&
                   DungeonData == state.DungeonData &&
                   IsCombatDungeon == state.IsCombatDungeon &&
                   FieldState == state.FieldState &&
                   PlayerName == state.PlayerName &&
                   PlayerLevel == state.PlayerLevel &&
                   MapID == state.MapID &&
                   MapLevel == state.MapLevel;
        }
        public bool IsSameStateExceptPlayerCount(object obj)
        {
            return obj is State state &&
                   ServerState == state.ServerState &&
                   IsHost == state.IsHost &&
                   //PlayerCountMax == state.PlayerCountMax &&
                   //PlayerCount == state.PlayerCount &&
                   PlayState == state.PlayState &&
                   DungeonID == state.DungeonID &&
                   IsDungeon == state.IsDungeon &&
                   DungeonData == state.DungeonData &&
                   IsCombatDungeon == state.IsCombatDungeon &&
                   FieldState == state.FieldState &&
                   PlayerName == state.PlayerName &&
                   PlayerLevel == state.PlayerLevel &&
                   MapID == state.MapID &&
                   MapLevel == state.MapLevel;
        }

        public State Clone()
        {
            return (State)MemberwiseClone();
        }
    }
}
