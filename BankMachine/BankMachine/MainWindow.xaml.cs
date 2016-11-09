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
    public class Account {
        public string accountNumber;
        public int pin;
        //public Dictionary<string, double> accountsAndBalances = new Dictionary<string, double>();
        public double[] balances;
        public Account(string num, int pass, double[] acctBal)
        {
            accountNumber = num;
            pin = pass;
            balances = acctBal;
        }
    }

    public partial class MainWindow : Window
    {
        //Variables used by more than one page
        enum pages { startPage, enterPinPage, removeCardPage, mainMenuPage, depositPage };
        bool loginViaAcctNum = false;
        Account currentAccount = null;
        pages currentPage;

        //Test accounts
        List<Account> accounts = new List<Account>();

        //Dictionary<string, double> accsBals = new Dictionary<string, double>();
        double[] accsBals = { 1561.22, 12122.35, 5001.02 };
        //Start Page Variables
        int currentBox = 0;

        //Pin Entry Page Variables
        string currentPin = "";

        //Determines if an account # is valid, if so, it returns true adn assigns the found account to the current account value
        private bool determineValidAcctNum(string num){
            currentAccount = accounts.Find(x => x.accountNumber == num);
            if (currentAccount == null)
            {
                return false;
            }
            return true;
        }

        //Used to reposition grids and make them visible to make switching easy
        private void GoToPage(pages page)
        {

            Thickness marg = new Thickness();

            //Set current page to hidden
            switch(currentPage){
                case pages.startPage:
                    StartPage.Visibility = Visibility.Hidden;
                    break;
                case pages.enterPinPage:
                    EnterPinPage.Visibility = Visibility.Hidden;
                    break;
                case pages.mainMenuPage:
                    MainMenuPage.Visibility = Visibility.Hidden;
                    break;
                case pages.depositPage:
                    DepositPage.Visibility = Visibility.Hidden;
                    break;
                default:
                    break;
            }

            //Set desired page to visible
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
                case pages.enterPinPage:
                    EnterPinPage.Visibility = Visibility.Visible;
                    marg = EnterPinPage.Margin;
                    marg.Left = 0;
                    marg.Right = 0;
                    marg.Top = 0;
                    marg.Bottom = 46;
                    EnterPinPage.Margin = marg;
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
                
                case pages.mainMenuPage:
                    MainMenuPage.Visibility = Visibility.Visible;
                    marg = MainMenuPage.Margin;
                    marg.Left = 0;
                    marg.Right = 0;
                    marg.Top = 0;
                    marg.Bottom = 46;
                    MainMenuPage.Margin = marg;
                    break;
                case pages.depositPage:
                    DepositPage.Visibility = Visibility.Visible;
                    marg = DepositPage.Margin;
                    marg.Left = 0;
                    marg.Right = 0;
                    marg.Top = 0;
                    marg.Bottom = 46;
                    DepositPage.Margin = marg;
                    break;

                default:
                    break;
            }
            currentPage = page;
        }


        private void updateBalances()
        {
            balance_chk.Text = "$" + currentAccount.balances[0];
            balance_sav.Text = "$" + currentAccount.balances[1];
            balance_tfs.Text = "$" + currentAccount.balances[2];
            balance_chk1.Text = "$" + currentAccount.balances[0];
            balance_sav1.Text = "$" + currentAccount.balances[1];
            balance_tfs1.Text = "$" + currentAccount.balances[2];
        }

        public MainWindow()
        {
            InitializeComponent();

            accounts.Add(new Account("1234123412341234", 1234, accsBals));

            accounts.Add(new Account("1111222233334444", 1111, accsBals));

            accounts.Add(new Account("1111111166666666", 1616, accsBals));

            //Make all pages except start page hidden
            //StartPage.Visibility = Visibility.Hidden;
            MainMenuPage.Visibility = Visibility.Hidden;
            RemoveCardPage.Visibility = Visibility.Hidden;
            EnterPinPage.Visibility = Visibility.Hidden;
            DepositPage.Visibility = Visibility.Hidden;
            GoToPage(pages.startPage);
        }
        
        //Start Page Event Handeler Functions

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            AccNumError.Visibility = Visibility.Hidden;
            //Check if account # is valid
            string acctNum = PinEntry0.Text+PinEntry1.Text+PinEntry2.Text+PinEntry3.Text;
            if(acctNum.Length!=16){
                AccNumError.Text = "Your account number must be 16 digits long!";
                AccNumError.Visibility = Visibility.Visible;
                return;
            }
            if (determineValidAcctNum(acctNum))
            {
                loginViaAcctNum = true;
                currentBox = 0;

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

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            AccNumError.Visibility = Visibility.Hidden;
            PinEntry0.Text = "----";
            PinEntry1.Text = "----";
            PinEntry2.Text = "----";
            PinEntry3.Text = "----";
            currentBox = 0;
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
            if (currentPin.Length < 4)
            {
                PinErrorInfo.Visibility = Visibility.Hidden;
                PinEntry.AppendText("• ");
                currentPin+=num;
            }
            else
            {
                PinEntryErrorMessage.Text = "Pin can only be 4 digits long";
                PinErrorInfo.Visibility = Visibility.Visible;
            }
            
        }

        private void EnterPinOK(object sender, RoutedEventArgs e)
        {
            if (currentPin.Length!= 4)
            {                
                PinEntryErrorMessage.Text = "Pin must be 4 digits long.";
                PinErrorInfo.Visibility = Visibility.Visible;
                return;
            }
            //Check if pin # is valid
            if (currentPin==currentAccount.pin.ToString())
            {
                updateBalances();
                EnterPinPage.Visibility = Visibility.Hidden;
                if (!loginViaAcctNum)
                {
                    GoToPage(pages.removeCardPage);
                }
                else
                {
                    GoToPage(pages.mainMenuPage);
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
                PinEntry.Text = PinEntry.Text.Substring(0, PinEntry.Text.Length - 2);
                currentPin = currentPin.Substring(0, currentPin.Length - 1);
            }
            else {
                PinEntry.Text = "";
                currentPin = "";
            }
        }

        private void PinEntryCancel(object sender, RoutedEventArgs e)
        {
            Clear_Click(sender, e);
            PinEntry.Text = "";
            currentPin = "";
            currentAccount = null;
            GoToPage(pages.startPage);
        }

        //Main Menu Page Methods And Action Listeners

        private void DepositButton(object sender, RoutedEventArgs e)
        {
            GoToPage(pages.depositPage);
        }

        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            Clear_Click(sender, e);
            currentPin = "";
            PinEntry.Text = "";
            currentAccount = null;
            GoToPage(pages.startPage);
        }

        //Deposit Page Methods And Action Listeners

    }
}
