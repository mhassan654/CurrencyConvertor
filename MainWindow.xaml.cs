using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CurrencyConvertor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BindCurrency();
        }

        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            double ConvertableValue;

            // check amount textbox is nul or blank
            if (txtCurrency.Text == null || txtCurrency.Text.Trim() == "")
            {
                //if amount textbox is null or blank it will show the below message box
                MessageBox.Show("Please Enter currency","Information", MessageBoxButton.OK, MessageBoxImage.Information);

                //after clicking on message box ok sets the focus on amount txtbox
                txtCurrency.Focus();
                return;
            }else if (cmbFromCurrency.SelectedValue == null || cmbFromCurrency.SelectedIndex == 0)
            {
                MessageBox.Show("Please Select Currecny From", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                cmbFromCurrency.Focus();
                return;
            }
            else if (cmbToCurrency.SelectedValue == null || cmbToCurrency.SelectedIndex == 0)
            {
                MessageBox.Show("Please Select Currecny To", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

                cmbFromCurrency.Focus();
                return;
            }

            //check if value from and to combox sletec boxes are the same
            if (cmbFromCurrency.Text == cmbToCurrency.Text)
            {
                // amount textbox value set in converted value
                //double.parse is used for converting the datstype string
                // textbox text have string and convertedvalue is double
                ConvertableValue = double.Parse(txtCurrency.Text);

                //show the lable converted currency and converted currency name and tostring
                lblCurrency.Content=cmbToCurrency.Text + " " + ConvertableValue.ToString("N3");
            }
            else
            {
                ConvertableValue = (double.Parse(cmbFromCurrency.SelectedValue.ToString()) *
                    double.Parse(txtCurrency.Text)) /
                double.Parse(cmbToCurrency.SelectedValue.ToString());

                lblCurrency.Content=cmbToCurrency.Text + " " + ConvertableValue.ToString("N3");

            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ClearControls();
        }

       
        private void NumberValidationText(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BindCurrency()
        {
            DataTable dtCurrency = new DataTable();
            dtCurrency.Columns.Add("Text");
            dtCurrency.Columns.Add("Value");

            // add rows in the Datatable with text and values
            dtCurrency.Rows.Add("--SELECT--", 0);
            dtCurrency.Rows.Add("INR", 1);
            dtCurrency.Rows.Add("USD", 1.19);
            dtCurrency.Rows.Add("EUR", 1);
            dtCurrency.Rows.Add("SAR", 20);
            dtCurrency.Rows.Add("POUND", 5);
            dtCurrency.Rows.Add("DEM", 45);

            cmbFromCurrency.ItemsSource = dtCurrency.DefaultView;
            cmbFromCurrency.DisplayMemberPath = "Text";
            cmbFromCurrency.SelectedValuePath="Value";
            cmbFromCurrency.SelectedIndex = 0;

            //combox to currency
            cmbToCurrency.ItemsSource = dtCurrency.DefaultView;
            cmbToCurrency.DisplayMemberPath = "Text";
            cmbToCurrency.SelectedValuePath = "Value";
            cmbToCurrency.SelectedIndex = 0;
        }

        private void ClearControls()
        {
            txtCurrency.Text =string.Empty;
            if (cmbFromCurrency.Items.Count > 0) 
            {
                cmbFromCurrency.SelectedIndex = 0;
            }

            if (cmbToCurrency.Items.Count > 0)
            {
                cmbToCurrency.SelectedIndex = 0;
            }
            lblCurrency.Content = "";
            txtCurrency.Focus();


        }
    }
}