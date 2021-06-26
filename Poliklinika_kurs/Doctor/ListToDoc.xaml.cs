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
    /// Логика взаимодействия для ListToDoc.xaml
    /// </summary>
    public partial class ListToDoc : Window
    {
        private int _userId;
        public ListToDoc(int id)
        {
            _userId = id;
            InitializeComponent();
            Update();
        }
        private void Update()
        {
            regList.ItemsSource = null;
            using (Modeldb db = new Modeldb())
            {
                var query = from a in db.RegistrationDoc
                            where _userId == a.doctor_id
                            select new
                            {
                                id = a.id,
                                Пациент = a.pacient,
                                Дата = a.date.ToString(),
                                Время = a.time
                            };
                regList.ItemsSource = query.ToList();
                var name = db.Doctors.Where(x => x.id == _userId).FirstOrDefault();
                nameLab.Content = $"История записей для {name.name}";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (regList.SelectedIndex != -1)
            {
                int regId = GetRegId(regList.SelectedItem.ToString());
                using (Modeldb db = new Modeldb())
                {
                    db.Database.ExecuteSqlCommand($"DELETE FROM RegistrationDoc WHERE id = {regId}");
                    db.SaveChanges();
                }
                Update();
            }
            else
            {
                MessageBox.Show("Запись не была выбрана");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (regList.SelectedIndex != -1)
            {
                int regId = GetRegId(regList.SelectedItem.ToString());
                VerdictWindow verdict = new VerdictWindow(regId);
                verdict.Show();
            }
            else
            {
                MessageBox.Show("Запись не была выбрана");
            }
        }
        private int GetRegId(string a)
        {
            int index = a.IndexOf(',');
            int result = int.Parse(a.Substring(7, index - 7));
            return result;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Update();
        }
    }
}
