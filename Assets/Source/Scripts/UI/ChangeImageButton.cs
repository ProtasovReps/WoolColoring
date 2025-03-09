using UnityEngine;
using UnityEngine.UI;

public class ChangeImageButton : ButtonView
{
    [SerializeField] private Image _activeImage;
    [SerializeField] private Image _inactiveImage;

    private bool _isActive = true;

    protected override void OnButtonClick()
    {
        _isActive = !_isActive;

        if (_isActive == true)
        {
            _inactiveImage.enabled = false;
            _activeImage.enabled = true;
        }
        else
        {
            _inactiveImage.enabled = true;
            _activeImage.enabled = false;
        }
    }
}