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
    public partial class HistoryVisitWindow : Window
    {
        private int _userId;
        public HistoryVisitWindow(int id)
        {
            InitializeComponent();
            _userId = id;
            Update();
        }
        private void Update()
        {
            regList.ItemsSource = null;
            using (Modeldb db = new Modeldb())
            {
                var query = from a in db.HistoryDes
                            where _userId == a.doctor_id || _userId == a.pacient_id
                            select new
                            {
                                Врач = a.doctor,
                                Пациент = a.pacient,
                                Дата = a.date.ToString(),
                                Диагноз = a.verdict
                            };
                regList.ItemsSource = query.ToList();
            }
        }

        private void isFilter_Click(object sender, RoutedEventArgs e)
        {
            if (isFilter.IsChecked == true)
                firstFilter.Visibility = secondFilter.Visibility = sortButton.Visibility = cancelButton.Visibility = Visibility.Visible;
            else
                firstFilter.Visibility = secondFilter.Visibility = sortButton.Visibility = cancelButton.Visibility = Visibility.Hidden;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void sortButton_Click(object sender, RoutedEventArgs e)
        {
            if (firstFilter.SelectedDate.HasValue == true && secondFilter.SelectedDate.HasValue == true)
            {
                DateTime? first = firstFilter.SelectedDate.Value.Date;
                DateTime? second = secondFilter.SelectedDate.Value.Date;
                using (Modeldb db = new Modeldb())
                {
                    regList.ItemsSource = null;
                    var query = from a in db.HistoryDes
                                where a.date >= first && a.date <= second && (_userId == a.doctor_id || _userId == a.pacient_id)
                                select new
                                {
                                    Врач = a.doctor,
                                    Пациент = a.pacient,
                                    Дата = a.date.ToString(),
                                    Диагноз = a.verdict
                                };
                    regList.ItemsSource = query.ToList();
                }
            }
            else if (firstFilter.SelectedDate.HasValue == true && secondFilter.SelectedDate.HasValue == false)
            {
                DateTime? first = firstFilter.SelectedDate.Value.Date;
                using (Modeldb db = new Modeldb())
                {
                    regList.ItemsSource = null;
                    var query = from a in db.HistoryDes
                                where a.date == first && (_userId == a.doctor_id || _userId == a.pacient_id)
                                select new
                                {
                                    Врач = a.doctor,
                                    Пациент = a.pacient,
                                    Дата = a.date.ToString(),
                                    Диагноз = a.verdict
                                };
                    regList.ItemsSource = query.ToList();
                }
            }
            else if (firstFilter.SelectedDate.HasValue == false && secondFilter.SelectedDate.HasValue == true)
            {
                using (Modeldb db = new Modeldb())
                {
                    DateTime? second = secondFilter.SelectedDate.Value.Date;
                    regList.ItemsSource = null;
                    var query = from a in db.HistoryDes
                                where a.date == second && (_userId == a.doctor_id || _userId == a.pacient_id)
                                select new
                                {
                                    Врач = a.doctor,
                                    Пациент = a.pacient,
                                    Дата = a.date.ToString(),
                                    Диагноз = a.verdict
                                };
                    regList.ItemsSource = query.ToList();
                }
            }
            else
            {
                MessageBox.Show("Дата сортировки не была выбрана");
            }
        }
    }
}
