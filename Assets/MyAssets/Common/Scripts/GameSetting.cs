namespace StShoot.Common
{
    public class GameSetting
    {
        public GameLevel Level;
        public string RoomID;
        
        public GameSetting(GameLevel level, string roomID)
        {
            Level = level;
            RoomID = roomID;
        }
    }
}
