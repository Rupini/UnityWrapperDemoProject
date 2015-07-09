using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VKSdkAndroidWrapper
{
    public interface ISocialWrapper
    {
        string GetToken();

        string GetAppID();

        void Share(string message, int usedID);

        void RequestUserList(string requestParams, Action<List<User>, bool> onDataRecieved);

    }
}
