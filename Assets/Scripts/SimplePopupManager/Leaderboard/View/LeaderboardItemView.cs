using UnityEngine.UI;
using UnityEngine;
using TMPro;

namespace SimplePopupManager
{
    public class LeaderboardItemView : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private Image _background;

        [Header("Avatar")]
        [SerializeField] private Image _avatarImage;
        [SerializeField] private GameObject _loadingRoot;

        [Header("Size")]
        [SerializeField] private LayoutElement _layout;
        [SerializeField] private float _baseHeight = 120f;

        public void SetFields(LeaderboardItem item, Color color, float sizeScale = 1f)
        {
            _nameText.text = item.name;
            _scoreText.text = item.score.ToString();
            _background.color = color;
            _avatarImage.enabled = false;
            _loadingRoot.SetActive(true);

            if (_layout != null)
                _layout.preferredHeight = _baseHeight * Mathf.Max(0.5f, sizeScale);
        }

        public void SetAvatar(Sprite sprite)
        {
            _avatarImage.sprite = sprite;
            _avatarImage.enabled = sprite != null;
            _loadingRoot.SetActive(false);
        }
    }
}