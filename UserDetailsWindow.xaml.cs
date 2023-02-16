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
using Travel_Pal.Enums;
using Travel_Pal.Interfaces;
using Travel_Pal.Managers;

namespace Travel_Pal
{
    /// <summary>
    /// Interaction logic for UserDetailsWindow.xaml
    /// </summary>
    public partial class UserDetailsWindow : Window
    {

        UserManager userManager;
        TravelManager travelManager;
        IUser currentUser;
        string username, password;
        Countries location;

      

        public UserDetailsWindow(UserManager userManager, TravelManager travelManager)

        {
            InitializeComponent();

            this.userManager = userManager; 
            this.location = location;
            this.travelManager = travelManager;

            txbShowusername.Text = userManager.SignedInUser.Username;
}
            

        private void btnChangeDetails_Click(object sender, RoutedEventArgs e)
        {
            string confirmPassword = txbConfirmpassword.Text;
            bool confirm = false;
            if (confirm == false)
            {

                if (txbUsername.Text.Length < 3)
                {
                    MessageBox.Show("Username must be longer than 3 characters");
                }
                if (txbPassword.Text.Length < 5)
                {
                    MessageBox.Show("Password have to be more than 5 characters");
             
                }

                if (txbPassword.Text != txbConfirmpassword.Text)
                {
                    MessageBox.Show("Password does not match");
                }

                if (txbUsername.Text.Length >= 3 && txbPassword.Text.Length >= 5 && txbPassword.Text == txbConfirmpassword.Text)
                {
                    confirm = true; 
                }

                if(confirm == true)
                {
                    MessageBox.Show($"Updated: Username: {username}  password: {password}");
                  
                    userManager.SignedInUser.Username = txbUsername.Text;
                    userManager.SignedInUser.Password = txbPassword.Text;
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //TravelWindow travelsWindow = new(userManager,location,travelManager,currentUser);
            TravelWindow travelsWindow = new(userManager, travelManager, currentUser);
            travelsWindow.Show();
            Close();
        }


        
    }
}
