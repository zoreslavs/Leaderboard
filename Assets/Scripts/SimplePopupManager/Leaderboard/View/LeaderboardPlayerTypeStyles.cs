using UnityEngine;

namespace SimplePopupManager
{
    [CreateAssetMenu(menuName = "Leaderboard/LeaderboardPlayerTypeStyles")]
    public class LeaderboardPlayerTypeStyles : ScriptableObject
    {
        [System.Serializable]
        public class Style
        {
            public PlayerType type;
            public Color color = Color.white;
            public float scale = 1f;
        }

        [Header("Player Type Styles")]
        public Style[] styles = new Style[]
        {
            // Diamond
            new Style { type = PlayerType.Diamond, color = HexToColor("#B9F2FF"), scale = 1.2f },
            // Gold
            new Style { type = PlayerType.Gold,    color = HexToColor("#FFD700"), scale = 1.1f },
            // Silver
            new Style { type = PlayerType.Silver,  color = HexToColor("#C0C0C0"), scale = 1.0f },
            // Bronze
            new Style { type = PlayerType.Bronze,  color = HexToColor("#CD7F32"), scale = 0.95f },
            // Default
            new Style { type = PlayerType.Default, color = new Color(1f, 1f, 1f, 0.15f), scale = 0.9f }
        };

        public (Color, float) Get(PlayerType type)
        {
            foreach (var style in styles)
            {
                if (style.type == type)
                    return (style.color, style.scale);
            }

            return (Color.white, 1f);
        }

        private static Color HexToColor(string hex)
        {
            if (ColorUtility.TryParseHtmlString(hex, out Color color))
                return color;

            return Color.white;
        }
    }
}