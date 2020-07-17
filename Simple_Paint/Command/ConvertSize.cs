using System;
using System.Globalization;
using System.Windows.Data;


namespace Simple_Paint.Command
{
    public class ConvertSize : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                int i = (int) value; 
                return i.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        { 
            string strValue = value as string;
            int i = Int32.Parse(strValue);
            return i;
        }
    }
}