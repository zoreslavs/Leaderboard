using System.Threading.Tasks;
using UnityEngine;

namespace SimplePopupManager
{
    public class LeaderboardPopupController : MonoBehaviour, IPopupInitialization
    {
        [SerializeField] private LeaderboardPopupView _view;
        [SerializeField] private LeaderboardPlayerTypeStyles _styles;

        private readonly LeaderboardService _service = new();
        private readonly LeaderboardAvatarCache _cache = new();

        public async Task Init(object param)
        {
            if (_view == null)
            {
                Debug.LogError("[LeaderboardPopupController] View reference missing.");
                return;
            }

            _view.CloseButton.onClick.AddListener(() =>
            {
                PopupServices.Manager?.ClosePopup(PopupNames.Leaderboard);
            });

            await PopulateAsync();
        }

        private void OnDestroy()
        {
            if (_view != null)
                _view.CloseButton.onClick.RemoveAllListeners();
        }

        private async Task PopulateAsync()
        {
            foreach (Transform child in _view.ContentRoot)
            {
                Destroy(child.gameObject);
            }

            var items = await _service.LoadAsync();
            if (items == null || items.Length == 0)
            {
                Debug.LogWarning("[LeaderboardPopupController] No items to display.");
                return;
            }

            foreach (var item in items)
            {
                var (color, scale) = _styles.Get(item.PlayerType);
                var view = Instantiate(_view.ItemPrefab, _view.ContentRoot);
                view.SetFields(item, color, scale);

                _ = LoadAvatarAsync(item.avatar, view);
            }
        }

        private async Task LoadAvatarAsync(string url, LeaderboardItemView view)
        {
            if (string.IsNullOrEmpty(url))
            {
                Debug.LogWarning($"[LeaderboardPopupController] Missing avatar URL.");
                return;
            }

            var sprite = await _cache.GetOrLoad(url);

            if (view != null)
                view.SetAvatar(sprite);
        }
    }
}