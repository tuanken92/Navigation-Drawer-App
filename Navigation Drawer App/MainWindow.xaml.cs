using Navigation_Drawer_App.MVVM.Model;
using Navigation_Drawer_App.MVVM.View;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Navigation_Drawer_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            // Set tooltip visibility

            //if (Tg_Btn.IsChecked == true)
            //{
            //    tt_home.Visibility = Visibility.Collapsed;
            //    tt_contacts.Visibility = Visibility.Collapsed;
            //    tt_messages.Visibility = Visibility.Collapsed;
            //    tt_maps.Visibility = Visibility.Collapsed;
            //    tt_settings.Visibility = Visibility.Collapsed;
            //    tt_signout.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    tt_home.Visibility = Visibility.Visible;
            //    tt_contacts.Visibility = Visibility.Visible;
            //    tt_messages.Visibility = Visibility.Visible;
            //    tt_maps.Visibility = Visibility.Visible;
            //    tt_settings.Visibility = Visibility.Visible;
            //    tt_signout.Visibility = Visibility.Visible;
            //}

            //var list_view_item = sender as ListViewItem;
            //Console.WriteLine($"Menu = {list_view_item.Name}");
        }

        /*private void ListView_Selected_Item(object sender, MouseEventArgs e)
        {

            var list_view = sender as ListView;
            Console.WriteLine($"Menu = {list_view.Name}");
            Console.WriteLine($"Menu = {list_view.SelectedItem.ToString()}");
        }*/

        private void Tg_Btn_Unchecked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 0.2;
        }

        private void Tg_Btn_Checked(object sender, RoutedEventArgs e)
        {
            img_bg.Opacity = 0.1;
        }

        private void BG_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_Btn.IsChecked = false;
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            DlgExitProgram dlgExitPrg = new DlgExitProgram();
            dlgExitPrg.Owner = this;
            dlgExitPrg.Topmost = true;
            dlgExitPrg.ShowDialog();

            if (dlgExitPrg.DlgResult)
            { Close(); }

        }

        private void MaximizeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;

        }

        private void Title_and_button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
