using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
using Travel_Pal.Enum;
using Travel_Pal.Enums;
using Travel_Pal.Interfaces;
using Travel_Pal.Managers;

namespace Travel_Pal
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {

        UserManager _userManager;   

        TravelManager _travelManager;
      
        public RegisterWindow(UserManager userManager, TravelManager travelManager)
        {
            InitializeComponent();
            this._userManager = userManager;
            this._travelManager = travelManager;
            btn_Register.IsEnabled = false;


            var Countries = System.Enum.GetValues(typeof(EU_OR_NOT_EU));
            foreach(var EUORNOTEU in Countries)
            {
                cbxCountries.Items.Add(EUORNOTEU);   
            }
        }

        private void btn_Register_Click(object sender, RoutedEventArgs e)
        {

            string UserName = txbUsername.Text;
            string Password = txbPassword.Text;
            // string selectedCountryString = cbxCountries.SelectedItem as string;

            Countries selectedCountry = (Countries)cbxCountries.SelectedItem;

            // Countries selectedCountry = (Countries)System.Enum.Parse(typeof(Countries), selectedCountryString);

            if (UserName.Length < 3)
            {
                MessageBox.Show("The Username must contain more than 3 letters! ");
                txbUsername.Clear();
            }
               
            else if (Password.Length < 5)
            {
            MessageBox.Show("The Password must contain atleast 5 letters!");
            txbPassword.Clear();
            }
                
            else
            {

           if (_userManager.AddUser(UserName, Password, selectedCountry))
                {
            MainWindow mainWindow = new MainWindow(_userManager,_travelManager);
            mainWindow.Show();
            Close();

                }
                else
                {
                    MessageBox.Show("The username is already taken");
                }
            }
        }
       
        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(_userManager,_travelManager);
            mainWindow.Show();  
            Close();

        }

        private void cbxCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
                if(cbxCountries.SelectedIndex == (int)EU_OR_NOT_EU.EU)

                {

                cbxAllCountries.Items.Clear();
                var EUCountries = System.Enum.GetValues(typeof(EuCountries));

                    foreach(var EUCountry in EUCountries)
                {
                    
                    cbxAllCountries.Items.Add(EUCountry);
                }
                }

                else if((cbxCountries.SelectedIndex == (int)EU_OR_NOT_EU.OTHER))
            {
                cbxAllCountries.Items.Clear();
                var AllCountries = System.Enum.GetValues(typeof(Countries));

                foreach (var Countries in AllCountries)
                {
                    
                    cbxAllCountries.Items.Add(Countries);
                }
            }
                
                
           

        }

        private void cbxAllCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btn_Register.IsEnabled = true;
        }
    }
}
