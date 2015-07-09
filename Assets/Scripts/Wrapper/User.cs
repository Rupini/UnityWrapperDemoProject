using System;
using UnityEngine;

namespace VKSdkAndroidWrapper
{
    public class User : RawObject
    {
        #region Static
        private static string[] fieldNames = { "id", "fullName", "profilePhotoUrl", "isDisabled" };
        private static FieldType[] fieldTypes = { FieldType.Int, FieldType.String, FieldType.String, FieldType.Bool };
        #endregion
        #region Definiton
        private int id = -1;
        private string fullName;
        private string profilePhotoUrl;
        private bool? isDisabled;

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
                if (id == -1) id = (int)fields["id"];
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

        public bool IsDisabled
        {
            get
            {
                if (isDisabled == null) isDisabled = (bool)fields["isDisabled"];
                return isDisabled.Value;
            }
        }
        #endregion
    }
}
