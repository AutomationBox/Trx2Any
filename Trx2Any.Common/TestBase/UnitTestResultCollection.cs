using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trx2Any.Common.TestBase
{
    
    public class UnitTestResultCollection : List<UnitTestResult>
    {
        //public event EventHandler UnitTestResultAdded;
        //public void Add(UnitTestResult unitTestResult)
        //{
        //    this.Add(unitTestResult);
        //    //if (UnitTestResultAdded != null)
        //    //{
        //    //    UnitTestResultAdded((object)UnitTestResultAdded, new EventArgs());
        //    //}
        //}

        public string CollectionName
        {
            get;
            set;
        }
        public UnitTestResultCollection(string collectionName)
        {
            this.CollectionName = collectionName;
        }
        public UnitTestResult Item(int index)
        {
            return (UnitTestResult)this[index];
        }

    }
}
