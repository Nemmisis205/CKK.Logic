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
        public StoreItem _HeldItem { get; set; }


        public MainWindow(Store store)
        {
            _Store = store;
            InitializeComponent();
            _Items = new ObservableCollection<StoreItem>();
            Product test1 = new Product(0, "Victory", 4.99m);
            Product test2 = new Product(0, "Freedom", 9.99m);
            Product test3 = new Product(0, "Bling", 999999999.99m);
            Product test4 = new Product(0, "Test", 3.33m);
            Product test5 = new Product(0, "Luck", 4.99m);
            Product test6 = new Product(0, "Skill", 9.99m);
            Product test7 = new Product(0, "Concentrated Power of Will", 999999999.99m);
            Product test8 = new Product(0, "Pleasure", 3.33m);
            Product test9 = new Product(0, "Pain", 3.33m);
            Product test10 = new Product(0, "Reason to Remember the Name", 3.33m);

            _Store.AddStoreItem(test1, 1);
            _Store.AddStoreItem(test2, 19);
            _Store.AddStoreItem(test3, 1);
            _Store.AddStoreItem(test4, 2);
            _Store.AddStoreItem(test5, 1);
            _Store.AddStoreItem(test6, 19);
            _Store.AddStoreItem(test7, 1);
            _Store.AddStoreItem(test8, 2);
            _Store.AddStoreItem(test9, 1);
            _Store.AddStoreItem(test10, 2);

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

        private void ItemButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            StoreItem buttonItem = clickedButton.DataContext as StoreItem;
            itemId.Text = buttonItem.Product.Id.ToString();
            itemName.Text = buttonItem.Product.Name;
            itemPrice.Text = buttonItem.Product.Price.ToString();
            itemQuantity.Text = buttonItem.Quantity.ToString();

            _HeldItem = buttonItem;
            ItemPopup.IsOpen = true;
        }

        private void ItemPopupCloseButton_Click(object sender, RoutedEventArgs e)
        {
            ItemPopup.IsOpen = false;
            _HeldItem = null;
            itemId.Text = "";
            itemName.Text = "";
            itemPrice.Text = "";
            itemQuantity.Text = "";
        }

        private void itemModifyButton_Click(object sender, RoutedEventArgs e)
        {
            Button modButton = sender as Button;
            if (itemName.IsEnabled == false)
            {
                itemName.IsEnabled = true;
                itemPrice.IsEnabled = true;
                itemQuantity.IsEnabled = true;
                modButton.Content = "Save Item";
            }
            else if (itemName.IsEnabled == true)
            {
                _HeldItem.Product.Name = itemName.Text;
                _HeldItem.Product.Price = decimal.Parse(itemPrice.Text);
                _HeldItem.Quantity = int.Parse(itemQuantity.Text);
                itemName.IsEnabled = false;
                itemPrice.IsEnabled = false;
                itemQuantity.IsEnabled = false;
                modButton.Content = "Modify Item";
                RefreshList();
            }

        }

        private void removeItemButton_Click(object sender, RoutedEventArgs e)
        {
            removePopup.IsOpen = true;
        }

        private void removeConfirmationButton_Click(object sender, RoutedEventArgs e)
        {
            _Store.DeleteStoreItem(_HeldItem.Product.Id);
            removePopup.IsOpen = false;
            ItemPopup.IsOpen = false;
            _HeldItem = null;
            itemId.Text = "";
            itemName.Text = "";
            itemPrice.Text = "";
            itemQuantity.Text = "";
            RefreshList();
        }

        private void confirmCloseButton_Click(object sender, RoutedEventArgs e)
        {
            removePopup.IsOpen = false;
        }
    }

    
}
