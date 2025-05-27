using MolbertSystem.Model;
using MolbertSystem.View;
using MolbertSystem.Presenter;
using ColorBlockSystem;
using ColorBlockSystem.Model;

namespace MolbertSystem
{
    public class PictureBinder
    {
        private readonly PictureView _pictureView;
        private readonly Malbert _malbert;
        private readonly ColorBlockBinder _colorBlockBinder;

        public PictureBinder(PictureView pictureView, ColorBlockBinder colorBlockBinder, Malbert malbert)
        {
            _pictureView = pictureView;
            _colorBlockBinder = colorBlockBinder;
            _malbert = malbert;
        }

        public Picture Bind()
        {
            ColorBlock[] colorBlocks = _colorBlockBinder.Bind();
            Picture pictureModel = new(colorBlocks);
            PicturePresenter picturePresenter = new(_pictureView);

            _malbert.Initilize(picturePresenter);
            return pictureModel;
        }
    }
}