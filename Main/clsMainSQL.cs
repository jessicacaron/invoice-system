using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace Group_Project.Main
{
    internal class clsMainSQL
    {

        /// <summary>
        /// SQL to update an invoice's total cost
        /// </summary>
        /// <param name="iNewCost">New total cost</param>
        /// <param name="iInvoiceID">InvoiceNum to update</param>
        /// <returns>SQL statement to update invoice cost</returns>
        /// <exception cref="Exception"></exception>
        public static string UpdateInvoice(DateTime invoiceDate, int iNewCost, int iInvoiceID)
        {
            try
            {
                string sSQL = "UPDATE Invoices SET InvoiceDate = #" + invoiceDate.ToShortDateString() +  "#, TotalCost = " + iNewCost + " WHERE InvoiceNum = " + iInvoiceID;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL to add a line item to an invoice
        /// </summary>
        /// <param name="iInvoiceID">InvoiceNum to add onto</param>
        /// <param name="iLineItemNum">Which line number this item is</param>
        /// <param name="sItemCode">The item code to add</param>
        /// <returns>SQL statement to add item onto invoice</returns>
        /// <exception cref="Exception"></exception>
        public static string InsertLineItem(int iInvoiceID, int iLineItemNum, string sItemCode)
        {
            try
            {
                string sSQL = "INSERT INTO LineItems (InvoiceNum, LineItemNum, ItemCode) VALUES (" + iInvoiceID + ", " + iLineItemNum + ", '" + sItemCode + "')";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL to make a new invoice
        /// </summary>
        /// <param name="invoiceDate">Date of the invoice</param>
        /// <param name="iTotalCost">Invoice's total cost</param>
        /// <returns>SQL statement to make a new invoice</returns>
        /// <exception cref="Exception"></exception>
        public static string InsertInvoice(DateTime invoiceDate, int iTotalCost)
        {
            try
            {
                string sSQL = "INSERT INTO Invoices (InvoiceDate, TotalCost) Values (#" + invoiceDate.ToShortDateString() + "#, " + iTotalCost + ")";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL to select an invoice
        /// </summary>
        /// <param name="iInvoiceID">InvoiceNum to be selected</param>
        /// <returns>SQL statement to select an invoice</returns>
        /// <exception cref="Exception"></exception>
        public static string SelectInvoice(int iInvoiceID)
        {
            try
            {
                string sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices WHERE InvoiceNum = " + iInvoiceID;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL to get the ID of the last invoice that was added to the system
        /// </summary>
        /// <returns>SQL statement to get the ID of the last invoice that was added to the system</returns>
        /// <exception cref="Exception"></exception>
        public static string SelectLastInvoice()
        {
            try
            {
                string sSQL = "SELECT MAX(InvoiceNum) FROM Invoices";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL to select all invoices
        /// </summary>
        /// <returns>SQL statement to select all invoices</returns>
        /// <exception cref="Exception"></exception>
        public static string SelectAllInvoices()
        {
            try
            {
                string sSQL = "SELECT InvoiceNum, InvoiceDate, TotalCost FROM Invoices";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL to select all items in the system
        /// </summary>
        /// <returns>SQL statement to select all items in the system</returns>
        /// <exception cref="Exception"></exception>
        public static string SelectAllItems()
        {
            try
            {
                string sSQL = "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL to select all line items from an invoice
        /// </summary>
        /// <param name="iInvoiceID">InvoiceNum from which to select line items</param>
        /// <returns>SQL statement to select line items from an invoice</returns>
        /// <exception cref="Exception"></exception>
        public static string SelectInvoiceLineItems(int iInvoiceID)
        {
            try
            {
                string sSQL = "SELECT LineItems.ItemCode, ItemDesc.ItemDesc, ItemDesc.Cost FROM LineItems, ItemDesc WHERE LineItems.ItemCode = ItemDesc.ItemCode AND LineItems.InvoiceNum = " + iInvoiceID;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL to delete all line items from an invoice
        /// </summary>
        /// <param name="iInvoiceID">InvoiceNum from which to delete line items</param>
        /// <returns>SQL statement to delete all line items from invoice</returns>
        /// <exception cref="Exception"></exception>
        public static string DeleteInvoiceLineItems(int iInvoiceID)
        {
            try
            {
                string sSQL = "DELETE FROM LineItems WHERE InvoiceNum = " + iInvoiceID;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

    }
    
}
