using System;
using System.Collections.Generic;
using System.Text;
using EMS.Core.Framework;

namespace EMS.BusinessObjects
{
    #region Delegate: Collection change Event
    public delegate void NewItemAdded();
    #endregion

    #region Services
    internal class Services
    {
        private Services() { }

        private static ServiceFactory _factory;
        internal static ServiceFactory Factory
        {
            get { return _factory; }
        }

        static Services()
        {
            _factory = new ServiceFactory("EMS.Core.ServiceFactory");
        }
    }
    #endregion
}
