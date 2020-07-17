using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Simple_Paint.ViewModel;

namespace Simple_Paint.Command
{
    public class ButtonUndoCommand : ICommand
    {
        private readonly SimplePaintViewModel _simplePaintViewModel;
        
        public ButtonUndoCommand(SimplePaintViewModel simplePaintViewModel)
        {
            _simplePaintViewModel = simplePaintViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_simplePaintViewModel.ImageSave.Count-1 >= 0)
            {
                if(_simplePaintViewModel.ImageSave.Count-1 == 0)
                {
                    _simplePaintViewModel.ImageSave.Clear();
                    _simplePaintViewModel.GetFirstImage().CopyPixels(_simplePaintViewModel.GetImageData(),_simplePaintViewModel.GetStride(),0);
                    _simplePaintViewModel.Imagesource = _simplePaintViewModel.GetFirstImage();
                    _simplePaintViewModel.ImageSave.Push(new TempImage(_simplePaintViewModel.Imagesource,_simplePaintViewModel.GetStride()));
                    return;
                }
                _simplePaintViewModel.ImageSave.Pop();
                var tempImage = _simplePaintViewModel.ImageSave.Peek();
                _simplePaintViewModel.SetImageData(tempImage.TempPixelData);
                _simplePaintViewModel.Imagesource = BitmapSource.Create(tempImage.Tempbmp.PixelWidth, tempImage.Tempbmp.PixelHeight,tempImage.Tempbmp.DpiX,tempImage.Tempbmp.DpiY,tempImage.Tempbmp.Format,tempImage.Tempbmp.Palette,_simplePaintViewModel.GetImageData(),_simplePaintViewModel.GetStride());
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}