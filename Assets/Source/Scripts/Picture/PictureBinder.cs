using System;

public class PictureBinder
{
    public Picture Bind(PictureView pictureView, ColorBlock[] blockModels, Malbert malbert)
    {
        if (pictureView == null)
            throw new ArgumentNullException(nameof(pictureView));

        if (blockModels.Length == 0)
            throw new EmptyCollectionException();

        if (malbert == null)
            throw new ArgumentNullException(nameof(malbert));

        var pictureModel = new Picture(blockModels);
        var picturePresenter = new PicturePresenter(pictureModel, pictureView);

        malbert.Initilize(picturePresenter);
        return pictureModel;
    }
}
