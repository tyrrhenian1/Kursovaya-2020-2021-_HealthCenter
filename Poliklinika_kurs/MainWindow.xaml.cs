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
using Poliklinika_kurs.User;
using Poliklinika_kurs.Doctor;
using Poliklinika_kurs.Admin;
using Poliklinika_kurs.Models;

namespace Poliklinika_kurs
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            if(loginBox.Text.Length == 0 || passBox.Password.Length == 0)
            {
                MessageBox.Show("Введите логин или пароль");
            }
            else
            {
                using(Modeldb db = new Modeldb())
                {
                    var a = from b in db.Users
                            where b.login.Equals(loginBox.Text) &&
                                  b.password.Equals(passBox.Password)
                            select b;
                    if (a.Count() != 0)
                    {
                        foreach(var i in a)
                        {
                            if(i.type == 1)
                            {
                                UserWindow userWindow = new UserWindow(i.id);
                                userWindow.Show();
                                Close();
                            }
                            else if (i.type == 2)
                            {
                                DoctorWindow doctorWindow = new DoctorWindow(i.id);
                                doctorWindow.Show();
                                Close();
                            }
                            else
                            {
                                AdminWindow adminWindow = new AdminWindow(i.id);
                                adminWindow.Show();
                                Close();
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введенные данные не корректны!");
                    }
                }
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registration = new RegistrationWindow();
            registration.Show();
            Close();
        }

        private void showpass_Click(object sender, RoutedEventArgs e)
        {
            if(showpass.IsChecked == true)
            {
                showpassBox.Text = passBox.Password;
                passBox.Visibility = Visibility.Hidden;
                showpassBox.Visibility = Visibility.Visible;
            }
            else
            {
                passBox.Password = showpassBox.Text;
                showpassBox.Visibility = Visibility.Hidden;
                passBox.Visibility = Visibility.Visible;           
            }
        }
    }
}
