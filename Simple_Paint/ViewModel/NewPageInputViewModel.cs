using Simple_Paint.Command;

namespace Simple_Paint.ViewModel
{
    public class NewPageInputViewModel 
    {

        public int Width { get; set; }
        public int Height { get; set; }
        public ButtonCreateCommand B { get; set; }

        public NewPageInputViewModel()
        {
            B = new ButtonCreateCommand(this);
        }
       
        
    }
}