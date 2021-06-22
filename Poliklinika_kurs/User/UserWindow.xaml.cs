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


namespace Poliklinika_kurs.User
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private int _userId;
        public UserWindow(int id)
        {
            InitializeComponent();
            _userId = id;
        }

        private void showCard_Click(object sender, RoutedEventArgs e)
        {
            Medcard medcard = new Medcard(_userId);
            medcard.Show();
        }

        private void showDoctors_Click(object sender, RoutedEventArgs e)
        {
            DoctorsList doctorsList = new DoctorsList(_userId);
            doctorsList.Show();
        }

        private void showReg_Click(object sender, RoutedEventArgs e)
        {
            RegistrationDocLog log = new RegistrationDocLog(_userId);
            log.Show();
        }
    }
}
