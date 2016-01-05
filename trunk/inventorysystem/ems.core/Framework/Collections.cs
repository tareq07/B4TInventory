using System;
using System.Collections;
using System.Web;

namespace EMS.Core.Framework
{
    #region Framework: BusinessObjects
    [Serializable]
    public class BusinessObjectCollection : CollectionBase
    {
        public bool Contains(BusinessObject businessObject)
        {
            return InnerList.Contains(businessObject);
        }

    }

    #endregion

    #region Framework: IndexedBusinessObjects
    [Serializable]
    public abstract class IndexedBusinessObjects : CollectionBase
    {
        protected Hashtable _hashtable = new Hashtable();
        private int _autoKey = 0;
        protected virtual ID GetKeyID(BusinessObject item)
        {
            return item.ID;
        }

        protected int AddItem(BusinessObject item)
        {
            if (item.IsNew && item.ID.ToInt32 == 0)
            {
                _autoKey--;
                BusinessObject.Factory.SetID(item, new ID(_autoKey));
            }

            ID keyID = GetKeyID(item);

            int index;
            if (_hashtable.Contains(keyID))
            {
                index = (int)_hashtable[keyID];
                InnerList[index] = item;
            }
            else
            {
                index = InnerList.Add(item);
                _hashtable.Add(keyID, index);
            }
            return index;
        }
        protected void AddItem(BusinessObject item, int index)
        {
            if (item.IsNew && item.ID.ToInt32 == 0)
            {
                _autoKey--;
                BusinessObject.Factory.SetID(item, new ID(_autoKey));
            }

            ID keyID = GetKeyID(item);

            int curIndex;
            if (_hashtable.Contains(keyID))
            {
                curIndex = (int)_hashtable[keyID];
                InnerList[curIndex] = item;
            }
            else
            {
                InnerList.Insert(index, item);
                _hashtable.Add(keyID, index);
                // Update Index
                for (int i = index; i < InnerList.Count; i++)
                {
                    BusinessObject mc = (BusinessObject)InnerList[i];
                    _hashtable[GetKeyID(mc)] = i;

                }
            }
        }
        protected void Swap(BusinessObject obj1, BusinessObject obj2)
        {
            int nIndex1 = this.GetIndex(obj1.ID);
            int nIndex2 = this.GetIndex(obj2.ID);
            this.RemoveItem(obj1);
            this.RemoveItem(obj2);

            if (this.Count < nIndex1)
            {
                this.AddItem(obj2);
                this.AddItem(obj1);
            }
            else
            {
                this.AddItem(obj2, nIndex1);
                this.AddItem(obj1, nIndex2);
            }
        }

        public bool Contains(BusinessObject businessObject)
        {
            return InnerList.Contains(businessObject);
        }

        public bool Contains(ID keyID)
        {
            return _hashtable.Contains(keyID);
        }
        protected void RemoveItem(BusinessObject item)
        {
            ID key = GetKeyID(item);
            if (_hashtable.Contains(key))
            {
                int index = (int)_hashtable[key];
                _hashtable.Remove(key);

                InnerList.Remove(item);
                // Update Index

                for (int i = index; i < InnerList.Count; i++)
                {
                    BusinessObject mc = (BusinessObject)InnerList[i];
                    _hashtable[GetKeyID(mc)] = i;

                }
            }
        }

        protected override void OnClear()
        {
            base.OnClear();
            _hashtable.Clear();
        }

        protected BusinessObject GetItem(int index)
        {
            return (BusinessObject)InnerList[index];  
        }
        protected int GetIndex(ID id)
        {
            int index = -1;
            if (_hashtable.Contains(id))
            {
                index = (int)_hashtable[id];
            }
            return index;
        }


        protected BusinessObject GetItem(ID id)
        {
            if (_hashtable.Contains(id))
            {
                int index = (int)_hashtable[id];
                return (BusinessObject)InnerList[index];
            }
            else
                return null;
        }

        protected BusinessObject GetItem(BusinessObject keyObject)
        {
            if (keyObject != null)
            {
                return GetItem(keyObject.ID);
            }
            else
                return null;
        }

        protected string IDInString()
        {
            string sReturn = "";
            foreach (BusinessObject oItem in this)
            {
                sReturn = sReturn + oItem.ObjectID.ToString() + ",";
            }
            if (sReturn.Length > 0) sReturn = sReturn.Remove(sReturn.Length - 1, 1);
            return sReturn;
        }
    }
    #endregion

    #region Framework: Child Collection
    [Serializable]
    public class ChildCollection : CollectionBase
    {
        protected BusinessObject _parent;
        protected ArrayList _removedItems = new ArrayList();

        protected object GetRemovedItems(Type type)
        {
            return _removedItems.ToArray(type);
        }

        public void SetParent(BusinessObject parent)
        {
            _parent = parent;

            // set the parent for contained items
            foreach (ChildBusinessObject item in InnerList)
            {
                item.SetParent(_parent);
            }
        }

        protected virtual int AddItem(ChildBusinessObject item)
        {
            item.SetParent(_parent);
            return InnerList.Add(item);
        }

        protected virtual void RemoveItem(ChildBusinessObject item)
        {
            if (item != null)
            {
                item.SetParent(null);
                if (!item.IsNew)
                    _removedItems.Add(item);
            }

            InnerList.Remove(item);
        }

        protected ChildBusinessObject GetItem(int index)
        {
            return (ChildBusinessObject)InnerList[index];
        }

        protected void SetItem(int index, ChildBusinessObject item)
        {
            if (InnerList[index] != null)
                ((ChildBusinessObject)InnerList[index]).SetParent(null);

            if (item != null)
                item.SetParent(_parent);

            InnerList[index] = item;
        }

        protected int GetIndex(ID id)
        {
            int index = -1;
            foreach (ChildBusinessObject oItem in InnerList)
            {
                index = index + 1;
                if (oItem.ID == id)
                {
                    return index;
                }
            }
            return -1;
        }

        public ChildCollection() { }
    }

    #endregion

    #region Framework: IndexedChildCollection
    [Serializable]
    public abstract class IndexedChildCollection : ChildCollection
    {
        protected Hashtable _hashtable = new Hashtable();
        private int _autoKey = 0;
        protected virtual ID GetKeyID(ChildBusinessObject item)
        {
            return item.ID;
        }

        protected override int AddItem(ChildBusinessObject item)
        {
            if (item.IsNew && item.ID.ToInt32 == 0)
            {
                _autoKey--;
                BusinessObject.Factory.SetID(item, new ID(_autoKey));
            }

            ID keyID = GetKeyID(item);
            int index;
            if (_hashtable.Contains(keyID))
            {
                index = (int)_hashtable[keyID];
                base.SetItem(index, item);
            }
            else
            {
                index = base.AddItem(item);
                _hashtable.Add(keyID, index);
            }

            return index;
        }
        protected void AddItem(ChildBusinessObject item, int index)
        {
            if (item.IsNew && item.ID.ToInt32 == 0)
            {
                _autoKey--;
                ChildBusinessObject.Factory.SetID(item, new ID(_autoKey));
            }

            ID keyID = GetKeyID(item);

            int curIndex;
            if (_hashtable.Contains(keyID))
            {
                curIndex = (int)_hashtable[keyID];
                InnerList[curIndex] = item;
            }
            else
            {
                InnerList.Insert(index, item);
                _hashtable.Add(keyID, index);
                // Update Index
                for (int i = index; i < InnerList.Count; i++)
                {
                    ChildBusinessObject mc = (ChildBusinessObject)InnerList[i];
                    _hashtable[GetKeyID(mc)] = i;

                }
            }
        }


        protected override void RemoveItem(ChildBusinessObject item)
        {
            ID key = GetKeyID(item);
            if (_hashtable.Contains(key))
            {
                int index = (int)_hashtable[key];
                _hashtable.Remove(key);

                base.RemoveItem(item);

                // Update Index

                for (int i = index; i < InnerList.Count; i++)
                {
                    ChildBusinessObject child = (ChildBusinessObject)InnerList[i];
                    _hashtable[GetKeyID(child)] = i;

                }
            }
        }
        protected void Swap(ChildBusinessObject obj1, ChildBusinessObject obj2)
        {
            int nIndex1 = this.GetIndex(obj1.ID);
            int nIndex2 = this.GetIndex(obj2.ID);
            this.RemoveItem(obj1);
            this.RemoveItem(obj2);

            if (this.Count < nIndex1)
            {
                this.AddItem(obj2);
                this.AddItem(obj1);
            }
            else
            {
                this.AddItem(obj2, nIndex1);
                this.AddItem(obj1, nIndex2);
            }
        }

        protected ChildBusinessObject GetItem(ID id)
        {
            if (_hashtable.Contains(id))
            {
                int index = (int)_hashtable[id];
                return (ChildBusinessObject)InnerList[index];
            }
            else
                return null;
        }

        protected ChildBusinessObject GetItem(BusinessObject keyObject)
        {
            if (keyObject != null)
            {
                return GetItem(keyObject.ID);
            }
            else
                return null;
        }

        protected override void OnClear()
        {
            base.OnClear();
            _hashtable.Clear();
        }
    }

    #endregion
}
