using System;
using UnityEngine;

namespace VKSdkAndroidWrapper
{
    public class User : RawObject
    {
        #region Static
        private static string[] fieldNames = { "id", "fullName", "profilePhotoUrl" };
        private static FieldType[] fieldTypes = { FieldType.Int, FieldType.String, FieldType.String };
        #endregion
        #region Definiton
        private int id = -1;
        private string fullName;
        private string profilePhotoUrl;
        
        protected override string[] GetFieldNames()
        {
            return fieldNames;
        }

        protected override FieldType[] GetFielsTypes()
        {
            return fieldTypes;
        }
        #endregion
        #region Constructor
        public User(AndroidJavaObject jo)
            : base(jo)
        {
        }
        #endregion
        #region Properties
        public int ID
        {
            get
            {
                if (id == -1) id = Convert.ToInt32(fields["id"].ToString());
                return id;
            }
        }

        public string FullName
        {
            get
            {
                if (fullName == null) fullName = fields["fullName"].ToString();
                return fullName;
            }
        }

        public string ProfilePhotoUrl
        {
            get
            {
                if (profilePhotoUrl == null) profilePhotoUrl = fields["profilePhotoUrl"].ToString();
                return profilePhotoUrl;
            }
        }
        #endregion
    }
}
