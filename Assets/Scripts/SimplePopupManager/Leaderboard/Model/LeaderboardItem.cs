using System;

namespace SimplePopupManager
{
    public enum PlayerType
    {
        Diamond,
        Gold,
        Silver,
        Bronze,
        Default
    }

    [Serializable]
    public class LeaderboardItem
    {
        public string type;
        public string avatar;
        public string name;
        public int score;

        public PlayerType PlayerType =>
            Enum.TryParse(type, true, out PlayerType parsed) ? parsed : PlayerType.Default;
    }

    [Serializable]
    public class LeaderboardItems
    {
        public LeaderboardItem[] items;
    }
}