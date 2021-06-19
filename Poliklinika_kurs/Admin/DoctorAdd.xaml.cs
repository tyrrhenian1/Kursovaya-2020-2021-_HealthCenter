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

namespace Poliklinika_kurs.Admin
{
    /// <summary>
    /// Логика взаимодействия для DoctorAdd.xaml
    /// </summary>
    public partial class DoctorAdd : Window
    {
        private int UserId { get; set; }
        public DoctorAdd()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (Modeldb db = new Modeldb())
            {
                Users user = new Users()
                {
                    login = loginBox.Text,
                    password = passBox.Text,
                    type = 2
                };
                db.Users.Add(user);
                db.SaveChanges();
                var query = from a in db.Users
                            where loginBox.Text.Equals(a.login)
                            select a;
                foreach (var i in query)
                {
                    UserId = i.id;
                }
                Doctors doctor = new Doctors()
                {
                    id = UserId,
                    name = fioBox.Text,
                    specialization = specializationBox.Text,
                    expirience = expirienceBox.Text
                };
                db.Doctors.Add(doctor);
                db.SaveChanges();
                Close();
            }
        }
    }
}
