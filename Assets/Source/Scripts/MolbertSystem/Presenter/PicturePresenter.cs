using UnityEngine;
using MolbertSystem.View;

namespace MolbertSystem.Presenter
{
    public class PicturePresenter
    {
        private readonly PictureView _pictureView;

        public PicturePresenter(PictureView pictureView)
        {
            _pictureView = pictureView;
        }

        public void Move(Transform colorBlock, Vector3 tasrgetBound)
        {
            _pictureView.Move(colorBlock, tasrgetBound);
        }
    }
}
