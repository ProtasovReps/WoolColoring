using UnityEngine;

public class PicturePresenter
{
    private readonly Picture _picture;
    private readonly PictureView _pictureView;

    public PicturePresenter(Picture picture, PictureView pictureView)
    {
        _picture = picture;
        _pictureView = pictureView;
    }

    public void Move(Transform colorBlock, Vector3 tasrgetBound)
    {
        _pictureView.Move(colorBlock, tasrgetBound);
    }
}
