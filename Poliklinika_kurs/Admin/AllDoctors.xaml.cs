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
using Poliklinika_kurs.User;

namespace Poliklinika_kurs.Admin
{
    /// <summary>
    /// Логика взаимодействия для AllDoctors.xaml
    /// </summary>
    public partial class AllDoctors : Window
    {
        public AllDoctors()
        {
            InitializeComponent();
            Update();
        }
        private void Update()
        {
            using (Modeldb db = new Modeldb())
            {
                specializationBox.Items.Clear();
                fioBox.Items.Clear();
                doctorList.ItemsSource = null;
                var query = from a in db.Doctors
                            select new
                            {
                                id = a.id,
                                ФИО = a.name,
                                Специальность = a.specialization,
                                Стаж = a.expirience
                            };
                doctorList.ItemsSource = query.ToList();
                foreach (var i in query)
                {
                    fioBox.Items.Add(i.ФИО);
                }
                var spec = query.GroupBy(x => x.Специальность).Select(x => x.FirstOrDefault());
                foreach (var b in spec)
                {
                    specializationBox.Items.Add(b.Специальность);
                }
            }
        }

        private void specializationBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (specializationBox.SelectedIndex != -1)
            {
                using (Modeldb db = new Modeldb())
                {
                    if (filterFio.IsChecked == true)
                    {
                        fioBox.Items.Clear();
                        doctorList.ItemsSource = null;
                        string spec = specializationBox.SelectedValue.ToString();
                        var query = from a in db.Doctors
                                    where spec.Equals(a.specialization)
                                    select new
                                    {
                                        Id = a.id,
                                        ФИО = a.name,
                                        Специальность = a.specialization,
                                        Стаж = a.expirience
                                    };
                        doctorList.ItemsSource = query.ToList();
                        foreach (var i in query)
                        {
                            fioBox.Items.Add(i.ФИО);
                        }
                    }
                    else
                    {
                        doctorList.ItemsSource = null;
                        string spec = specializationBox.SelectedValue.ToString();
                        var query = from a in db.Doctors
                                    where spec.Equals(a.specialization)
                                    select new
                                    {
                                        Id = a.id,
                                        ФИО = a.name,
                                        Специальность = a.specialization,
                                        Стаж = a.expirience
                                    };
                        doctorList.ItemsSource = query.ToList();
                    }
                }
            }
        }

        private void fioBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fioBox.SelectedIndex != -1)
            {
                using (Modeldb db = new Modeldb())
                {
                    doctorList.ItemsSource = null;
                    string fio = fioBox.SelectedValue.ToString();
                    var query = from a in db.Doctors
                                where fio.Equals(a.name)
                                select new
                                {
                                    Id = a.id,
                                    ФИО = a.name,
                                    Специальность = a.specialization,
                                    Стаж = a.expirience
                                };
                    doctorList.ItemsSource = query.ToList();
                }
            }
        }
        private string GetDoctorName(string a)
        {
            string result = "";
            int index = a.IndexOf(',');
            string f = a.Substring(index + 7, a.Length - index - 7);
            index = f.IndexOf(',');
            for (int i = 0; i < index; i++)
            {
                result += f[i];
            }
            return result.Trim();
        }

        private string GetDate(string a)
        {
            string result = "";
            string[] date = a.Split('.');
            date[2] = date[2].Substring(0, 4);
            result = $"{date[2]}-{date[1]}-{date[0]}";
            return result.Trim();
        }

        private void filterSpec_Click(object sender, RoutedEventArgs e)
        {
            if (filterSpec.IsChecked == true)
            {
                specializationBox.Visibility = Visibility.Visible;
            }
            else
            {
                specializationBox.Visibility = Visibility.Hidden;
            }
        }

        private void filterFio_Click(object sender, RoutedEventArgs e)
        {
            if (filterFio.IsChecked == true)
            {
                fioBox.Visibility = Visibility.Visible;

            }
            else
            {
                fioBox.Visibility = Visibility.Hidden;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            filterFio.IsChecked = filterSpec.IsChecked = false;
            fioBox.Visibility = specializationBox.Visibility = Visibility.Hidden;
            specializationBox.SelectedIndex = -1;
            fioBox.SelectedIndex = -1;
            Update();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (doctorList.SelectedIndex != -1)
            {
                int id = GetRegId(doctorList.SelectedItem.ToString());
                using (Modeldb db = new Modeldb())
                {
                    db.Database.ExecuteSqlCommand($"DELETE FROM RegistrationDoc WHERE doctor_id = {id}");
                    db.Database.ExecuteSqlCommand($"DELETE FROM Doctors WHERE id = {id}");
                    db.Database.ExecuteSqlCommand($"DELETE FROM Users WHERE id = {id}");
                    db.SaveChanges();
                }
                Update();
            }
        }
        private int GetRegId(string a)
        {
            int index = a.IndexOf(',');
            int result = int.Parse(a.Substring(7, index - 7));
            return result;
        }
    }
}
