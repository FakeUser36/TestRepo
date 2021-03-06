﻿using System;
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
        public double[] balances = { 0, 0, 0 };
        public Account(string num, int pass, double[] acctBal)
        {
            accountNumber = num;
            pin = pass;
            balances[0] = acctBal[0];
            balances[1] = acctBal[1];
            balances[2] = acctBal[2];
        }
    }

    /// <summary>
    /// Keeps track of bill counts on the withdraw page.
    /// </summary>
    public class Withdraw
    {
        private int[] billCounts = new int[5];
        private int[] values = { 5, 10, 20, 50, 100 };
        public Withdraw()
        {
            reset();
        }
        public void reset()
        {
            Array.Clear(this.billCounts, 0, this.billCounts.Length);
        }
        public int change(int value, int count)
        {
            int index = Array.IndexOf(values, value);
            // error if index not found.
            if (this.billCounts[index] + count < 0)
            {
                this.billCounts[index] = 0;
            }
            else
            {
                this.billCounts[index] += count;
            }
            return this.billCounts[index];
        }
        public int getTotal()
        {
            int total = 0;
            for (int i = 0; i < 5; ++i)
                total += this.billCounts[i] * this.values[i];
            return total;
        }
        public int[] getValues()
        {
            return values;
        }
    }

    public partial class MainWindow : Window
    {
        //Variables used by more than one page
        enum pages { startPage = 0, enterPinPage, removeCardPage, mainMenuPage, depositPage, actionSuccessfulPage, withdrawPage, transferPage };
        string[] pageNames = { "StartPage", "EnterPinPage", "RemoveCardPage", "MainMenuPage", "DepositPage", "ActionSuccessfulPage", "WithdrawPage", "TransferPage" };

        bool loginViaAcctNum = false;
        Account currentAccount = null;
        pages currentPage;
        int currentSelectedAccount = -1;
        int transferAccount = -1;

        //Test accounts
        List<Account> accounts = new List<Account>();

        //Withdraw data
        Withdraw withdrawData = new Withdraw();

        //Deposit data
        double depositVal = 0;

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
            //bad coding
            resetDeposit();
            resetTransfer();
            TransactionForWithdraw.Visibility = Visibility.Hidden;
            withdrawPageReset();

            Thickness margHide = new Thickness();
            margHide.Left = -9999;
            margHide.Right = 9999;
            margHide.Top = 0;
            margHide.Bottom = 46;

            Thickness margShow = new Thickness();
            margShow.Left = 0;
            margShow.Right = 0;
            margShow.Top = 0;
            margShow.Bottom = 46;

            //Hide all pages
            for (int i = 0; i < pageNames.Length; ++i)
            {
                Grid hideThis = (Grid)this.FindName(pageNames[i]);
                hideThis.Visibility = Visibility.Hidden;
                hideThis.Margin = margHide;
            }

            //Show the page requested
            Grid nextPage = (Grid)this.FindName(pageNames[(int)page]);
            nextPage.Visibility = Visibility.Visible;
            nextPage.Margin = margShow;

            currentPage = page;
        }

        private void updateBalances()
        {
            balance_chk.Text = currentAccount.balances[0].ToString("c");
            balance_sav.Text = currentAccount.balances[1].ToString("c");
            balance_tfs.Text = currentAccount.balances[2].ToString("c");
            balance_chk1.Text = currentAccount.balances[0].ToString("c");
            balance_sav1.Text = currentAccount.balances[1].ToString("c");
            balance_tfs1.Text = currentAccount.balances[2].ToString("c");
            balance_chk2.Text = currentAccount.balances[0].ToString("c");
            balance_sav2.Text = currentAccount.balances[1].ToString("c");
            balance_tfs2.Text = currentAccount.balances[2].ToString("c");
            balance_chk3.Text = currentAccount.balances[0].ToString("c");
            balance_sav3.Text = currentAccount.balances[1].ToString("c");
            balance_tfs3.Text = currentAccount.balances[2].ToString("c");
            balance_chk4.Text = currentAccount.balances[0].ToString("c");
            balance_sav4.Text = currentAccount.balances[1].ToString("c");
            balance_tfs4.Text = currentAccount.balances[2].ToString("c");
            balance_chk5.Text = currentAccount.balances[0].ToString("c");
            balance_sav5.Text = currentAccount.balances[1].ToString("c");
            balance_tfs5.Text = currentAccount.balances[2].ToString("c");
        }

        public MainWindow()
        {
            InitializeComponent();
            GoToPage(pages.startPage);

            accounts.Add(new Account("1234123412341234", 1234, accsBals));
            accounts.Add(new Account("1111222233334444", 1111, accsBals));
            accounts.Add(new Account("1111111166666666", 1616, accsBals));
            accounts.Add(new Account("1111111111111111", 1111, accsBals));

            //Make all pages except start page hidden
            //StartPage.Visibility = Visibility.Hidden;
            MainMenuPage.Visibility = Visibility.Hidden;
            RemoveCardPage.Visibility = Visibility.Hidden;
            EnterPinPage.Visibility = Visibility.Hidden;
            DepositPage.Visibility = Visibility.Hidden;
            ActionSuccessfulPage.Visibility = Visibility.Hidden;
            WithdrawPage.Visibility = Visibility.Hidden;
            DepositPopup.Visibility = Visibility.Hidden;
            TransactionForWithdraw.Visibility = Visibility.Hidden;
            TransferPage.Visibility = Visibility.Hidden;
            TransferToSelectError.Visibility = Visibility.Hidden;
            TransferFromSelectError.Visibility = Visibility.Hidden;
            TransferEntryError.Visibility = Visibility.Hidden;
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
            if (currentPage == pages.startPage)
            {
                currentAccount = accounts[0]; //Card input will use first account
                loginViaAcctNum = false;
                GoToPage(pages.enterPinPage);
            }
            else if (currentPage == pages.removeCardPage)
            {
                GoToPage(pages.mainMenuPage);
            }
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

        private void WithdrawPageButton(object sender, RoutedEventArgs e)
        {
            GoToPage(pages.withdrawPage);
        }


        private void TransferPageButton(object sender, RoutedEventArgs e)
        {
            GoToPage(pages.transferPage);
        }

        private void btn_logout_Click(object sender, RoutedEventArgs e)
        {
            Clear_Click(sender, e);
            currentPin = "";
            PinEntry.Text = "";
            currentAccount = null;
            GoToPage(pages.startPage);
        }

        /* Event handler for each + and - button on the withdraw page.
           Updates the text fields beside the buttons.
           Updates the total sum text field.
        */
        private void withdrawBillCountClick(object sender, RoutedEventArgs e)
        {
            string buttonTag = (string) (sender as Button).Tag;

            //Parse Tag for bill values (5, 10, 20, 50, 100)
            int billValue = Int32.Parse(buttonTag.Substring(1, buttonTag.Length-1));

            //Tag starts with either - or +
            string diffTag = buttonTag[0].ToString();
            int diff = (diffTag == "-") ? -1 : 1;

            //Remove error frame if error solved
            if (diffTag == "+")
                errorWithdraw0Error.Visibility = Visibility.Hidden;
            errorWithdraw9Error.Visibility = Visibility.Hidden;

            TextBlock billCountTextBlock = (TextBlock)this.FindName("numof"+billValue.ToString());

            //Set count of bills
            if (billCountTextBlock != null)
                billCountTextBlock.Text = withdrawData.change(billValue, diff).ToString();

            //Set total
            int total = withdrawData.getTotal();
            withdraw_total.Text = "$"+total.ToString();
        }

        //Clear the WithdrawPage for the next withdraw
        private void withdrawPageReset()
        {
            //unset data
            withdrawData.reset();

            //unset texts
            for (int i = 0; i < 5; ++i)
            {
                TextBlock numof = (TextBlock)FindName("numof" + withdrawData.getValues()[i].ToString());
                numof.Text = "0";
            }
            withdraw_total.Text = "$0";

            //unset radios
            checking.IsChecked = false;
            saving.IsChecked = false;
            tfsa.IsChecked = false;
            currentSelectedAccount = -1;

            //hide errors
            errorWithdraw0Error.Visibility = Visibility.Hidden;
            errorWithdraw9Error.Visibility = Visibility.Hidden;
        }

        private void WithdrawConfirmButton(object sender, RoutedEventArgs e)
        {
            if (currentSelectedAccount == -1)
            {
                WithdrawalSelectError.Visibility = Visibility.Visible;
                return;
            }

            int total = withdrawData.getTotal();
            if (total > 0)
            {
                if (total < currentAccount.balances[currentSelectedAccount])
                    performWithdrawal(total);
                else
                    errorWithdraw9Error.Visibility = Visibility.Visible;
            } else
            {
                errorWithdraw0Error.Visibility = Visibility.Visible;
            }
        }

        private void performWithdrawal(double total)
        {
            currentAccount.balances[currentSelectedAccount] -= total;
            updateBalances();
            WithdrawalSelectError.Visibility = Visibility.Hidden;
            //WithdrawalError.Visibility = Visibility.Hidden;
            ActionSuccessfulUpdate("Successfully withdrawn " + total.ToString("c") + ".");
            withdrawPageReset();
            GoToPage(pages.actionSuccessfulPage);
            TransactionForWithdraw.Visibility = Visibility.Visible;
        }

        //Successful transaction page and account listeners
        private void GoToMainMenu(object sender, RoutedEventArgs e)
        {
            GoToPage(pages.mainMenuPage);
        }

        //Update the action successful page with a message before gong to it.
        private void ActionSuccessfulUpdate(string message)
        {
            TextBlock previousTransactionTextBlock = (TextBlock)this.FindName("transaction_complete_message");
            previousTransactionTextBlock.Text = message;
        }

        /* 
         * Deposit Page Methods And Action Listeners
        */

        //This method is called whenever the user presses a key on the deposit page
        private void enterNumToDeposit(object sender, RoutedEventArgs e)
        {
            DepositError.Visibility = Visibility.Hidden;
            string num = (((Button)sender).Content.ToString());
            if (DepositEntry.Text.Length < 16)
            {
                if (num == "." & DepositEntry.Text.Contains("."))
                {
                    DepositErrorText.Text = "You may not have more than one decimal point.";
                    DepositError.Visibility=Visibility.Visible;
                    return;
                }

                string [] nums = DepositEntry.Text.Split('.');

                if(nums.Length>1 && nums[1].Length>1){
                    DepositErrorText.Text = "You may not deposit fractions of cents.";
                    DepositError.Visibility = Visibility.Visible;
                    return;
                }

                DepositEntry.Text += num;
            }
            else {
                DepositErrorText.Text = "Please contact an employee to deposit that much at once.";
                DepositError.Visibility = Visibility.Visible;
            }
        }

        //This method is invoked when the user 
        private void DepositConfirmButton(object sender, RoutedEventArgs e)
        {
            if (currentSelectedAccount == -1) {
                DepositSelectError.Visibility = Visibility.Visible;
                return;
            }

            if (DepositEntry.Text.Length < 1) {
                DepositErrorText.Text = "Please enter an amount to deposit.";
                DepositError.Visibility = Visibility.Visible;
                return;
            }

            if (Double.TryParse(DepositEntry.Text, out depositVal))
            {
                DepositPopup.Visibility = Visibility.Visible;
            }
            else
            {
                DepositErrorText.Text = "You may not have more than one decimal point.";
                DepositError.Visibility = Visibility.Visible;
            }
        }

        private void resetDeposit()
        {
            DepositEntry.Text = "";
            DepositSelectError.Visibility = Visibility.Hidden;
            DepositError.Visibility = Visibility.Hidden;
            checking1.IsChecked = false;
            saving1.IsChecked = false;
            tfsa1.IsChecked = false;
            currentSelectedAccount = -1;
            depositVal = 0;
            DepositPopup.Visibility = Visibility.Hidden;
        }

        private void performDeposit() {
            currentAccount.balances[currentSelectedAccount] += depositVal;
            updateBalances();
            ActionSuccessfulUpdate("Successfully deposited " + depositVal.ToString("c") + ".");
            resetDeposit();
            GoToPage(pages.actionSuccessfulPage);
        }

        private void DepositEnrtyBack(object sender, RoutedEventArgs e){
            DepositError.Visibility = Visibility.Hidden;
            if (DepositEntry.Text.Length > 1)
            {
               DepositEntry.Text = DepositEntry.Text.Substring(0, DepositEntry.Text.Length - 1);
            }
            else 
            {
                DepositEntry.Text = "";
            }
        }

        private void DepositClear(object sender, RoutedEventArgs e){
            DepositEntry.Text = "";
        }

        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {
            DepositSelectError.Visibility = Visibility.Hidden;
            WithdrawalSelectError.Visibility = Visibility.Hidden;
            TransferFromSelectError.Visibility = Visibility.Hidden;

            string button = ((RadioButton)sender).Content.ToString();
            switch (button){
                case "Checking":
                    currentSelectedAccount = 0;
                    break;
                case "Saving":
                    currentSelectedAccount = 1;
                    break;
                case "TFSA":
                    currentSelectedAccount = 2;
                    break;
                default:
                    break;
            }
        }

        private void MoneySlot(object sender, MouseButtonEventArgs e)
        {
            if (currentPage == pages.depositPage && DepositPopup.Visibility == Visibility.Visible)
            {
                performDeposit();
            }
            else if (currentPage == pages.actionSuccessfulPage && TransactionForWithdraw.Visibility == Visibility.Visible)
            {
                GoToPage(pages.mainMenuPage);
            }
        }

        //Transfer page methods
        private void RadioButtonChecked2(object sender, RoutedEventArgs e)
        {
            TransferToSelectError.Visibility = Visibility.Hidden;

            string button = ((RadioButton)sender).Content.ToString();
            switch (button)
            {
                case "Checking":
                    transferAccount = 0;
                    break;
                case "Saving":
                    transferAccount = 1;
                    break;
                case "TFSA":
                    transferAccount = 2;
                    break;
                default:
                    break;
            }
        }

        //This method is called whenever the user presses a key on the deposit page
        private void enterNumToTransfer(object sender, RoutedEventArgs e)
        {
            TransferEntryError.Visibility = Visibility.Hidden;
            string num = (((Button)sender).Content.ToString());
            if (TransferEntry.Text.Length < 16)
            {
                if (num == "." & TransferEntry.Text.Contains("."))
                {
                    TransferEntryErrorText.Text = "You may not have more than one decimal point.";
                    TransferEntryError.Visibility = Visibility.Visible;
                    return;
                }

                string[] nums = TransferEntry.Text.Split('.');

                if (nums.Length > 1 && nums[1].Length > 1)
                {
                    TransferEntryErrorText.Text = "You may not transfer fractions of cents.";
                    TransferEntryError.Visibility = Visibility.Visible;
                    return;
                }

                TransferEntry.Text += num;
            }
            else
            {
                TransferEntryErrorText.Text = "Please contact an employee to transfer that much at once.";
                TransferEntryError.Visibility = Visibility.Visible;
            }
        }

        private void TransferConfirmButton(object sender, RoutedEventArgs e)
        {
            TransferToSelectError.Visibility = Visibility.Hidden;
            TransferFromSelectError.Visibility = Visibility.Hidden; 
            TransferEntryError.Visibility = Visibility.Hidden;


            if (currentSelectedAccount == -1)
            {
                TransferFromSelectErrorText.Text = "Please select an account to transfer funds from.";
                TransferFromSelectError.Visibility = Visibility.Visible; 
                return;
            }
            else if (transferAccount == -1)
            {
                TransferToSelectErrorText.Text="Please select an account to transfer funds from.";
                TransferToSelectError.Visibility = Visibility.Visible;
                return;
            }

            if (TransferEntry.Text.Length < 1)
            {
                TransferEntryErrorText.Text = "Please enter an amount to deposit.";
                TransferEntryError.Visibility = Visibility.Visible;
                return;
            }

            if(currentSelectedAccount==transferAccount){
                TransferToSelectErrorText.Text="The account you want to transfer money to must be different from the account you are taking money from.";
                TransferToSelectError.Visibility=Visibility.Visible;
                TransferFromSelectErrorText.Text = "";
                TransferFromSelectError.Visibility = Visibility.Visible;

                return;
            }

            double val;

            if (Double.TryParse(TransferEntry.Text, out val))
            {
                if (val <= currentAccount.balances[currentSelectedAccount])
                {
                    performTransfer(val);
                }
                else {
                    TransferFromSelectErrorText.Text = "You do not have sufficent funds to transfer this amount.";
                    TransferFromSelectError.Visibility = Visibility.Visible;
                }
                
            }
            else
            {
                //We should hopefully never get here
                TransferEntryErrorText.Text = "Please enter an amount to deposit. BAD";
                TransferEntryError.Visibility = Visibility.Visible;
            }
        }

        private void resetTransfer()
        {
            TransferEntry.Text = "";
            TransferEntryError.Visibility = Visibility.Hidden;
            TransferFromSelectError.Visibility = Visibility.Hidden;
            TransferToSelectError.Visibility = Visibility.Hidden;
            checking2.IsChecked = false;
            saving2.IsChecked = false;
            tfsa2.IsChecked = false;
            checking3.IsChecked = false;
            saving3.IsChecked = false;
            tfsa3.IsChecked = false;
            currentSelectedAccount = -1;
            transferAccount = -1;
        }

        private void TransferClear(object sender, RoutedEventArgs e)
        {
            TransferEntry.Text = "";
        }

        private void TransferEntryBack(object sender, RoutedEventArgs e)
        {
            TransferEntryError.Visibility = Visibility.Hidden;
            if (TransferEntry.Text.Length > 1)
            {
                TransferEntry.Text = TransferEntry.Text.Substring(0, TransferEntry.Text.Length - 1);
            }
            else
            {
                TransferEntry.Text = "";
            }
        }

        private void performTransfer(double val) {
            currentAccount.balances[transferAccount] += val;
            currentAccount.balances[currentSelectedAccount] -= val;
            updateBalances();
            ActionSuccessfulUpdate("Successfully transfered " + val.ToString("c") + ".");
            GoToPage(pages.actionSuccessfulPage);
        }

    }
}

