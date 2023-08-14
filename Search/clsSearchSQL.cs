using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Reflection;

namespace Group_Project.Search
{
    class clsSearchSQL
    {

        /// <summary>
        /// Creates a Sql statement to return all invoices from the invoice table
        /// </summary>
        /// <returns>String: SQL statement</returns>
        /// <exception cref="Exception"></exception>
        public static string GetInvoices()
        {
            try
            {
                string sSQL = "SELECT* FROM Invoices";
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Creates a Sql statement to return invoice based on invoice number
        /// </summary>
        /// <returns>String: SQL statement</returns>
        /// <exception cref="Exception"></exception>
        public static string GetInvoicesByNumber(string invoiceNum)
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + invoiceNum;
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Creates a Sql statement to return invoice based on invoice number and date and cost
        /// </summary>
        /// <returns>String: SQL statement</returns>
        /// <exception cref="Exception"></exception>
        public static string GetInvoicesByAll(string invoiceNum, string invoiceDate, string invoiceCost)
        {
            try
            {
                string sSQL = "SELECT* FROM Invoices WHERE InvoiceNum = " + invoiceNum + " AND TotalCost = " + invoiceCost +  " AND InvoiceDate = #" + invoiceDate + "#";
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Creates a Sql statement to return invoice based on invoice number and date
        /// </summary>
        /// <returns>String: SQL statement</returns>
        /// <exception cref="Exception"></exception>
        public static string GetInvoicesByNumberDate(string invoiceNum, string invoiceDate)
        {
            try
            {
                string sSQL = "SELECT* FROM Invoices WHERE InvoiceNum = " + invoiceNum + " AND InvoiceDate = #" + invoiceDate + "#";
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Creates a Sql statement to return invoice based on invoice number and cost
        /// </summary>
        /// <returns>String: SQL statement</returns>
        /// <exception cref="Exception"></exception>
        public static string GetInvoicesByNumberCost(int invoiceNum, int invoiceCost)
        {
            try
            {
                string sSQL = "SELECT* FROM Invoices WHERE InvoiceNum = " + invoiceNum.ToString() + " AND TotalCost = " + invoiceCost.ToString();
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Creates a Sql statement to return invoice based on invoice date and cost
        /// </summary>
        /// <returns>String: SQL statement</returns>
        /// <exception cref="Exception"></exception>
        public static string GetInvoicesByCostDate(string invoiceCost, string invoiceDate)
        {
            try
            {
                string sSQL = "SELECT* FROM Invoices WHERE TotalCost = " + invoiceCost + " AND InvoiceDate = #" + invoiceDate + "#";
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Creates a Sql statement to return invoice based on cost
        /// </summary>
        /// <returns>String: SQL statement</returns>
        /// <exception cref="Exception"></exception>
        public static string GetInvoicesByCost(string invoiceCost)
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices WHERE TotalCost = " + invoiceCost;
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Creates a Sql statement to return invoice based on date
        /// </summary>
        /// <returns>String: SQL statement</returns>
        /// <exception cref="Exception"></exception>
        public static string GetInvoicesByDate(string invoiceDate)
        {
            try
            {
                string sSQL = "SELECT * FROM Invoices WHERE InvoiceDate = #" + invoiceDate + "#";
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// Creates a Sql statement to return distinct invoices based on invoice number
        /// </summary>
        /// <returns>String: SQL statement</returns>
        /// <exception cref="Exception"></exception>
        public static string GetDistinctInvoiceNum()
        {
            try
            {
                string sSQL = "SELECT DISTINCT(InvoiceNum) From Invoices order by InvoiceNum";
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Creates a Sql statement to return distinct invoices based on invoice date
        /// </summary>
        /// <returns>String: SQL statement</returns>
        /// <exception cref="Exception"></exception>
        public static string GetDistinctDate()
        {
            try
            {
                string sSQL = "SELECT DISTINCT(InvoiceDate) From Invoices order by InvoiceDate";
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Creates a Sql statement to return distinct invoices based on invoice total cost
        /// </summary>
        /// <returns>String: SQL statement</returns>
        /// <exception cref="Exception"></exception>
        public static string GetDistinctCost()
        {
            try
            {
                string sSQL = "SELECT DISTINCT(TotalCost) From Invoices order by TotalCost";
                return sSQL;
            }
            catch (Exception ex)
            {

                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


    }
}
