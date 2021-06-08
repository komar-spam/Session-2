using DemEx.Edits;
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

namespace DemEx
{
    /// <summary>
    /// Логика взаимодействия для ClientView.xaml
    /// </summary>
    public partial class ClientView : Window
    {
        public ClientView()
        {
            InitializeComponent();
            Refresh();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e) //добавление
        {
            ClientEdit clientEdit = new ClientEdit();
            clientEdit.ShowDialog();
            Refresh();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //редактирование
        {
            try
            {
                var item = clientDataGrid?.SelectedItem as Client;
                if (item != null)
                {
                    ClientEdit clientEdit = new ClientEdit(item);
                    clientEdit.ShowDialog();
                    Refresh();
                }
            }
            catch
            {
                MessageBox.Show("При попытке редактирования произошла ошибка");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) //удаление
        {
            try
            {
                var item = clientDataGrid.SelectedItem as Client;
                if (item != null)
                {
                    if (MessageBox.Show("Вы хотите удалить этот элемент?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.No)
                        return;
                    var cl = App.entities.Client.FirstOrDefault(x => x.ID == item.ID);
                    if (cl != null)
                    {
                        App.entities.Client.Remove(cl);
                        App.entities.SaveChanges();
                        Refresh();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не удалось удалить элемент");
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void Refresh()
        {
            clientDataGrid.ItemsSource = App.entities.Client.Where(x => x.FirstName.ToLower().Contains(SearchTb.Text) || x.LastName.ToLower().Contains(SearchTb.Text.ToLower()) 
            || x.Patronymic.ToLower().Contains(SearchTb.Text.ToLower()) ).ToList();
        }
    }
}
