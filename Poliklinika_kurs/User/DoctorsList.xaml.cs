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

namespace Poliklinika_kurs.User
{
    /// <summary>
    /// Логика взаимодействия для DoctorsList.xaml
    /// </summary>
    public partial class DoctorsList : Window
    {
        public DoctorsList(int id)
        {
            InitializeComponent();
            Update();
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
                foreach(var b in spec)
                {
                    specializationBox.Items.Add(b.Специальность);
                }
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
                                        ФИО = a.name,
                                        Специальность = a.specialization,
                                        Стаж = a.expirience
                                    };
                        doctorList.ItemsSource = query.ToList();
                        foreach(var i in query)
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
            if(fioBox.SelectedIndex != -1)
            {
                    using (Modeldb db = new Modeldb())
                    {
                        doctorList.ItemsSource = null;
                        string fio = fioBox.SelectedValue.ToString();
                        var query = from a in db.Doctors
                                    where fio.Equals(a.name)
                                    select new
                                    {
                                        ФИО = a.name,
                                        Специальность = a.specialization,
                                        Стаж = a.expirience
                                    };
                        doctorList.ItemsSource = query.ToList();
                    }
            }
        }
    }
}
