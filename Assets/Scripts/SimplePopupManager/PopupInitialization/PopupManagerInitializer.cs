using UnityEngine;

namespace SimplePopupManager
{
    public class PopupManagerInitializer : MonoBehaviour
    {
        [SerializeField] private RectTransform _popupRoot;
        [SerializeField] private bool _dontDestroyOnLoad = true;

        private void Awake()
        {
            if (_popupRoot == null)
            {
                var canvas = FindObjectOfType<Canvas>();
                if (canvas == null)
                {
                    Debug.LogError("[PopupManagerInitializer] No Canvas found in scene.");
                    return;
                }

                var root = new GameObject("PopupRoot", typeof(RectTransform));
                _popupRoot = root.GetComponent<RectTransform>();
                _popupRoot.SetParent(canvas.transform, false);
                _popupRoot.anchorMin = Vector2.zero;
                _popupRoot.anchorMax = Vector2.one;
                _popupRoot.offsetMin = Vector2.zero;
                _popupRoot.offsetMax = Vector2.zero;
                _popupRoot.localScale = Vector3.one;
            }

            PopupServices.Manager ??= new PopupManagerServiceService(_popupRoot);

            if (_dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);

            Debug.Log("[PopupManagerInitializer] PopupManager initialized successfully.");
        }
    }
}