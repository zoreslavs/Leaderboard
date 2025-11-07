namespace SimplePopupManager
{
    public class OpenPopupButton : PopupButtonBase
    {
        protected override void OnClick()
        {
            if (Validate())
                PopupServices.Manager.OpenPopup(_popupName, null);
        }
    }
}