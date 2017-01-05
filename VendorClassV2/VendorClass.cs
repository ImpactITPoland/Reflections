using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorClassV2
{
    public class VendorClass
    {
        #region Public Area

        public List<String> VendorPublicItems
        {
            get
            {
                if (vendorInternalList == null || DateTime.Now - lastUpdate > TimeSpan.FromSeconds(5))
                    RefreshItems();

                return vendorInternalList;
            }
        }

        public string DatetTime
        {
            get
            {
                return lastUpdate.ToString();
            }
        }
        #endregion

        #region Private Area

        private List<string> vendorInternalList;
        private DateTime lastUpdate;

        private void RefreshItems()
        {
            lastUpdate = DateTime.Now;
            if (vendorInternalList == null)
                vendorInternalList = new List<string>();

            vendorInternalList.Add(string.Format("Time: {0}", lastUpdate));
        }

        #endregion

    }
}
