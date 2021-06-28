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
using Microsoft.Win32;
using Poliklinika_kurs.Models;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using ClosedXML.Excel;

namespace Poliklinika_kurs.Admin
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow(int id)
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DoctorAdd doctorAdd = new DoctorAdd();
            doctorAdd.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AllDoctors doctors = new AllDoctors();
            doctors.Show();
        }
    }
}
