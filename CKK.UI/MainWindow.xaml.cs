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
using CKK.Persistance;
using CKK.Persistance.Interfaces;
using CKK.Persistance.Models;

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


        public MainWindow(FileStore store)
        {
            _Store = new FileStore();
            InitializeComponent();
            _Items = new ObservableCollection<StoreItem>();
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
            FileStore tp = Application.Current.FindResource("globStore") as FileStore;
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

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            var sortedItems = new ObservableCollection<StoreItem>();
            if (ID_Radio.IsChecked == true)
            {
                foreach (StoreItem i in _Store.GetAllProductsByName(SearchText.Text))
                {
                    sortedItems.Add(i);
                }
            }
            else if (Quantity_Radio.IsChecked == true)
            {
                foreach (StoreItem i in _Store.GetProductsByQuantity(_Store.GetAllProductsByName(SearchText.Text)))
                {
                    sortedItems.Add(i);
                }
            }
            else if (Price_Radio.IsChecked == true)
            {
                foreach (StoreItem i in _Store.GetProductsByPrice(_Store.GetAllProductsByName(SearchText.Text)))
                {
                    sortedItems.Add(i);
                }
            }
            invBox.ItemsSource = sortedItems;
        }

        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            invBox.ItemsSource = _Items;
            RefreshList();
        }
    }

    
}
