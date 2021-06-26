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
    /// Логика взаимодействия для VerdictWindow.xaml
    /// </summary>
    public partial class VerdictWindow : Window
    {
        private int _stringId;
        private DateTime? _date;
        private string _pacient;
        private string _doctor;
        private int? _doctorId;
        private int? _pacientId;
        public VerdictWindow(int id)
        {
            _stringId = id;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (Modeldb db = new Modeldb())
            {
                if (verdictBox.SelectedItem != null)
                {
                    var query = from a in db.RegistrationDoc
                                where _stringId == a.id
                                select a;
                    foreach(var i in query)
                    {
                        _date = i.date;
                        _pacient = i.pacient;
                        _doctor = i.doctor;
                        _pacientId = i.pacient_id;
                        _doctorId = i.doctor_id;
                    }
                    db.Database.ExecuteSqlCommand($"DELETE FROM RegistrationDoc WHERE id = {_stringId}");
                    HistoryDes history = new HistoryDes()
                    {
                        date = _date,
                        pacient = _pacient,
                        pacient_id = _pacientId,
                        doctor = _doctor,
                        doctor_id = _doctorId,
                        verdict = verdictBox.Text
                    };
                    db.HistoryDes.Add(history);
                    db.SaveChanges();
                    MessageBox.Show("Диагноз успешно выставлен");
                    Close();
                }
                else
                {
                    MessageBox.Show("Диагноз не был выбран");
                }
            }

        }
    }
}
