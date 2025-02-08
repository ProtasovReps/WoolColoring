using System.Collections.Generic;

public class PicturePresenter
{
    private readonly Picture _picture;
    private readonly PictureView _pictureView;

    public PicturePresenter(Picture picture, PictureView pictureView)
    {
        _picture = picture;
        _pictureView = pictureView;
    }
}
