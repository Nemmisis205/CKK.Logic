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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using CKK.Logic;
using CKK.Logic.Interfaces;
using CKK.Logic.Models;

namespace CKK.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IStore _Store;
        public ObservableCollection<StoreItem> _Items { get; private set; }

        public MainWindow(Store store)
        {
            _Store = store;
            InitializeComponent();
            _Items = new ObservableCollection<StoreItem>();
            Product test1 = new Product(0, "Victory", 4.99m);
            Product test2 = new Product(0, "Freedom", 9.99m);
            Product test3 = new Product(0, "3 gyatdamn seconds to myself", 999999999.99m);
            Product test4 = new Product(0, "Test", 3.33m);
            Product test5 = new Product(0, "12Victory", 4.99m);
            Product test6 = new Product(0, "23Freedom", 9.99m);
            Product test7 = new Product(0, "343 gyatdamn seconds to myself", 999999999.99m);
            Product test8 = new Product(0, "56Test", 3.33m);

            _Store.AddStoreItem(test1, 1);
            _Store.AddStoreItem(test2, 19);
            _Store.AddStoreItem(test3, 1);
            _Store.AddStoreItem(test4, 2);
            _Store.AddStoreItem(test5, 1);
            _Store.AddStoreItem(test6, 19);
            _Store.AddStoreItem(test7, 1);
            _Store.AddStoreItem(test8, 2);

            invBox.ItemsSource = _Items;
            RefreshList();


        }

        private void RefreshList()
        {
            _Items.Clear();
            foreach (StoreItem i in new ObservableCollection<StoreItem>(_Store.GetStoreItems()))
            {
                _Items.Add(i);
            }
        }
        public void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddPop.IsOpen = true;
        }

        public MainWindow() 
        {
            Store tp = (Store)Application.Current.FindResource("globStore");
            MainWindow window = new MainWindow(tp);
            window.Show();
            this.Close();
        }

        private void AddProdButton_Click(object sender, RoutedEventArgs e)
        {
            Product product = new Product(int.Parse(newId.Text), newName.Text, decimal.Parse(newPrice.Text));
            _Store.AddStoreItem(product, int.Parse(newQuantity.Text));
            AddPop.IsOpen = false;
            newId.Text = "";
            newName.Text = "";
            newPrice.Text = "";
            newQuantity.Text = "";
            RefreshList();
        }
    }

    
}
