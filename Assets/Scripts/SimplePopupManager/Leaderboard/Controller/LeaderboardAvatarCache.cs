using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;

namespace SimplePopupManager
{
    public sealed class LeaderboardAvatarCache
    {
        private readonly Dictionary<string, Sprite> _cache = new();

        public bool TryGetAvatar(string url, out Sprite sprite) => _cache.TryGetValue(url, out sprite);

        public async Task<Sprite> GetOrLoad(string url)
        {
            if (string.IsNullOrEmpty(url))
                return null;

            if (_cache.TryGetValue(url, out var cachedSprite))
                return cachedSprite;

            var loadTask = LoadSpriteAsync(url);
            var sprite = await loadTask;

            if (sprite != null)
                _cache[url] = sprite;

            return sprite;
        }

        private static async Task<Sprite> LoadSpriteAsync(string url)
        {
            using var request = UnityWebRequestTexture.GetTexture(url);
            var operation = request.SendWebRequest();

            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning($"[LeaderboardAvatarCache] Failed to load avatar: {url} ({request.error}).");
                return null;
            }

            var texture = DownloadHandlerTexture.GetContent(request);
            if (texture == null)
            {
                Debug.LogWarning($"[LeaderboardAvatarCache] Empty texture at {url}.");
                return null;
            }

            return Sprite.Create(
                texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f),
                100f
            );
        }
    }
}