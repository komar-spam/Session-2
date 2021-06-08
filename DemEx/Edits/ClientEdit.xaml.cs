using DemEx.Models;
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

namespace DemEx.Edits
{
    /// <summary>
    /// Логика взаимодействия для ClientEdit.xaml
    /// </summary>
    public partial class ClientEdit : Window
    {
        Client client_ = new Client();
        bool isEdit = false;

        public ClientEdit(Client client = null)
        {
            InitializeComponent();
            genderCodeComboBox.ItemsSource = App.entities.Gender.ToList();
            if (client != null)
            {
                isEdit = true;
                client_ = client;
                genderCodeComboBox.SelectedItem = client_.Gender;
            }
            grid1.DataContext = client_;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e) //отмена
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                client_.Gender = genderCodeComboBox.SelectedItem as Gender;
                if (!isEdit)
                {
                    App.entities.Client.Add(client_);
                }
                App.entities.SaveChanges();
                Close();
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }
    }
}
