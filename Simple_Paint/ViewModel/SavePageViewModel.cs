using Simple_Paint.Command;

namespace Simple_Paint.ViewModel
{
    public class SavePageViewModel 
    {
        public bool PngSaveButton { get; set; }
        public bool JpegSaveButton { get; set; }
        public SaveButtonCommand Sbc { get; set; }
        
        public SavePageViewModel()
        {
            Sbc = new SaveButtonCommand(this);
            
        }
    }
}