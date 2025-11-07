using UnityEngine.UI;
using UnityEngine;

namespace SimplePopupManager
{
    public class LeaderboardPopupView : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private LeaderboardItemView _itemPrefab;
        [SerializeField] private Transform _contentRoot;
        [SerializeField] private Button _closeButton;

        public LeaderboardItemView ItemPrefab => _itemPrefab;
        public Transform ContentRoot => _contentRoot;
        public Button CloseButton => _closeButton;
    }
}