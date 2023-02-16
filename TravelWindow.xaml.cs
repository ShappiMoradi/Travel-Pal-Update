using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Travel_Pal.Classes;
using Travel_Pal.Enums;
using Travel_Pal.Interfaces;
using Travel_Pal.Managers;

namespace Travel_Pal
{
    /// <summary>
    /// Interaction logic for TravelWindow.xaml
    /// </summary>
    public partial class TravelWindow : Window
    {
        //// Field variable
        UserManager userManager;
        List<IUser> users;
        IUser currentUser;
        List<Travel> travels;

        TravelManager travelManager;
        private IUser CurrentUser;

      
       
        public TravelWindow(UserManager userManager, TravelManager travelManager, IUser currentUser)
        {
            InitializeComponent();  
            this.userManager = userManager;
            this.travelManager = travelManager;
            this.CurrentUser = currentUser;

            lblShowusername.Content = userManager.SignedInUser.Username;


            User user = userManager.SignedInUser as User;
            Admin admin = userManager.SignedInUser as Admin;


            if (user is User)
            {
               
              
                travels = travelManager.GetAllTravels();

                // foreach travel class that exists in the User
                foreach (Travel travel in user.travels)
                {

                    // function to give the item we gonna put in Listview
                    ListViewItem selectitem = new ListViewItem();

                    // Give tag as its keeps track off whats been chosen in Listview


                    selectitem.Content = travel.Country;


                    selectitem.Tag = travel;


                    // Add to Listview list
                    lvListview.Items.Add(selectitem);



                 
                    lvListview.SelectedItem = btnDetails;



                }

            }
            else if (admin is Admin)
            {
                // IF its a User make all button hidden that is not needed for Adnin
                

                travels = travelManager.GetAllTravels();

                foreach (Travel travel in travels)
                {

                    
                    ListViewItem listViewItem = new();
                    listViewItem.Tag = travel;
                    listViewItem.Content = travel.Country;
                   lvListview.Items.Add(listViewItem);
                }


            }


        }

        //This button opens up the users details window
        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            UserDetailsWindow userDetailsWindow = new UserDetailsWindow(userManager,travelManager);
            userDetailsWindow.Show();
         Close();
        }

        //Button that opens up the users Travel details 
        private void btnDetails_Click(object sender, RoutedEventArgs e)
        {
            if (lvListview.SelectedItems.Count == 0)
            {
                MessageBox.Show("Nothing is Selected, is there a added travel?", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            ListViewItem selectitem = lvListview.SelectedItem as ListViewItem;   
            Travel travelItem = selectitem.Tag as Travel;
            TravelDetailsWindow travelDetailsWindow = new(userManager, travelManager, travelItem,currentUser );
            travelDetailsWindow.Show();



            
        }

        // A Window where the user can put in their travel destination
        private void btnAddTravel_Click(object sender, RoutedEventArgs e)
        {
            AddTravelWindow addTravelWindow = new(userManager,currentUser,travelManager);
            addTravelWindow.Show();
            this.Close();
        }

        //Asking the user if they want to sign out
        private void btnSignOut_Click(object sender, RoutedEventArgs e)
        {
        if (MessageBox.Show("Do you want to sign out?","Sign out",MessageBoxButton.YesNo,MessageBoxImage.Asterisk) == MessageBoxResult.No)
            {
                //If the user don't want to sign out
            }  
        // If the user want to sign out  -> shuts down MainWindow
        else
            {
                
                Close();
               

            }
        }
        //Remove button so user can remove travel/country
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lvListview.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a item to Remove", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }



            ListViewItem selectedItem = lvListview.SelectedItem as ListViewItem;
            Travel selectedTravel = selectedItem.Tag as Travel;
            travelManager.RemoveTravel(selectedTravel);


            // Remove an item from list

            users = userManager.GetAllUsers();
            foreach (IUser dauser in users)
            {
                if (dauser is User)
                {
                    User u = dauser as User;

                    if (u.travels.Contains(selectedTravel))
                    {
                        u.travels.Remove(selectedTravel);
                        lvListview.Items.RemoveAt(lvListview.SelectedIndex);
                    }
                }
            }
        }
         // Info regarding the app
        private void btnInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(" Hello Welcome to Travel Pal! \r This is a app where you can store your travel information\r This app was created  with the purpose to make it easier for you to plan your travels.");
        }
    }
}
