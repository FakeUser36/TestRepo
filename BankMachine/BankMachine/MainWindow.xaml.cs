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
        //Start Page Variables
        int currentBox = 0;
        bool loginViaAcctNum = false;
        string currentAccountNumber = "";

        enum pages { startPage, removeCardPage, enterPinPage};
        Dictionary<string, int> accountNumbersAndPins = new Dictionary<string,int>();
        int[] balances = { 123, 123, 123 };
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
                    marg = EnterPinPage.Margin;
                    marg.Left = 0;
                    marg.Right = 0;
                    marg.Top = 0;
                    marg.Bottom = 46;
                    EnterPinPage.Margin = marg;
                    break;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            GoToPage(pages.startPage);

            balance_chk.Text = "$" + balances[0] + ".0";
            balance_sav.Text = "$" + balances[1] + ".0";
            balance_tfs.Text = "$" + balances[2] + ".0";

            accountNumbersAndPins.Add("1234123412341234", 1234);
            accountNumbersAndPins.Add("1111222233334444", 1111);
            accountNumbersAndPins.Add("1111111166666666", 1616);

            RemoveCardPage.Visibility = Visibility.Hidden;
            EnterPinPage.Visibility = Visibility.Hidden;
            GoToPage(pages.startPage);
        }
        
        //Start Page Event Handeler Functions

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            AccNumError.Visibility = Visibility.Visible;
            //Check if account # is valid
            string acctNum = PinEntry0.Text+PinEntry1.Text+PinEntry2.Text+PinEntry3.Text;
            if(acctNum.Length!=16){
                AccNumError.Text = "Your account number must be 16 digits long!";
                AccNumError.Visibility = Visibility.Visible;
                return;
            }
            if (accountNumbersAndPins.ContainsKey(acctNum))
            {
                currentAccountNumber = acctNum;
                loginViaAcctNum = true;
                StartPage.Visibility = Visibility.Hidden;
                GoToPage(pages.enterPinPage);
            }
            else {
                AccNumError.Text = "That is not a valid account number, please enter a valid account number.";
                AccNumError.Visibility = Visibility.Visible;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AccNumError.Visibility = Visibility.Hidden;
            switch (currentBox)
            {
                case 0:
                    if (PinEntry0.Text == "----")
                    {
                        //Do nothing
                    }
                    else if (PinEntry0.Text.Length > 1)
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
                    else if (PinEntry1.Text.Length > 1)
                    {
                        PinEntry1.Text = PinEntry1.Text.Substring(0,PinEntry1.Text.Length-1);
                    }
                    else
                    {
                        PinEntry1.Text = "----";
                        currentBox--;
                    }
                    break;
                case 2:
                    if (PinEntry2.Text == "----")
                    {
                        //Do nothing
                    }
                    else if (PinEntry2.Text.Length > 1)
                    {
                        PinEntry2.Text = PinEntry2.Text.Substring(0,PinEntry2.Text.Length-1);
                    }
                    else
                    {
                        PinEntry2.Text = "----";
                        currentBox--;
                    }
                    break;
                case 3:
                    if (PinEntry3.Text == "----")
                    {
                        //Do nothing
                    }
                    else if (PinEntry3.Text.Length > 1)
                    {
                        PinEntry3.Text = PinEntry3.Text.Substring(0,PinEntry3.Text.Length-1);
                    }
                    else
                    {
                        PinEntry3.Text = "----";
                        currentBox--;
                    }
                    break;
                default:
                    break;
            }
        }

        private void enterNumToEntry(object sender, RoutedEventArgs e){

            AccNumError.Visibility = Visibility.Hidden;

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
                    else if (PinEntry3.Text.Length < 4)
                    {
                        PinEntry3.Text += num.ToString();
                    }
                    else 
                    {
                        //The Acct num can only be 16 digits long
                        AccNumError.Text = "Your account number can only be 16 digits long";
                        AccNumError.Visibility = Visibility.Visible;
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
                PinErrorInfo.Visibility = Visibility.Hidden;
                PinEntry.AppendText(num);
            }
            else
            {
                PinEntryErrorMessage.Text = "Pin can only be 4 digits long";
                PinErrorInfo.Visibility = Visibility.Visible;
            }
            
        }

        private void EnterPinOK(object sender, RoutedEventArgs e)
        {
            if (PinEntry.Text.Length != 4)
            {                
                PinEntryErrorMessage.Text = "Pin must be 4 digits long";
                PinErrorInfo.Visibility = Visibility.Visible;
                return;
            }
            //Check if pin # is valid
            if (true)
            {
                EnterPinPage.Visibility = Visibility.Hidden;
                if (loginViaAcctNum)
                {
                    GoToPage(pages.removeCardPage);
                }
                else
                {
                    GoToPage(pages.removeCardPage);
                }
            }
            else {
                PinEntryErrorMessage.Text = "Pin is not valid";
                PinErrorInfo.Visibility = Visibility.Visible;
            }
            
            
        }

        private void EnterPinBack(object sender, RoutedEventArgs e)
        {
            PinErrorInfo.Visibility = Visibility.Hidden;
            if (PinEntry.Text.Length > 1)
            {
                PinEntry.Text = PinEntry.Text.Substring(0, PinEntry.Text.Length - 1);
            }
            else {
                PinEntry.Text = "";
            }
        }
    }
}
