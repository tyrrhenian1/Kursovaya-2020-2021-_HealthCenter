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
using Poliklinika_kurs.Resources;

namespace Poliklinika_kurs.User
{
    /// <summary>
    /// Логика взаимодействия для Medcard.xaml
    /// </summary>
    public partial class Medcard : Window
    {
        private int UserId { get; set; }
        public Medcard(int id)
        {
            InitializeComponent();
            UserId = id;
            using (Modeldb db = new Modeldb())
            {
                var query = from a in db.Pacients
                            where UserId == a.id
                            select a;
                if(query.Count() != 0)
                {
                    foreach(var i in query)
                    {
                        nameBox.Content = i.name;
                        birthdayBox.Content = i.birthday;
                        addressBox.Content = i.adress;
                    }
                }
            }
        }
    }
}
