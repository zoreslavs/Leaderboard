namespace SimplePopupManager
{
    public class ClosePopupButton : PopupButtonBase
    {
        protected override void OnClick()
        {
            if (Validate())
                PopupServices.Manager.ClosePopup(_popupName);
        }
    }
}