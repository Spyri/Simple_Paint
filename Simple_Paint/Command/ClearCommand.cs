using System;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Simple_Paint.ViewModel;

namespace Simple_Paint.Command
{
    public class ClearCommand : ICommand
    {
        private readonly SimplePaintViewModel _simplePaintViewModel;

        public ClearCommand(SimplePaintViewModel simplePaintViewModel)
        {
            _simplePaintViewModel = simplePaintViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _simplePaintViewModel.ImageSave.Clear();
            _simplePaintViewModel.GetFirstImage().CopyPixels(_simplePaintViewModel.GetImageData(),_simplePaintViewModel.GetStride(),0); 
            _simplePaintViewModel.Imagesource = _simplePaintViewModel.GetFirstImage(); 
            _simplePaintViewModel.ImageSave.Push(new TempImage(_simplePaintViewModel.Imagesource,_simplePaintViewModel.GetStride()));
            _simplePaintViewModel.Imagesource = BitmapSource.Create(_simplePaintViewModel.Imagesource.PixelWidth, _simplePaintViewModel.Imagesource.PixelHeight, _simplePaintViewModel.Imagesource.DpiX,
                _simplePaintViewModel.Imagesource.DpiY, _simplePaintViewModel.Imagesource.Format, _simplePaintViewModel.Imagesource.Palette, _simplePaintViewModel.GetImageData(), _simplePaintViewModel.Imagesource.PixelWidth*(_simplePaintViewModel.Imagesource.Format.BitsPerPixel/8));
        }

        public event EventHandler CanExecuteChanged;
    }
}