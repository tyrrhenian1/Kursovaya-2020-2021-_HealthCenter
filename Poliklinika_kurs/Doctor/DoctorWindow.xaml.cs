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
using Poliklinika_kurs.Models;

namespace Poliklinika_kurs.Doctor
{
    /// <summary>
    /// Логика взаимодействия для DoctorWindow.xaml
    /// </summary>
    public partial class DoctorWindow : Window
    {
        private int _userId;
        public DoctorWindow(int id)
        {
            InitializeComponent();
            _userId = id;
            using (Modeldb db = new Modeldb())
            {
                var query = from a in db.Doctors
                            where _userId == a.id
                            select a;
                    foreach(var i in query)
                    {
                        nameBox.Content = i.name;
                        specializationBox.Content = i.specialization;
                        expirienceBox.Content = i.expirience;
                    }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ListToDoc toDoc = new ListToDoc(_userId);
            toDoc.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            HistoryVisitWindow history = new HistoryVisitWindow(_userId);
            history.Show();
        }
    }
}
