using System.Collections.Generic;

public class PictureBinder
{
    public Picture Bind(PictureView pictureView, List<ColorBlock> blockModels)
    {
        var pictureModel = new Picture(blockModels);
        var picturePresenter = new PicturePresenter(pictureModel, pictureView);

        return pictureModel;
    }
}
