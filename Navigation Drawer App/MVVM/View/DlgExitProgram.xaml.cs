using Navigation_Drawer_App.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Navigation_Drawer_App.MVVM.View
{
    /// <summary>
    /// Interaction logic for DlgMessageBox.xaml
    /// </summary>
    public partial class DlgExitProgram : Window
    {
        public bool DlgResult = false;
        public DlgExitProgram()
        {
            InitializeComponent();
            MyLib.SetThemeColor((Color)ColorConverter.ConvertFromString("#64b5f6"), (Color)ColorConverter.ConvertFromString("#f5f5f5"));
            
        }

        private void TitleMessage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var buttonName = ((Button)sender).Name;
            switch (buttonName)
            {
                case "YesButton":
                    DlgResult = true;
                    break;
                case "NoButton":
                    DlgResult = false;
                    break;
                default:
                    break;
            }
            Close();
        }
    }
}
