using Group_Project.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Group_Project.Main
{
    internal class clsMainLogic
    {

        /// <summary>
        /// Allows us to communicate with the database
        /// </summary>
        private clsDataAccess db = new clsDataAccess();

        /// <summary>
        /// Holds all the items that are stored in the system
        /// </summary>
        private ObservableCollection<clsItem> itemsCollection;

        /// <summary>
        /// Holds all the invoices that are stored in the system
        /// With this we can add new invoices to the program without
        /// having to retrieve them all from the database every time
        /// a new invoice is added
        /// </summary>
        private ObservableCollection<clsInvoice> invoicesCollection;

        /// <summary>
        /// Gets all the items from the database so that the view can access that information
        /// </summary>
        /// <returns>A List of all the items stored in the database</returns>
        public ObservableCollection<clsItem> GetItemsCollection()
        {
            try
            {
                if (itemsCollection == null)
                {
                    int rows = 0;

                    itemsCollection = new ObservableCollection<clsItem>();
                    DataSet dataSetItems = db.ExecuteSQLStatement(clsMainSQL.SelectAllItems(), ref rows);
                    foreach (DataRow row in dataSetItems.Tables[0].Rows)
                    {
                        itemsCollection.Add(new clsItem(row[0].ToString(), row[1].ToString(), Int32.Parse(row[2].ToString())));
                    }
                }

                return itemsCollection;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets all the invoices from the database so that the view can access that information
        /// </summary>
        /// <returns>A List of all the invoices stored in the database</returns>
        public ObservableCollection<clsInvoice> GetInvoicesCollection()
        {
            try
            {
                if (invoicesCollection == null)
                {
                    int rows = 0;

                    invoicesCollection = new ObservableCollection<clsInvoice>();
                    DataSet dataSetItems = db.ExecuteSQLStatement(clsMainSQL.SelectAllInvoices(), ref rows);
                    foreach (DataRow row in dataSetItems.Tables[0].Rows)
                    {
                        invoicesCollection.Add(new clsInvoice(Int32.Parse(row[0].ToString()), DateTime.Parse(row[1].ToString()).ToShortDateString(), Int32.Parse(row[2].ToString())));
                    }
                }

                return invoicesCollection;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This can be used to grab a specific invoice from its ID
        /// </summary>
        /// <param name="iInvoiceID">The ID of the invoice to return</param>
        /// <returns>The invoice that corresponds to the input invoice ID</returns>
        public clsInvoice GetInvoiceByID(int iInvoiceID)
        {
            foreach (clsInvoice invoice in invoicesCollection)
            {
                if (invoice.iInvoiceID == iInvoiceID)
                {
                    return invoice;
                }
            }

            return null;
        }

        /// <summary>
        /// Called by the view in order to save a new invoice into the database
        /// </summary>
        /// <param name="invoiceDate">The date of the new invoice</param>
        /// <param name="lineItems">The list of items that are on the invoice</param>
        public void SaveNewInvoice(DateTime invoiceDate, ObservableCollection<clsItem> lineItems)
        {
            try
            {
                int totalCost = 0;

                foreach (clsItem i in lineItems)
                {
                    totalCost += i.iItemCost;
                }

                // Save the invoice to the database
                db.ExecuteNonQuery(clsMainSQL.InsertInvoice(invoiceDate, totalCost));

                // Get the auto-generated ID of the invoice
                int iInvoiceNum = Int32.Parse(db.ExecuteScalarSQL(clsMainSQL.SelectLastInvoice()));

                invoicesCollection.Add(new clsInvoice(iInvoiceNum, invoiceDate.ToShortDateString(), totalCost));

                // Save all the line items to the database
                for (int i = 0; i < lineItems.Count; i++)
                {
                    db.ExecuteNonQuery(clsMainSQL.InsertLineItem(iInvoiceNum, (i + 1), lineItems[i].sItemCode));
                }

                // Clear out the collection of line items so the next new invoice can start from scratch
                lineItems.Clear();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Gets the line items on any invoice from the database
        /// </summary>
        /// <param name="iInvoiceID">The invoice from which to get the line items</param>
        /// <returns>List of line items on the invoice</returns>
        public ObservableCollection<clsItem> GetInvoiceLineItems(int iInvoiceID)
        {
            try
            {
                int rows = 0;

                ObservableCollection<clsItem> lineItemsCollection = new ObservableCollection<clsItem>();
                DataSet dataSetItems = db.ExecuteSQLStatement(clsMainSQL.SelectInvoiceLineItems(iInvoiceID), ref rows);
                foreach (DataRow row in dataSetItems.Tables[0].Rows)
                {
                    lineItemsCollection.Add(new clsItem(row[0].ToString(), row[1].ToString(), Int32.Parse(row[2].ToString())));
                }
                return lineItemsCollection;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing invoice in the database
        /// </summary>
        /// <param name="iInvoiceID">The ID of the invoice to be updated</param>
        /// <param name="invoiceDate">The new invoice date</param>
        /// <param name="lineItems">The new List of line items to save to the invoice</param>
        /// <param name="index">The index in the invoice DataGrid that the invoice to update is</param>
        public void UpdateInvoice(int iInvoiceID, DateTime invoiceDate, ObservableCollection<clsItem> lineItems)
        {
            try
            {
                int totalCost = 0;

                foreach (clsItem i in lineItems)
                {
                    totalCost += i.iItemCost;
                }

                // Save the changes to the invoice to the database
                db.ExecuteNonQuery(clsMainSQL.UpdateInvoice(invoiceDate, totalCost, iInvoiceID));

                // Get the index of the invoice in memory so we can update it
                int index = -1;

                foreach (clsInvoice invoice in invoicesCollection)
                {
                    if (invoice.iInvoiceID == iInvoiceID)
                    {
                        index = invoicesCollection.IndexOf(invoice);
                        break;
                    }
                }

                // Update invoice in memory
                if (index != -1)
                {
                    invoicesCollection[index] = new clsInvoice(iInvoiceID, invoiceDate.ToShortDateString(), totalCost);
                }

                // Clear saved line items
                db.ExecuteNonQuery(clsMainSQL.DeleteInvoiceLineItems(iInvoiceID));

                // Save all the current line items to the database
                for (int i = 0; i < lineItems.Count; i++)
                {
                    db.ExecuteNonQuery(clsMainSQL.InsertLineItem(iInvoiceID, (i + 1), lineItems[i].sItemCode));
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }
}
