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

namespace BankMachine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int currentBox = 0;
        enum pages { startPage, removeCardPage, enterPinPage };
        List<string> accountNumbers = new List<string>();
    
        private void GoToPage(pages page)
        {
            Thickness marg = new Thickness();
            switch (page)
            {
                case pages.startPage:
                    StartPage.Visibility = Visibility.Visible;
                    marg = StartPage.Margin;
                    marg.Left = 0;
                    marg.Right = 0;
                    marg.Top = 0;
                    marg.Bottom = 46;
                    StartPage.Margin = marg;
                    break;
                case pages.removeCardPage:
                    RemoveCardPage.Visibility = Visibility.Visible;
                    marg = RemoveCardPage.Margin;
                    marg.Left = 0;
                    marg.Right = 0;
                    marg.Top = 0;
                    marg.Bottom = 46;
                    RemoveCardPage.Margin = marg;
                    break;
                case pages.enterPinPage:
                    EnterPinPage.Visibility = Visibility.Visible;
                    marg = RemoveCardPage.Margin;
                    marg.Left = 0;
                    marg.Right = 0;
                    marg.Top = 0;
                    marg.Bottom = 46;
                    RemoveCardPage.Margin = marg;
                    break;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            RemoveCardPage.Visibility = Visibility.Hidden;
            EnterPinPage.Visibility = Visibility.Hidden;
            GoToPage(pages.startPage);
        }
        
        //Start Page Event Handeler Functions

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            //Check if account # is valid
            StartPage.Visibility = Visibility.Hidden;
            GoToPage(pages.enterPinPage);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            switch (currentBox)
            {
                case 0:
                    if (PinEntry0.Text == "----")
                    {
                        //Do nothing
                    }
                    else if (PinEntry0.Text.Length != 1)
                    {
                        PinEntry0.Text = PinEntry0.Text.Substring(0,PinEntry0.Text.Length-1);
                    }
                    else
                    {
                        PinEntry0.Text = "----";
                    }
                    break;
                case 1:
                    if (PinEntry1.Text == "----")
                    {
                        //Do nothing
                    }
                    else if (PinEntry1.Text.Length != 1)
                    {
                        PinEntry1.Text = PinEntry1.Text.Substring(0,PinEntry1.Text.Length-1);
                    }
                    else
                    {
                        PinEntry1.Text = "----";
                        currentBox--;
                        Back_Click(sender, e);
                    }
                    break;
                case 2:
                    if (PinEntry2.Text == "----")
                    {
                        //Do nothing
                    }
                    else if (PinEntry2.Text.Length != 1)
                    {
                        PinEntry2.Text = PinEntry2.Text.Substring(0,PinEntry2.Text.Length-1);
                    }
                    else
                    {
                        PinEntry2.Text = "----";
                        currentBox--;
                        Back_Click(sender, e);
                    }
                    break;
                case 3:
                    if (PinEntry3.Text == "----")
                    {
                        //Do nothing
                    }
                    else if (PinEntry3.Text.Length != 1)
                    {
                        PinEntry3.Text = PinEntry3.Text.Substring(0,PinEntry3.Text.Length-1);
                    }
                    else
                    {
                        PinEntry3.Text = "----";
                        currentBox--;
                        Back_Click(sender, e);
                    }
                    break;
                default:
                    break;
            }
        }

        private void enterNumToEntry(object sender, RoutedEventArgs e){
            string num = (((Button)sender).Content.ToString());
            switch (currentBox)
            {
                case 0:
                    if (PinEntry0.Text == "----")
                    {
                        PinEntry0.Text = num.ToString();
                    }
                    else if (PinEntry0.Text.Length != 4)
                    {
                        PinEntry0.Text += num.ToString();
                    }
                    else 
                    {
                        currentBox++;
                        enterNumToEntry(sender,e);
                    }
                    break;
                case 1:
                    if (PinEntry1.Text == "----")
                    {
                        PinEntry1.Text = num.ToString();
                    }
                    else if (PinEntry1.Text.Length != 4)
                    {
                        PinEntry1.Text += num.ToString();
                    }
                    else 
                    {
                        currentBox++;
                        enterNumToEntry(sender,e);
                    }
                    break;
                case 2:
                    if (PinEntry2.Text == "----")
                    {
                        PinEntry2.Text = num.ToString();
                    }
                    else if (PinEntry2.Text.Length != 4)
                    {
                        PinEntry2.Text += num.ToString();
                    }
                    else 
                    {
                        currentBox++;
                        enterNumToEntry(sender,e);
                    }
                    break;
                case 3:
                    if (PinEntry3.Text == "----")
                    {
                        PinEntry3.Text = num.ToString();
                    }
                    else if (PinEntry3.Text.Length != 4)
                    {
                        PinEntry3.Text += num.ToString();
                    }
                    else 
                    {
                        //Do nothing
                    }
                    break;
                default:
                    break;
            }
        }

        private void EnterCard(object sender, MouseButtonEventArgs e)
        {

        }

        //Pin Entry Page Event Handler Functions

        private void enterNumToPin(object sender, RoutedEventArgs e)
        {
            string num = (((Button)sender).Content.ToString());
            if (PinEntry.Text.Length < 4)
            {
                PinErrorInfo.Visibiltiy
                PinEntry.AppendText(num);
            }
            else{ 
                
            }
            
        }

        private void EnterPinOK(object sender, RoutedEventArgs e)
        {
            //Check if pin # is valid
            EnterPinPage.Visibility = Visibility.Hidden;
            GoToPage(pages.removeCardPage);
        }

        private void EnterPinBack(object sender, RoutedEventArgs e)
        {

        }
        
    }
}
