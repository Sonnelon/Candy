using CandyCat.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CandyCat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

   
    public partial class MainWindow : Window
    {
        SolidColorBrush brushMouseOver = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#66a8de"));

        SolidColorBrush brushMouseEnter = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2da6c4"));


        int countCourt = 0;

        DbStaff dbStaff = new DbStaff();

        List<Sweets> sweetsData = new List<Sweets>();

        string searchHelp = "🔍Введите название";

        private ObservableCollection<string> items;

        private int countData;

    
       
        public MainWindow()
        {
 

            InitializeComponent();

            AddToCourt();

            TypeComboBox.Items.Clear();


            TypeComboBox.ItemsSource = new List<string>() {"All","Cake","Cupcake","Cookie", "Brownie","Cheesecake","Tart","Pancake","Pie","Roll" };
            
            TypeComboBox.SelectedIndex = 0;

            SortTextBox.ItemsSource = new List<string>() { "По названию", "По уменьшению цены", "По увеличению цены" };

            SortTextBox.SelectedIndex = 0;

         

            GetData();

            countData = sweetsData.Count;

            FillSuggestions();

            SearchBox.Text = searchHelp;

 
            SearchBox.Text = searchHelp;


            Sort();

        }

        public void FillSuggestions()
        {

            SearchBox.FilterMode = AutoCompleteFilterMode.Contains;

   
            SearchBox.ItemsSource = sweetsData.Select(sweet => sweet.Name).ToList();

        }
       

        public void GetData()
        {
            sweetsData = dbStaff.GetData();

           

            SweetsListView.ItemsSource = sweetsData;

        }

        private void SweetAddClick(object sender, RoutedEventArgs e)
        {
            countCourt++;
            AddToCourt();
        }

       

        private void addButton_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void SweetsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        public void SearchHintAdd()
        {
            if (SearchBox.Text == "")
            {

                SearchBox.Text = searchHelp;

                SearchBox.Opacity = 0.7;

            }
            
        }

        public void SearchHintClear()
        {
            if (searchHelp == SearchBox.Text)
            {
                SearchBox.Opacity = 1;

              SearchBox.Text = "";
            }
       
        }

       

        private void Search_LostFocus(object sender, RoutedEventArgs e)
        {
            SearchHintAdd();
        }

        private void Search_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchHintClear();
        }

        public void Search()
        {
            string searchText = SearchBox.Text.ToLower().Trim();

            List<Sweets> sweetTemp = new List<Sweets>();

            List<Sweets> sweeeTempType = new List<Sweets>();

            sweeeTempType = ChangeType(sweetsData);

            if (SearchBox.Text == searchHelp)
            {
                searchText = "";
            }


                for (int i = 0; i < sweeeTempType.Count; i++)
                {
                    if (sweeeTempType[i].Name.ToLower().Trim().Contains(searchText))
                    {
                        sweetTemp.Add(sweeeTempType[i]);
                    }

                }
             
                 SweetsListView.ItemsSource = sweetTemp;


                  SearchBox.ItemsSource = sweetTemp.Select(sweet => sweet.Name).ToList();
        }

        private void SearchBox_TextChanged(object sender, RoutedEventArgs e)
        {
            Search();
        }
        public List<Sweets> ChangeType(List<Sweets> sweets)
        {
            string type = TypeComboBox.SelectedItem.ToString();

            if (type == "All")
            {
                return sweets;
            }

            List<Sweets> tempData = new List<Sweets>();

            for (int i = 0; i < sweetsData.Count; i++)
            {
                if (sweets[i].Type == type)
                {
                    tempData.Add(sweets[i]);
                }
            }
            return tempData;
        }
      
        private void TypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }

        private void TypeComboBox_Selected(object sender, RoutedEventArgs e)
        {
            Search();
        }

        public void Sort()
        {

            switch (SortTextBox.SelectedIndex)
            {
                case 0: sweetsData = sweetsData.OrderBy(x => x.Name).ToList();  break;
                case 1: sweetsData = sweetsData.OrderByDescending(x => x.Price).ToList(); break;
                case 2: sweetsData = sweetsData.OrderBy(x => x.Price).ToList(); break; 

                
                
                default:
                    break;
            }
           
            Search();


        }
        public void AddToCourt()
        {

            if (countCourt > 0)
            {
                CourtCount.Visibility = System.Windows.Visibility.Visible;

                TextBlockCount.Visibility = System.Windows.Visibility.Visible;

                if (countCourt < 10)
                {
                    TextBlockCount.Text = countCourt.ToString();
                }
                else
                {
                    TextBlockCount.Text = "9+";
                }

                
            }
            else
            {
                CourtCount.Visibility = System.Windows.Visibility.Hidden;

                TextBlockCount.Visibility = System.Windows.Visibility.Hidden;
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sort();
        }

        private void CartButton_Click(object sender, RoutedEventArgs e)
        {

        }

     

        public void ButtonChange(bool IsMouseOver)
        {


            if (IsMouseOver) {

                CartButton.Background = brushMouseEnter;

            } else {

                CartButton.Background = brushMouseOver;
            }

        }

        private void CartButton_MouseEnter(object sender, MouseEventArgs e)
        {
            ButtonChange(true);
        }

        private void CartButton_MouseLeave(object sender, MouseEventArgs e)
        {
            ButtonChange(false);
        }

        private void SearchBox_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            MessageBox.Show("");
        }

        private void SearchBox_ToolTipClosing(object sender, ToolTipEventArgs e)
        {
            MessageBox.Show("");

        }
    }
}
