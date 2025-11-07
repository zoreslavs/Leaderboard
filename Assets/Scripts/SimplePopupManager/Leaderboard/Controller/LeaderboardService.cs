using System.Threading.Tasks;
using System.Linq;
using UnityEngine;
using System;

namespace SimplePopupManager
{
    [Serializable]
    public class LeaderboardRoot
    {
        public LeaderboardItem[] leaderboard;
    }

    public class LeaderboardService
    {
        private const string JSON_PATH = "Leaderboard";

        private LeaderboardItem[] _cache;

        public async Task<LeaderboardItem[]> LoadAsync(bool forceReload = false)
        {
            if (!forceReload && _cache != null && _cache.Length > 0)
            {
                Debug.Log($"[LeaderboardService] Using cached leaderboard ({_cache.Length} items).");
                return _cache;
            }

            Debug.Log("[LeaderboardService] Start loading Leaderboard.json...");

            var request = Resources.LoadAsync<TextAsset>(JSON_PATH);
            while (!request.isDone)
            {
                await Task.Yield();
            }

            var asset = request.asset as TextAsset;
            if (asset == null || string.IsNullOrWhiteSpace(asset.text))
            {
                Debug.LogError("[LeaderboardService] No Leaderboard.json or empty.");
                _cache = Array.Empty<LeaderboardItem>();
                return _cache;
            }

            var json = asset.text;
            Debug.Log($"[LeaderboardService] Loaded JSON text length: {json.Length}.");

            var root = JsonUtility.FromJson<LeaderboardRoot>(json);
            LeaderboardItem[] items = root?.leaderboard;

            if (items == null || items.Length == 0)
            {
                Debug.LogWarning("[LeaderboardService] Parsed 0 items.");
                _cache = Array.Empty<LeaderboardItem>();
                return _cache;
            }

            Debug.Log($"[LeaderboardService] Parsed {items.Length} items.");

            var sorted = items.OrderByDescending(i => i.score).ToArray();
            _cache = sorted;
            return _cache;
        }
    }
}