using System;
using System.Collections.Generic;

namespace VendorClass1
{
    public class VendorClass
    {
        #region Public Area

        public VendorClass()
        {
            vendorPrivateList = new List<string>();
            RefreshItems();
        }


        public List<String> VendorPublicItems
        {
            get
            {
                if (vendorPrivateList.Count == 0 || DateTime.Now - lastUpdate > TimeSpan.FromSeconds(5))
                    RefreshItems();

                return vendorPrivateList;
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

        private List<string> vendorPrivateList;
        private DateTime lastUpdate;

        private void RefreshItems()
        {
            lastUpdate = DateTime.Now;
            vendorPrivateList.Add(string.Format("Time: {0}", lastUpdate));
        }

        #endregion

    }
}
