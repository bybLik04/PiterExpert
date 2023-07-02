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

namespace PiterExp
{
    /// <summary>
    /// Логика взаимодействия для ExitMessageBox.xaml
    /// </summary>
    public partial class ExitMessageBox : Window
    {
        public bool DialogResult { get; private set; }

        public ExitMessageBox()
        {
            InitializeComponent();
        }
       
        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
