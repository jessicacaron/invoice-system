using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project.Common
{
    class clsItem : INotifyPropertyChanged
    {
        /// <summary>
        /// Item's Item Code
        /// </summary>
        public string sItemCode { get; set; }

        /// <summary>
        /// Item's description
        /// </summary>
        public string sItemDesc { get; set; }

        /// <summary>
        /// Item's cost
        /// </summary>
        public int iItemCost { get; set; }

        /// <summary>
        /// Constructor for item that assigns three values based on passed values
        /// </summary>
        /// <param name="sItemCode">Item Code</param>
        /// <param name="sItemDesc">Item Description</param>
        /// <param name="iItemCost">Item's Cost</param>
        public clsItem(string sItemCode, string sItemDesc, int iItemCost)
        {
            try
            {
                this.sItemCode = sItemCode;
                this.sItemDesc = sItemDesc;
                this.iItemCost = iItemCost;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Return string representation of an instance of this class
        /// Allows us to control how instances of this class are display in 
        /// elements such as ComboBoxes
        /// </summary>
        /// <returns>"[Item Description]"</returns>
        public override string ToString()
        {
            try
            {
                return sItemDesc;
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
    }
}
