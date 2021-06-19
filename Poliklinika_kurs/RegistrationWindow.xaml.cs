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

namespace Poliklinika_kurs
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private int UserId { get; set; }
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(fioBox.Text.Length == 0 ||
               birthdayBox.SelectedDate == null ||
               adressBox.Text.Length == 0 ||
               loginBox.Text.Length == 0 ||
               passBox.Password.Length == 0 ||
               againpassBox.Password.Length == 0
               )
            {
                MessageBox.Show("Вы не ввели какие либо данные");
            }
            else if (!passBox.Password.Equals(againpassBox.Password))
            {
                MessageBox.Show("Пароли не совпадают");
            }
            else
            {
                using (Modeldb db = new Modeldb())
                {
                    Users users = new Users()
                    {
                        login = loginBox.Text,
                        password = passBox.Password,
                        type = 1  
                    };
                    db.Users.Add(users);
                    db.SaveChanges();
                    var query = from a in db.Users
                                where loginBox.Text.Equals(a.login)
                                select a;
                    foreach(var i in query)
                    {
                        UserId = i.id;
                    }
                    Pacients pacients = new Pacients()
                    {
                        id = UserId,
                        name = fioBox.Text,
                        birthday = birthdayBox.DisplayDate.Date,
                        adress = adressBox.Text
                    };
                    db.Pacients.Add(pacients);
                    db.SaveChanges();
                    MainWindow main = new MainWindow();
                    main.Show();
                    Close();
                }
            }
        }
    }
}
