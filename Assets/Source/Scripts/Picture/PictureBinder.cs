using System.Collections.Generic;

public class PictureBinder
{
    public Picture Bind(PictureView pictureView, List<ColorBlock> blockModels)
    {
        var pictureModel = new Picture();
        var picturePresenter = new PicturePresenter(pictureModel, pictureView);
        picturePresenter.Initialize(blockModels);

        return pictureModel;
    }
}
