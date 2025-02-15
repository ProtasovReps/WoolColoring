using System;
using System.Collections.Generic;

public class PictureBinder
{
    public Picture Bind(PictureView pictureView, List<ColorBlock> blockModels, Malbert malbert)
    {
        if (pictureView == null)
            throw new ArgumentNullException(nameof(pictureView));

        if (blockModels.Count == 0)
            throw new EmptyCollectionException();

        if (malbert == null)
            throw new ArgumentNullException(nameof(malbert));

        var pictureModel = new Picture(blockModels);
        var picturePresenter = new PicturePresenter(pictureModel, pictureView);

        malbert.Initilize(picturePresenter);

        return pictureModel;
    }
}
