using System;
using System.Reflection;
using System.Collections;
using EMS.Core;
using System.Web;

namespace EMS.Core.Framework
{
    #region Enumeration: ObjectState
    public enum ObjectState
    {
        New, Modified, Saved, Clone
    }
    #endregion

    #region Framwwork: Event delegate
    public delegate void ObjectIDChanged(ID newID);
    #endregion

    #region Framwwork: Business Object
    [Serializable]
    public class BusinessObject
    {
        protected ID _id = ID.Empty;
        protected ObjectState _objectState;

        [field: NonSerialized] // the event handler need to be non serialized
        public event ObjectIDChanged IDChanged;

        public ID ID
        {
            get { return _id; }
        }

        #region ObjectID
        public int ObjectID
        {
            get { return ID.ToInt32; }
        }
        #endregion

        public bool IsNew
        {
            get 
            {
                if (_objectState == ObjectState.Clone) return _objectState == ObjectState.Clone;
                return _objectState == ObjectState.New;
            }
        }

        public bool IsModified
        {
            get { return _objectState == ObjectState.Modified; }
        }

        public bool IsSaved
        {
            get { return _objectState == ObjectState.Saved; }
        }

        public bool IsClone
        {
            get { return _objectState == ObjectState.Clone; }
        }

        protected void SetObjectStateModified()
        {
            if (_objectState == ObjectState.Saved)
                _objectState = ObjectState.Modified;
        }

        // Inner class "Factory" is used to modify the Readonly Properties
        // This class should be only used by the corresponding service object
        // to assign the read only properties.

        // If the derived BuisinessObject class have any properties that it wants to be read only
        // it should define it's own Factory Class and derive it from BusinessObject.Factory and
        // the Service then can use the Factory to modify the read only proeprties.
        public class Factory
        {
            public static void SetID(BusinessObject bo, ID id)
            {
                bo._id = id;

                if (bo.IDChanged != null)
                    bo.IDChanged(id);
            }

            public static void SetObjectState(BusinessObject bo, ObjectState state)
            {
                bo._objectState = state;                
            }
            
        }

        public BusinessObject()
        {
            _objectState = ObjectState.New;
        }

        protected void SetThisAsParentOf(ChildCollection collection)
        {
            if (collection != null)
                collection.SetParent(this);
        }

        public override string ToString()
        {
            return GetType().Name + " (" + _id.ToString() + ")";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || (obj.GetType() != this.GetType()))
                return false;

            return _id.Equals(((BusinessObject)obj)._id);
        }

        public override int GetHashCode()
        {
            if ((object)_id == null)
                return base.GetHashCode();
            else
                return _id.GetHashCode();
        }

        public static bool operator ==(BusinessObject model1, BusinessObject model2)
        {
            if ((object)model1 != null)
                return model1.Equals(model2);
            else
                return ((object)model2 == null);
        }

        public static bool operator !=(BusinessObject model1, BusinessObject model2)
        {
            if ((object)model1 != null)
                return !model1.Equals(model2);
            else
                return !((object)model2 == null);
        }

        protected void MemberwiseCopy(BusinessObject copy)
        {
            foreach (FieldInfo fi in this.GetType().GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
            {
                fi.SetValue(this, fi.GetValue(copy));
            }
        }

    }

    #endregion

    #region Framework: Business Child Object
    [Serializable]
    public class ChildBusinessObject : BusinessObject
    {
        private ID _parentID;
        [NonSerialized]
        protected BusinessObject _parent;

        public ID parentID
        {
            get { return _parentID; }
        }

        protected internal void SetParent(BusinessObject parent)
        {
            _parentID = parent == null ? ID.Empty : parent.ID;
            _parent = parent;
        }
    }
    #endregion
}
