using System.Collections.Generic;
using UnityEngine;

namespace VKSdkAndroidWrapper
{
    public abstract class RawObject
    {
        #region Definition
        protected Dictionary<string, object> fields;
        protected abstract string[] GetFieldNames();
        protected abstract FieldType[] GetFielsTypes();
        #endregion
        #region Constructor
        public RawObject(AndroidJavaObject jo)
        {
            fields = new Dictionary<string, object>();
            int i = 0;
            foreach (var fieldName in GetFieldNames())
            {
                object value = null;
                switch (GetFielsTypes()[i])
                {
                    case FieldType.Int:
                        value = jo.Get<int>(fieldName);
                        break;
                    case FieldType.Float:
                        value = jo.Get<float>(fieldName);
                        break;
                    case FieldType.String:
                        value = jo.Get<string>(fieldName);
                        break;
                    default:

                        break;
                }
                fields.Add(fieldName, value);
                i++;
            }
        }
        
        public RawObject(Dictionary<string, object> fields)
        {
            this.fields = fields;
        }
        #endregion
        #region Methods
        public T Get<T>(string fieldName, ref bool success)
        {
            success = fields.ContainsKey(fieldName) && fields[fieldName].GetType() == typeof(T);
            if (success)
                return (T)fields[fieldName];
            else
                return default(T);
        }
        #endregion
    }
}
