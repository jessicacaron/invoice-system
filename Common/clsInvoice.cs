using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project
{
    public class clsInvoice : INotifyPropertyChanged
    {

        /// <summary>
        /// Invoice's ID
        /// </summary>
        public int iInvoiceID { get; set; }

        /// <summary>
        /// Invoice's date in short date string form
        /// </summary>
        public string sInvoiceDate { get; set; }

        /// <summary>
        /// Invoice's total cost
        /// </summary>
        public int iTotalCost { get; set; }

        /// <summary>
        /// Constructor for invoice that takes three parameters and sets them as it is created
        /// </summary>
        /// <param name="iInvoiceID">Invoice ID</param>
        /// <param name="sInvoiceDate">Invoice Date</param>
        /// <param name="iTotalCost">Invoice's total cost</param>
        public clsInvoice(int iInvoiceID, string sInvoiceDate, int iTotalCost)
        {
            try
            {
                this.iInvoiceID = iInvoiceID;
                this.sInvoiceDate = sInvoiceDate;
                this.iTotalCost = iTotalCost;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        
        /// <summary>
        /// Default constructor for invoices adding default values
        /// </summary>
        public clsInvoice()
        {
            try
            {
                this.iInvoiceID = 0;
                this.sInvoiceDate = "";
                this.iTotalCost = 0;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Allows a DataGrid to see when an instance of this class is changed
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Overrides the ToString method to send only the invoiceid as a string
        /// </summary>
        /// <returns>Invoice's ID</returns>
        public override string ToString()
        {
            try
            {
                return iInvoiceID.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
