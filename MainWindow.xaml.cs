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
using Travel_Pal.Classes;
using Travel_Pal.Enums;
using Travel_Pal.Interfaces;
using Travel_Pal.Managers;

namespace Travel_Pal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //  // field variables
        UserManager userManager = new UserManager();
        TravelManager travelManager = new TravelManager();  
        List<IUser> users;
        Countries countries;

       
        public MainWindow()
        {
            
            this.userManager = new UserManager();
            InitializeComponent();
            // connects user list to usermanager method to get all list thats added in usermanager
            users = userManager.GetAllUsers();
            // foreach user and admin that exists in usermanager
            foreach (IUser user in this.userManager.GetAllUsers())
            {
                // if the user in the list is a User or Admin
                // in this case a User
                if (user is User)
                {
                    // Calls User class and variable to get the properties of User class
                    User u = user as User;

                    // foreach Travel that exists in Users property Travel
                    foreach (Travel userTravel in u.travels)
                    {
                        // if in the class User prop Travel is a Trip or Vacation
                        if (userTravel is Trip)
                        {
                            //Calls the Travel and what kind of travel
                            Trip t = userTravel as Trip;
                            // add to travel list
                            this.travelManager.AddTravel(t);
                        }
                        else if (userTravel is Vacation)
                        {
                            //Calls the Travel and what kind of travel
                            Vacation v = userTravel as Vacation;
                            // add to travel list
                            this.travelManager.AddTravel(v);
                        }
                    }
                }
            }


        }

         //constructor
        public MainWindow(UserManager userManager, TravelManager travelManager)
        {
            InitializeComponent();
            this.userManager = userManager; 
            this.travelManager = travelManager;
            users=userManager.GetUsers();

        }               // Close down Window
        private void btnCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

      
        // Register User
        private void btn_Register_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow(userManager, travelManager);
            registerWindow.Show();
            Close();
        }

        //Signing in user
        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            string username =txbUsername.Text;
            string password = tbxPassword.Text;
           
           
        
            //  Checking if the username already exist

            bool isUserExisting = false;
            users = userManager.GetAllUsers();
            foreach (IUser user in users)
            {
                if(user.Username==username && user.Password == password)
                {
                    isUserExisting = true;
                    userManager.SignedInUser = user;
                    
                    TravelWindow travelWindow = new TravelWindow(userManager,travelManager,user);
                    travelWindow.Show();
                    Close();
                }
            }
            if (!isUserExisting) 
            {
                MessageBox.Show("Username or Password is incorrect");
            }
        }
    }
}
