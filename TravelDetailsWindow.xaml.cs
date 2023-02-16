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
using Travel_Pal.Classes;
using Travel_Pal.Interfaces;
using Travel_Pal.Managers;

namespace Travel_Pal
{
    /// <summary>
    /// Interaction logic for TravelDetailsWindow.xaml
    /// </summary>
    public partial class TravelDetailsWindow : Window
    {

        List<Travel> travels;
        UserManager userManager;
        TravelManager travelManager;
        Travel travel;
        IUser currentUser;

        public TravelDetailsWindow(UserManager userManager, TravelManager travelManager,Travel item, IUser user)


        {
            InitializeComponent();
            this.currentUser = user;


            this.userManager = userManager;
            this.travelManager = travelManager;
            this.travel = item;




            InitializeComponent();

            tbCountry.Text = travel.Country.ToString();
            tbDestination.Text = travel.Destination;
            tbTravelers.Text = travel.Travellers.ToString();
            

            DetermTravelType();

        }

        // User determing travel type
        private void DetermTravelType()
        {
            if (travel is Trip)
            {
                Trip trip = (Trip)travel;
                tbTravelInfo.Text = $"Triptype; {trip.tripType}";

                tbTravelType.Text = "Trip";

            }
            // if  user picks vacation it will show in travelinfo else: Not Allinclusive  
            else if (travel is Vacation)
            {
                Vacation vacation = (Vacation)travel;   
                tbTravelType.Text = "Vacation";
                if (vacation.AllInClusive)
                {
                    tbTravelInfo.Text = "Allinclusive";
                }

                else
                {
                    tbTravelInfo.Text = "Not Allinclusive";
                }

                
            }
        }

        // Exit button
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {

            Close();

        }
    }
}
