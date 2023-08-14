using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using Group_Project.Common;
using Group_Project.Search;

namespace Group_Project.Main
{
    /// <summary>
    /// Interaction logic for wndMain.xaml
    /// </summary>
    public partial class wndMain : Window
    {

        /// <summary>
        /// Communicate with the main window's business logic
        /// </summary>
        private clsMainLogic mainLogic;

        /// <summary>
        /// Used as the source for the datagrid when creating a new invoice
        /// </summary>
        private ObservableCollection<clsItem> lineItemsSource = new ObservableCollection<clsItem>();

        /// <summary>
        /// The search window
        /// </summary>
        private wndSearch wndSearch;

        public wndMain()
        {
            InitializeComponent();

            // Shutdown the application when the main window closes
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;

            mainLogic = new clsMainLogic();
            //wndSearch = new wndSearch();

            // Populate the dropdowns for items that can be added to an invoice
            cbItems.ItemsSource = mainLogic.GetItemsCollection();
            cbEditorItems.ItemsSource = mainLogic.GetItemsCollection();

            // Load the invoices in the system into memory
            mainLogic.GetInvoicesCollection();

            // Set up the datagrid for new items in a new invoice to use lineItemsSource as its source
            dgLineItems.ItemsSource = lineItemsSource;

        }

        /// <summary>
        /// Opens the Search window when the menu item is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wndSearch = new wndSearch();

                this.Hide();
                wndSearch.ShowDialog();
                this.Show();
                // If an invoice was selected to be edited then
                if (wndSearch.editSelected == true)
                {
                    // Access the SelectedInvoiceID from the search window
                    // Load the selected invoice into the editor UI
                    // Logic for loading the invoice into the editor in LoadInvoiceForEdit
                    LoadInvoiceForEdit(wndSearch.selectedInvoiceID);
                }

            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        /// <summary>
        /// Opens the form to update an invoice's information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadInvoiceForEdit(int iInvoiceID)
        {
            try
            {
                btnEditorAddItem.IsEnabled = false;
                btnEditorRemoveItem.IsEnabled = false;

                cbEditorItems.SelectedItem = null;

                tabEditInvoice.IsSelected = true;

                txtBlSelectInvoice.Visibility = Visibility.Collapsed;
                gridEditInvoice.Visibility = Visibility.Visible;

                clsInvoice invoice = mainLogic.GetInvoiceByID(iInvoiceID);

                dgEditorLineItems.ItemsSource = mainLogic.GetInvoiceLineItems(invoice.iInvoiceID);

                txtBxEditorInvoiceID.Text = invoice.iInvoiceID.ToString();

                txtBxEditorInvoiceDate.Text = invoice.sInvoiceDate;
                calEditorInvoiceDate.DisplayDate = DateTime.Parse(invoice.sInvoiceDate);
                calEditorInvoiceDate.SelectedDate = DateTime.Parse(invoice.sInvoiceDate);

                txtBxEditorTotalCost.Text = "$" + invoice.iTotalCost.ToString();
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        /* This functionality does not belong in the Main window, it belongs in the Search window

        /// <summary>
        /// Fired when user selects an invoice that is currently in the system
        /// Enables the "Edit Invoice" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgInvoices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dgInvoices.SelectedItems.Count > 0)
                {
                    btnEditInvoice.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        */

        /// <summary>
        /// Handler for both of the Add Item buttons
        /// Figures out whether the user is adding an item to a new invoice or an existing invoice
        /// Adds the item to the appropriate invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAddItemClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                ObservableCollection<clsItem> itemsSource;
                ComboBox cb;

                if (btn.Equals(btnAddItem))
                {
                    itemsSource = lineItemsSource;
                    cb = cbItems;
                }
                else
                {
                    itemsSource = (ObservableCollection<clsItem>)dgEditorLineItems.ItemsSource;
                    cb = cbEditorItems;
                }

                if (cb.SelectedItem != null)
                {
                    clsItem newItem = (clsItem)cb.SelectedItem;
                    itemsSource.Add(newItem);
                }

                UpdateTotalCost(itemsSource);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        /// <summary>
        /// Fired when user selects an item on an invoice
        /// Figures out whether the user is selecting an item from a new or existing invoice
        /// Enables the button to remove an item from the invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ItemsDataGridSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid dg = (DataGrid)sender;

                if (dg.SelectedItems.Count > 0)
                {
                    if (dg.Equals(dgLineItems))
                    {
                        btnRemoveItem.IsEnabled = true;
                    }
                    else
                    {
                        btnEditorRemoveItem.IsEnabled = true;
                    }

                }
            }
            catch(Exception ex)
            {
                HandleError(ex);
            }
        }

        /// <summary>
        /// Fired when the user clicks the Remove Item button
        /// Figures out whether to remove an item from the new invoice or an existing invoice
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonRemoveItemClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                DataGrid items;
                ObservableCollection<clsItem> itemsSource;

                if (btn.Equals(btnRemoveItem))
                {
                    items = dgLineItems;
                }
                else
                {
                    items = dgEditorLineItems;
                }

                itemsSource = (ObservableCollection<clsItem>)items.ItemsSource;
                itemsSource.RemoveAt(items.SelectedIndex);

                btn.IsEnabled = false;
                UpdateTotalCost(itemsSource);
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        /// <summary>
        /// Updates the Total Cost TextBox
        /// Fired whenever a change occurs to the items on an invoice
        /// </summary>
        /// <param name="itemsSource"></param>
        private void UpdateTotalCost(ObservableCollection<clsItem> itemsSource)
        {
            try
            {
                int totalCost = 0;

                foreach (clsItem i in itemsSource)
                {
                    totalCost += i.iItemCost;
                }

                if (itemsSource.Equals(lineItemsSource))
                {
                    txtBxTotalCost.Text = "$" + totalCost.ToString();
                }
                else
                {
                    txtBxEditorTotalCost.Text = "$" + totalCost.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
            
        }

        /// <summary>
        /// Fired when the user clicks Save Invoice button on the New Invoice form
        /// Saves a new invoice to the database and resets the form to its default state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveInvoice_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // User must select a date
                if (calInvoiceDate.SelectedDate != null)
                {
                    txtBxError.Visibility = Visibility.Collapsed;

                    mainLogic.SaveNewInvoice((DateTime)calInvoiceDate.SelectedDate, lineItemsSource);

                    txtBxTotalCost.Text = "";
                    txtBxItemCost.Text = "";
                    txtBxInvoiceDate.Text = "";
                    calInvoiceDate.SelectedDate = null;
                    calInvoiceDate.DisplayDate = DateTime.Today;
                    cbItems.SelectedItem = null;
                    btnAddItem.IsEnabled = false;
                }
                else
                {
                    // Tell the user they must select a date
                    txtBxError.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        /// <summary>
        /// Fired when user clicks Discard Changes when editing an existing invoice
        /// Closes the Edit Invoice screen and resets it to its default state
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditorDiscardChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                gridEditInvoice.Visibility = Visibility.Collapsed;
                txtBlSelectInvoice.Visibility = Visibility.Visible;

                txtBxEditorTotalCost.Text = "";
                txtBxEditorItemCost.Text = "";
                txtBxEditorInvoiceDate.Text = "";
                calEditorInvoiceDate.SelectedDate = null;
                calInvoiceDate.DisplayDate = DateTime.Today;
                cbEditorItems.SelectedItem = null;
                btnEditorAddItem.IsEnabled = false;
                btnEditorRemoveItem.IsEnabled = false;
                dgEditorLineItems.ItemsSource = null;
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        /// <summary>
        /// Fired when a selection is made from one of the item dropdowns
        /// Figures out whether user is editing an existing invoice or making a new invoice
        /// Enables the appropriate Add Item button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBox cb = (ComboBox)sender;

                if (cb.SelectedItem != null)
                {
                    clsItem item = (clsItem)cb.SelectedItem;

                    if (cb.Equals(cbItems))
                    {
                        btnAddItem.IsEnabled = true;
                        txtBxItemCost.Text = "$" + item.iItemCost.ToString();
                    }
                    else if (cb.Equals(cbEditorItems))
                    {
                        btnEditorAddItem.IsEnabled = true;
                        txtBxEditorItemCost.Text = "$" + item.iItemCost.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        /// <summary>
        /// Fired when the user selects a date on one of the Calendars
        /// Figures out whether user is editing an existing invoice or making a new invoice
        /// Updates the TextBox that writes out the string of which date is selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalendarSelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Calendar cal = (Calendar)sender;

                if (cal.SelectedDate != null)
                {
                    DateTime date = (DateTime)cal.SelectedDate;

                    if (cal.Equals(calInvoiceDate))
                    {
                        txtBxInvoiceDate.Text = date.ToShortDateString();
                    }
                    else if (cal.Equals(calEditorInvoiceDate))
                    {
                        txtBxEditorInvoiceDate.Text = date.ToShortDateString();
                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        /// <summary>
        /// Fired when user clicks Save Changes when editing an existing invoice
        /// Saves the changes the user made to the invoice to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditorSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mainLogic.UpdateInvoice(Int32.Parse(txtBxEditorInvoiceID.Text), (DateTime)calEditorInvoiceDate.SelectedDate, (ObservableCollection<clsItem>)dgEditorLineItems.ItemsSource);

                txtBxEditorTotalCost.Text = "";
                txtBxEditorItemCost.Text = "";
                txtBxEditorInvoiceDate.Text = "";
                calEditorInvoiceDate.SelectedDate = null;
                calInvoiceDate.DisplayDate = DateTime.Today;
                cbEditorItems.SelectedItem = null;
                btnEditorAddItem.IsEnabled = false;
                btnEditorRemoveItem.IsEnabled = false;
                dgEditorLineItems.ItemsSource = null;

                gridEditInvoice.Visibility = Visibility.Collapsed;
                txtBlSelectInvoice.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
        }

        private void HandleError(Exception ex)
        {
            Trace.WriteLine(ex.Message);
        }

    }
}
