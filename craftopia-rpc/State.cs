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
        public string FieldState => DungeonID == -1
                    ? "in Overworld"
                    : $"in Dungeon ({SingletonMonoBehaviour<OcDungeonManager>.Inst.GetDungeonData(DungeonID).DungeonName})";
        public string PlayerName;
        public int PlayerLevel;
        public int MapLevel;


        public State()
        {
            ServerState = SingletonMonoBehaviour<OcNetMng>.Inst.ServerState;
            IsHost = SingletonMonoBehaviour<OcNetMng>.Inst.IsHost;
            PlayerCountMax = OcNetMng.MAX_PLAYER_NUM;
            PlayerCount = SingletonMonoBehaviour<OcNetMng>.Inst.ConnectPlayerNum;
            DungeonID = OcPlMaster.Inst.CurrentDungeonId;
            PlayerName = SingletonMonoBehaviour<OcPlCharacterManager>.Inst.SelectedCharaMakeData.Name;
            PlayerLevel = OcPlMaster.Inst.PlLevelCtrl.Level.Value;
            MapLevel = SingletonMonoBehaviour<OcWorldManager>.Inst.WorldLevel_Map;
        }
    }
}
