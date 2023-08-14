using Group_Project.Main;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Group_Project.Search
{
    public class clsSearchLogic
    {
        /// <summary>
        /// 
        /// </summary>
        public clsDataAccess db = new clsDataAccess();
        /// <summary>
        /// 
        /// </summary>
        public List<string> invoiceCompleteList = new List<string>();
        /// <summary>
        /// 
        /// </summary>
        public List<int> invoiceNumberList = new List<int>();
        /// <summary>
        /// 
        /// </summary>
        public List<string> invoiceDateList = new List<string>();
        /// <summary>
        /// 
        /// </summary>
        public List<int> invoiceCostList = new List<int>();


        /// <summary>
        /// Sets the entire list of invoices
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void GetInvoiceList()
        {
            try
            {
                DataSet ds1;
                DataSet ds2;
                DataSet ds3;

                int iRet1 = 0;
                int iRet2 = 0;
                int iRet3 = 0;

                ds1 = db.ExecuteSQLStatement(clsSearchSQL.GetDistinctInvoiceNum(), ref iRet1);
                ds2 = db.ExecuteSQLStatement(clsSearchSQL.GetDistinctDate(), ref iRet2);
                ds3 = db.ExecuteSQLStatement(clsSearchSQL.GetDistinctCost(), ref iRet3);

                //Loop through all the values returned
                for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                {
                    int newInvoiceNumber;
                    newInvoiceNumber = (int)ds1.Tables[0].Rows[i][0];
                    invoiceNumberList.Add(newInvoiceNumber);
                }

                //Loop through all the values returned
                for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
                {
                    string newInvoiceDate;
                    newInvoiceDate = ds2.Tables[0].Rows[i][0].ToString();
                    invoiceDateList.Add(newInvoiceDate);
                }

                //Loop through all the values returned
                for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
                {
                    int newInvoiceCost;
                    newInvoiceCost = (int)ds3.Tables[0].Rows[i][0];
                    invoiceCostList.Add(newInvoiceCost);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns all invoices in a list of invoices
        /// </summary>
        /// <returns>List<clsInvoice></returns>
        /// <exception cref="Exception"></exception>
        public List<clsInvoice> GetAllInvoices()
        {
            try
            {
                DataSet ds;

                int iRet = 0;
                ds = db.ExecuteSQLStatement(clsSearchSQL.GetInvoices(), ref iRet);

                List<clsInvoice> newInvoices = new List<clsInvoice>();

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    clsInvoice invoice = new clsInvoice();
                    invoice.iInvoiceID = (int)ds.Tables[0].Rows[i][0];
                    invoice.sInvoiceDate = ds.Tables[0].Rows[i][1].ToString();
                    invoice.iTotalCost = (int)ds.Tables[0].Rows[i][2];
                    newInvoices.Add(invoice);
                }
                return newInvoices;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns all invoices by invoice number
        /// </summary>
        /// <param name="invoiceNo"></param>
        /// <returns>List<clsInvoice></returns>
        /// <exception cref="Exception"></exception>
        public List<clsInvoice> GetInvoiceByNumber(int invoiceNo)
        {
            try
            {
                List<clsInvoice> numberList = new List<clsInvoice>();
                DataSet ds = new DataSet();

                int iRet = 0;

                ds = db.ExecuteSQLStatement(clsSearchSQL.GetInvoicesByNumber(invoiceNo.ToString()), ref iRet);

                //Loop through all the values returned
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    clsInvoice invoice = new clsInvoice();
                    invoice.iInvoiceID = (int)ds.Tables[0].Rows[i][0];
                    invoice.sInvoiceDate = ds.Tables[0].Rows[i][1].ToString();
                    invoice.iTotalCost = (int)ds.Tables[0].Rows[i][2];
                    numberList.Add(invoice);
                }
                return numberList;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns all invoices by cost
        /// </summary>
        /// <param name="invoiceCost"></param>
        /// <returns>List<clsInvoice></returns>
        /// <exception cref="Exception"></exception>
        public List<clsInvoice> GetInvoiceByCost(int invoiceCost)
        {
            try
            {
                List<clsInvoice> costList = new List<clsInvoice>();
                DataSet ds = new DataSet();

                int iRet = 0;

                ds = db.ExecuteSQLStatement(clsSearchSQL.GetInvoicesByCost(invoiceCost.ToString()), ref iRet);

                //Loop through all the values returned
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    clsInvoice invoice = new clsInvoice();
                    invoice.iInvoiceID = (int)ds.Tables[0].Rows[i][0];
                    invoice.sInvoiceDate = ds.Tables[0].Rows[i][1].ToString();
                    invoice.iTotalCost = (int)ds.Tables[0].Rows[i][2];
                    costList.Add(invoice);
                }

                return costList;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns all invoices by date
        /// </summary>
        /// <param name="invoiceDate"></param>
        /// <returns>List<clsInvoice></returns>
        /// <exception cref="Exception"></exception>
        public List<clsInvoice> GetInvoiceByDate(string invoiceDate)
        {
            try
            {
                List<clsInvoice> numberList = new List<clsInvoice>();
                DataSet ds = new DataSet();

                int iRet = 0;

                ds = db.ExecuteSQLStatement(clsSearchSQL.GetInvoicesByDate(invoiceDate), ref iRet);

                //Loop through all the values returned
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    clsInvoice invoice = new clsInvoice();
                    invoice.iInvoiceID = (int)ds.Tables[0].Rows[i][0];
                    invoice.sInvoiceDate = ds.Tables[0].Rows[i][1].ToString();
                    invoice.iTotalCost = (int)ds.Tables[0].Rows[i][2];
                    numberList.Add(invoice);
                }

                return numberList;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns all invoices by invoice number and cost
        /// </summary>
        /// <param name="invoiceNo"></param>
        /// <param name="invoiceCost"></param>
        /// <returns>List<clsInvoice></returns>
        /// <exception cref="Exception"></exception>
        public List<clsInvoice> GetInvoiceByNumberCost(int invoiceNo, int invoiceCost)
        {
            try
            {
                List<clsInvoice> numberList = new List<clsInvoice>();
                DataSet ds = new DataSet();

                int iRet = 0;

                ds = db.ExecuteSQLStatement(clsSearchSQL.GetInvoicesByNumberCost(invoiceNo, invoiceCost), ref iRet);

                //Loop through all the values returned
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    clsInvoice invoice = new clsInvoice();
                    invoice.iInvoiceID = (int)ds.Tables[0].Rows[i][0];
                    invoice.sInvoiceDate = ds.Tables[0].Rows[i][1].ToString();
                    invoice.iTotalCost = (int)ds.Tables[0].Rows[i][2];
                    numberList.Add(invoice);
                }

                return numberList;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns all invoices by invoice number and date
        /// </summary>
        /// <param name="invoiceNo"></param>
        /// <param name="invoiceDate"></param>
        /// <returns>List<clsInvoice></returns>
        /// <exception cref="Exception"></exception>
        public List<clsInvoice> GetInvoiceByNumberDate(int invoiceNo, string invoiceDate)
        {
            try
            {
                List<clsInvoice> numberDateList = new List<clsInvoice>();
                DataSet ds = new DataSet();

                int iRet = 0;

                ds = db.ExecuteSQLStatement(clsSearchSQL.GetInvoicesByNumberDate(invoiceNo.ToString(), invoiceDate), ref iRet);

                //Loop through all the values returned
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    clsInvoice invoice = new clsInvoice();
                    invoice.iInvoiceID = (int)ds.Tables[0].Rows[i][0];
                    invoice.sInvoiceDate = ds.Tables[0].Rows[i][1].ToString();
                    invoice.iTotalCost = (int)ds.Tables[0].Rows[i][2];
                    numberDateList.Add(invoice);
                }

                return numberDateList;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns all invoices by cost and date
        /// </summary>
        /// <param name="invoiceCost"></param>
        /// <param name="invoiceDate"></param>
        /// <returns>List<clsInvoice></returns>
        /// <exception cref="Exception"></exception>
        public List<clsInvoice> GetInvoiceByCostDate(int invoiceCost, string invoiceDate)
        {
            try
            {
                List<clsInvoice> costDateList = new List<clsInvoice>();
                DataSet ds = new DataSet();

                int iRet = 0;

                ds = db.ExecuteSQLStatement(clsSearchSQL.GetInvoicesByCostDate(invoiceCost.ToString(), invoiceDate), ref iRet);

                //Loop through all the values returned
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    clsInvoice invoice = new clsInvoice();
                    invoice.iInvoiceID = (int)ds.Tables[0].Rows[i][0];
                    invoice.sInvoiceDate = ds.Tables[0].Rows[i][1].ToString();
                    invoice.iTotalCost = (int)ds.Tables[0].Rows[i][2];
                    costDateList.Add(invoice);
                }

                return costDateList;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Returns a list of invoice objects based on invoice id, total cost, and date
        /// </summary>
        /// <param name="invoiceNo">Int</param>
        /// <param name="invoiceDate">String</param>
        /// <param name="invoiceCost">Int</param>
        /// <returns>List<clsInvoice></returns>
        /// <exception cref="Exception"></exception>

        public List<clsInvoice> GetInvoiceByAll(int invoiceNo, string invoiceDate, int invoiceCost)
        {
            try
            {
                List<clsInvoice> allList = new List<clsInvoice>();
                DataSet ds = new DataSet();

                int iRet = 0;

                ds = db.ExecuteSQLStatement(clsSearchSQL.GetInvoicesByAll(invoiceNo.ToString(), invoiceDate, invoiceCost.ToString()), ref iRet);

                //Loop through all the values returned
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    clsInvoice invoice = new clsInvoice();
                    invoice.iInvoiceID = (int)ds.Tables[0].Rows[i][0];
                    invoice.sInvoiceDate = ds.Tables[0].Rows[i][1].ToString();
                    invoice.iTotalCost = (int)ds.Tables[0].Rows[i][2];
                    allList.Add(invoice);
                }
                return allList;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
