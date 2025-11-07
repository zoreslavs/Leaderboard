using UnityEngine.UI;
using UnityEngine;

namespace SimplePopupManager
{
    [RequireComponent(typeof(Button))]
    public abstract class PopupButtonBase : MonoBehaviour
    {
        [SerializeField] protected string _popupName;
        [SerializeField] protected Button _button;

        protected virtual void Awake()
        {
            if (_button == null)
                _button = GetComponent<Button>();
        }

        protected virtual void OnEnable() => _button.onClick.AddListener(OnClick);
        protected virtual void OnDisable() => _button.onClick.RemoveListener(OnClick);

        protected abstract void OnClick();

        protected bool Validate()
        {
            if (PopupServices.Manager == null)
            {
                Debug.LogError($"[{GetType().Name}] PopupServices.Manager is null.");
                return false;
            }

            if (string.IsNullOrEmpty(_popupName))
            {
                Debug.LogError($"[{GetType().Name}] Popup name is empty.");
                return false;
            }

            return true;
        }
    }
}