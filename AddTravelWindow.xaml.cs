using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Linq.Expressions;
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
using Travel_Pal.Enum;
using Travel_Pal.Enums;
using Travel_Pal.Interfaces;
using Travel_Pal.Managers;

namespace Travel_Pal
{
    /// <summary>
    /// Interaction logic for AddTravelWindow.xaml
    /// </summary>
    public partial class AddTravelWindow : Window
    {

        TravelManager TravelManager;
        UserManager UserManager;
        IUser currentUser;


        Countries location;

        List<int> travelchoices = new() { 1, 2, 3, 4, 5, 6 };

        public AddTravelWindow(UserManager userManager, IUser user, TravelManager travelManager)
        {
            InitializeComponent();
            this.UserManager = userManager;
            this.TravelManager = travelManager; 
          
            this.currentUser = user;
            userManager.GetUsers();
            var country = System.Enum.GetValues(typeof(Countries));
            foreach (var countries in country)
            {
                cbxCountrySelection.Items.Add(countries);
            }

            var travellers = System.Enum.GetValues(typeof(Enum.TravelersEnum));

            cbxTravellers.ItemsSource = travelchoices;
            var TravelType = System.Enum.GetValues(typeof(Enum.TravelType));

            foreach (var TypeTravel in TravelType)
            {
                cbxTravelType.Items.Add(TypeTravel);
            }

            var TripTypes = System.Enum.GetValues(typeof(Enum.TripTypes));

            foreach (var TripTypeTravel in TripTypes)

            {
                cbxTripType.Items.Add(TripTypeTravel);
            }
 }
        
       // Saving users travel choices

        private void btnSaveTravel_Click(object sender, RoutedEventArgs e)
        {

            string Destination = txbDestination.Text;
            Countries country = (Countries)cbxCountrySelection.SelectedIndex;
            var travellers = Convert.ToInt32(cbxTravellers.SelectedValue);
            var travelType = cbxTravelType.SelectedIndex;
            var tripType = cbxTripType.SelectedIndex;

            if (cbxTravelType.SelectedIndex == 0)
            {
                TripTypes tripTypes = (TripTypes)cbxTripType.SelectedItem;

                Trip Trip = new(tripTypes,Destination,travellers,country); 

                TravelManager.AddTravel(Trip);

                User theuser = UserManager.SignedInUser as User;

                theuser.travels.Add(Trip);
            }

            // Adding triptype/ if user selects Vacation Allinclusive check box appears

        if (cbxTravelType.SelectedIndex == 1)

                {
                bool allinclusive = (bool)CbAllInClusive.IsChecked;
                Vacation vacation = new(allinclusive, Destination, country, travellers);

                TravelManager.AddTravel(vacation);
                User theuser = UserManager.SignedInUser as User;

                theuser.travels.Add(vacation);
            }

            TravelWindow travelsWindow = new(UserManager, TravelManager, currentUser);
            travelsWindow.Show();
            Close();

        }
        // else hidden
        private void cbxTravelType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbxTravelType.SelectedIndex == (int)TravelType.Trip)
            {
                cbxTripType.Visibility = Visibility.Visible;

                CbAllInClusive.Visibility = Visibility.Hidden;

            }
            else if (cbxTravelType.SelectedIndex == (int)TravelType.Vacation)
            {
                CbAllInClusive.Visibility = Visibility.Visible;

                cbxTripType.Visibility = Visibility.Hidden;
                cbxTravelType.Visibility = Visibility.Hidden;
            }
        }

        // If user wants to exit
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            TravelWindow travelsWindow = new(UserManager, TravelManager, currentUser);
            travelsWindow.Show();
            Close();
        }

        private void cbxTravelType_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            string travelType = cbxTravelType.SelectedItem.ToString();

            if (travelType.Equals("Trip"))
            {
                cbxTripType.Visibility = Visibility.Visible;
            }
            else if (travelType.Equals("Vacation"))
            {
                CbAllInClusive.Visibility = Visibility.Visible;
            }
        }
    }
}
