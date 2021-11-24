using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace WPF9
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<string> styles = new List<string>() { "Светлая тема", "Темная тема"};
            styleBox.ItemsSource = styles;
            styleBox.SelectionChanged += ThemeChange;
            styleBox.SelectedIndex = 0;
        }

        private void ThemeChange(object sender, SelectionChangedEventArgs e)
        {
            radioBlack.Content = "Черный";
            int styleIndex = styleBox.SelectedIndex;
            Uri uri = new Uri("Light.xaml", UriKind.Relative);
            Uri dic = new Uri("Dictionary1.xaml", UriKind.Relative);
            if (styleIndex == 1)
            {
                uri = new Uri("Dark.xaml", UriKind.Relative);
                radioBlack.Content = "Белый";
            }
            ResourceDictionary resource1 = Application.LoadComponent(uri) as ResourceDictionary;
            ResourceDictionary resource2 = Application.LoadComponent(dic) as ResourceDictionary;
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resource1);
            Application.Current.Resources.MergedDictionaries.Add(resource2);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (textBox != null)
            {
                textBox.FontFamily = new FontFamily(((sender as ComboBox).SelectedItem).ToString());
            }
        }
        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (textBox != null)
            {
                textBox.FontSize = Convert.ToDouble((sender as ComboBox).SelectedItem);
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            string textColor = (sender as RadioButton).Content as string;

            if (textBox != null)
            {
                if (textColor == "Красный")
                    textBox.Foreground = new SolidColorBrush(Colors.Red);
                else if (textColor == "Зеленый")
                    textBox.Foreground = new SolidColorBrush(Colors.Green);
                else if (textColor == "Белый")
                    textBox.Foreground = new SolidColorBrush(Colors.White);
                else
                    textBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void Button_Click_I(object sender, RoutedEventArgs e)
        {

            if (textBox.FontStyle == FontStyles.Normal)
            {
                textBox.FontStyle = FontStyles.Italic;
            }
            else
            {
                textBox.FontStyle = FontStyles.Normal;
            }
        }

        private void Button_Click_B(object sender, RoutedEventArgs e)
        {

            if (textBox.FontWeight == FontWeights.Normal)
            {
                textBox.FontWeight = FontWeights.Bold;
            }
            else
            {
                textBox.FontWeight = FontWeights.Normal;
            }
        }

        private void Button_Click_U(object sender, RoutedEventArgs e)
        {

            if (textBox.TextDecorations == TextDecorations.Underline)
            {
                textBox.TextDecorations = new TextDecorationCollection();
            }
            else
            {
                textBox.TextDecorations = TextDecorations.Underline;
            }
        }

        private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                textBox.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void SaveExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, textBox.Text);
            }
        }

        private void CloseExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
