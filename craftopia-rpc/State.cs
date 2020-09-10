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
        public string PlayState => ServerState == OcServerState.Run ? IsHost
                    ? $"Multiplayer (Host)"
                    : $"Multiplayer (Guest)"
                    : "Solo";
        private int DungeonID;
        public bool isDungeon => DungeonID == -1 ? false : true;
        public bool isCombatDungeon;
        public string FieldState => isDungeon
                    ? $"in {SingletonMonoBehaviour<OcDungeonManager>.Inst.GetDungeonData(DungeonID).DungeonName}"
                    : "in Overworld";
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
            DungeonID = OcPlMaster.Inst.CurrentDungeonId;
            isCombatDungeon = (bool)(SingletonMonoBehaviour<OcDungeonManager>.Inst.GetDungeonData(DungeonID)?.isCombatDungeon);
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
                   isDungeon == state.isDungeon &&
                   isCombatDungeon == state.isCombatDungeon &&
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
                   isDungeon == state.isDungeon &&
                   isCombatDungeon == state.isCombatDungeon &&
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
