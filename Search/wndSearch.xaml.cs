using Group_Project.Common;
using Group_Project.Main;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
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

namespace Group_Project.Search
{
    /// <summary>
    /// Interaction logic for wndSearch.xaml
    /// </summary>
    public partial class wndSearch : Window
    {
        /// <summary>
        /// Connects to the search logic
        /// </summary>
        private clsSearchLogic searchLogic = new clsSearchLogic();
        /// <summary>
        /// Holds value of the selected Invoice as invoice
        /// </summary>
        clsInvoice selectedInvoice;
        /// <summary>
        /// Holds value of the selected invoice ID
        /// </summary>
        public int selectedInvoiceID;
        /// <summary>
        /// Refernced by main screen to see if user chose to edit an invoice
        /// </summary>
        public bool editSelected = false;



        public wndSearch()
        {
            InitializeComponent();

            try
            {
                //set the list of invoices to display in the datagrid
                dgDisplayInvoices.ItemsSource = searchLogic.GetAllInvoices();
                clsSearchLogic newSearch = new clsSearchLogic();
                newSearch.GetInvoiceList();
                cbInvoiceNumber.ItemsSource = newSearch.invoiceNumberList;
                cbInvoiceDate.ItemsSource = newSearch.invoiceDateList;
                cbTotalCost.ItemsSource = newSearch.invoiceCostList;
            }
            catch (Exception ex) 
            { 
                HandleError(ex); 
            }

        }

        /// <summary>
        /// Sets all combo boxes to null and resets the datagrid to display all invoices.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //set all combo boxes to null
                cbInvoiceDate.SelectedItem = null;
                cbTotalCost.SelectedItem = null;
                cbInvoiceNumber.SelectedItem = null;
                //reload datagrid
                dgDisplayInvoices.ItemsSource = searchLogic.GetAllInvoices();
            }
            catch (Exception ex)
            {

                HandleError(ex);
            }
        }

        /// <summary>
        /// Closes window and sets the edit mode and InvoiceID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectInvoice_Click(object sender, RoutedEventArgs e)
        {
            //add invoice number to selected and close window
            try
            {
                //set the SelectedInvoiceID to whatever the invoiceID is from datagrid current selected item cast as a invoice item
                //close search window back to edit
                if (dgDisplayInvoices.SelectedItem != null)
                {
                    clsInvoice clsSelectedInvoice = (clsInvoice)dgDisplayInvoices.SelectedItem;
                    selectedInvoice = clsSelectedInvoice;
                    selectedInvoiceID = clsSelectedInvoice.iInvoiceID;
                    editSelected = true;
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        /// <summary>
        /// Allows user to quit searching and go back to main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                editSelected = false;
                this.Hide();
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        /// <summary>
        /// Provides the data grid with a list of invoices based on which filters are selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbSelection_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                //all three
                if (cbInvoiceNumber.SelectedItem != null && cbTotalCost.SelectedItem != null && cbInvoiceDate.SelectedItem != null)
                {
                    int a = (int)cbInvoiceNumber.SelectedItem;
                    string b = cbInvoiceDate.SelectedItem.ToString();
                    int c = (int)cbTotalCost.SelectedItem;
                    dgDisplayInvoices.ItemsSource = searchLogic.GetInvoiceByAll(a, b, c);
                }
                //Number and Cost
                if (cbInvoiceNumber.SelectedItem != null && cbTotalCost.SelectedItem != null && cbInvoiceDate.SelectedItem == null)
                {
                    int a = (int)cbInvoiceNumber.SelectedItem;
                    int c = (int)cbTotalCost.SelectedItem;
                    dgDisplayInvoices.ItemsSource = searchLogic.GetInvoiceByNumberCost(a, c);
                }
                //Number and Date
                if (cbInvoiceNumber.SelectedItem != null && cbTotalCost.SelectedItem == null && cbInvoiceDate.SelectedItem != null)
                {
                    int a = (int)cbInvoiceNumber.SelectedItem;
                    string b = cbInvoiceDate.SelectedItem.ToString();
                    dgDisplayInvoices.ItemsSource = searchLogic.GetInvoiceByNumberDate(a, b);
                }
                //Cost and Date
                if (cbInvoiceNumber.SelectedItem == null && cbTotalCost.SelectedItem != null && cbInvoiceDate.SelectedItem != null)
                {
                    string b = cbInvoiceDate.SelectedItem.ToString();
                    int c = (int)cbTotalCost.SelectedItem;
                    dgDisplayInvoices.ItemsSource = searchLogic.GetInvoiceByCostDate(c, b);
                }
                //Number
                if (cbInvoiceNumber.SelectedItem != null && cbTotalCost.SelectedItem == null && cbInvoiceDate.SelectedItem == null)
                {
                    int a = (int)cbInvoiceNumber.SelectedItem;
                    dgDisplayInvoices.ItemsSource = searchLogic.GetInvoiceByNumber(a);
                }
                if (cbInvoiceNumber.SelectedItem == null && cbTotalCost.SelectedItem != null && cbInvoiceDate.SelectedItem == null)
                {
                    int c = (int)cbTotalCost.SelectedItem;
                    dgDisplayInvoices.ItemsSource = searchLogic.GetInvoiceByCost(c);
                }
                if (cbInvoiceNumber.SelectedItem == null && cbTotalCost.SelectedItem == null && cbInvoiceDate.SelectedItem != null)
                {
                    string b = cbInvoiceDate.SelectedItem.ToString();
                    dgDisplayInvoices.ItemsSource = searchLogic.GetInvoiceByDate(b);
                }
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }


        /// <summary>
        /// Handles the window close event and when user clicks "X" handle the close properly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                editSelected = false;
                this.Hide();
                e.Cancel = true;
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        /// <summary>
        /// Handles the error, should a catch find one.
        /// </summary>
        /// <param name="ex"></param>
        private void HandleError(Exception ex)
        {
            Trace.WriteLine(ex.Message);
        }
    }
}
