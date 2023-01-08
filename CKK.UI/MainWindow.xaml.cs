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
using CKK.DB.Interfaces;
using CKK.DB.Repository;
using CKK.DB.UOW;

namespace CKK.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProductRepository _Store;
        public ObservableCollection<Product> _Items { get; private set; }
        public Product _HeldItem { get; set; }


        //public MainWindow(ProductRepository store)
        //{
        //    _Store = new ProductRepository(new DatabaseConnectionFactory());
        //    InitializeComponent();
        //    _Items = new ObservableCollection<Product>();
        //    invBox.ItemsSource = _Items;
        //    RefreshList();
        //}

        private void RefreshList()
        {
            _Items.Clear();
            foreach (Product i in new ObservableCollection<Product>(_Store.GetAll()))
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
            _Store = new ProductRepository(new DatabaseConnectionFactory());
            InitializeComponent();
            _Items = new ObservableCollection<Product>();
            invBox.ItemsSource = _Items;
            RefreshList();
            this.Show();
        }

        private void AddProdButton_Click(object sender, RoutedEventArgs e)
        {
            Product product = new Product(int.Parse(newId.Text),decimal.Parse(newPrice.Text), int.Parse(newQuantity.Text), newName.Text);
            _Store.Add(product);
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
            Product buttonItem = clickedButton.DataContext as Product;
            itemId.Text = buttonItem.Id.ToString();
            itemName.Text = buttonItem.Name;
            itemPrice.Text = buttonItem.Price.ToString();
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
                _HeldItem.Name = itemName.Text;
                _HeldItem.Price = decimal.Parse(itemPrice.Text);
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
            _Store.Delete(_HeldItem);
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

        //private void Search_Button_Click(object sender, RoutedEventArgs e)
        //{
        //    var sortedItems = new ObservableCollection<Product>();
        //    if (ID_Radio.IsChecked == true)
        //    {
        //        foreach (Product i in _Store.GetByName(SearchText.Text))
        //        {
        //            sortedItems.Add(i);
        //        }
        //    }
        //    else if (Quantity_Radio.IsChecked == true)
        //    {
        //        foreach (Product i in _Store.GetProductsByQuantity(_Store.GetAllProductsByName(SearchText.Text)))
        //        {
        //            sortedItems.Add(i);
        //        }
        //    }
        //    else if (Price_Radio.IsChecked == true)
        //    {
        //        foreach (Product i in _Store.GetProductsByPrice(_Store.GetAllProductsByName(SearchText.Text)))
        //        {
        //            sortedItems.Add(i);
        //        }
        //    }
        //    invBox.ItemsSource = sortedItems;
        //}

        private void Search_Button_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Clear_Button_Click(object sender, RoutedEventArgs e)
        {
            invBox.ItemsSource = _Items;
            RefreshList();
        }
    }

    
}
