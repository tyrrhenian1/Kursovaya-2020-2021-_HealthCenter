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
        private int InUse;
        private int userId;
        private int doctorId;
        public DoctorsList(int id)
        {
            InitializeComponent();
            userId = id;
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
                                        Id = a.id,
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
                                        Id = a.id,
                                        ФИО = a.name,
                                        Специальность = a.specialization,
                                        Стаж = a.expirience
                                    };
                        doctorList.ItemsSource = query.ToList();
                    }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!(dateReg.SelectedDate < DateTime.Now))
            {
                using (Modeldb db = new Modeldb())
                {
                    string dateRegistration = dateReg.SelectedDate.ToString().Substring(0, 10);
                    var doctor = from b in db.Doctors
                                 where doctorId == b.id
                                 select b.name;
                    doctorFio.Text = doctor.FirstOrDefault().ToString();
                    
                    var checkQuery = from c in db.DiagnosticIn
                                     select c;
                    foreach (var i in checkQuery)
                    {
                        label1.Content = dateRegistration.Equals(i.date);
                        label2.Content = i.time.Equals(timeReg.Text);
                        label3.Content = doctorId == i.id;
                        label4.Text += dateRegistration + "   " + i.date + Environment.NewLine + timeReg.Text + "   " + i.time + Environment.NewLine + doctorId + "    " + i.id + Environment.NewLine;
                        if (dateRegistration.Equals(i.date) && i.time.Equals(timeReg.Text) && doctorId == i.doctor_id)
                        {
                            MessageBox.Show("Выбранные дата и время заняты");
                            InUse = 1;
                            break;
                        }
                        else
                        {
                            InUse = 0;
                        }
                    }
                }
                if (InUse == 0)
                {
                    using (Modeldb db = new Modeldb())
                    {
                        var pacient = from a in db.Pacients
                                      where userId.Equals(a.id)
                                      select a.name;
                        checkTime check = new checkTime()
                        {
                            date = dateReg.SelectedDate.ToString().Substring(0, 10),
                            time = timeReg.Text,
                        };
                        DiagnosticIn diagnostic = new DiagnosticIn
                        {
                            doctor = doctorFio.Text,
                            pacient = pacient.FirstOrDefault(),
                            doctor_id = doctorId,
                            pacient_id = userId,
                            date = dateReg.SelectedDate.ToString().Substring(0, 10),
                            time = timeReg.Text
                        };
                        db.checkTime.Add(check);
                        db.DiagnosticIn.Add(diagnostic);
                        db.SaveChanges();
                        MessageBox.Show("Вы были успешно зарегестрированны");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выбранная дата некорректна");
            }
            
        }

        private void doctorList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            doctorId = GetDoctorId(doctorList.SelectedItem.ToString());
            using (Modeldb db = new Modeldb())
            {
                var doctor = from b in db.Doctors
                             where doctorId == b.id
                             select b.name;
                doctorFio.Text = doctor.FirstOrDefault().ToString();
            }
            
        }
        private int GetDoctorId(string a)
        {
            int index = a.IndexOf(',');
            int f = int.Parse(a.Substring(startIndex:7,length: index - 7).Trim());
            return f;
        }
    }
}
