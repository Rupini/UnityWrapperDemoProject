using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


namespace VKSdkAndroidWrapper
{

    public class AndroidWrapper : MonoBehaviour, ISocialWrapper
    {
        #region Const

        private float COROUTINE_TICK_RATE = 0.1f;

        #endregion
        #region Static
        private static AndroidWrapper instance;
        public static AndroidWrapper I { get { return instance; } }

        private static bool initialized;

        public static void Initialize()
        {
            if (!initialized)
            {
                GameObject go = new GameObject("AndroidWrapper");
#if UNITY_ANDROID && !UNITY_EDITOR
               instance = go.AddComponent<AndroidWrapper>();
#else
                instance = null;
#endif
                initialized = true;
            }
        }

        #endregion
        #region Definition
        private AndroidJavaClass unityActivityClass;
        private AndroidJavaObject activityInstance;

        private bool requestInProcess;
        private Action<List<User>, bool> onDataRecieved;
        #endregion
        #region Initialize
        private void Awake()
        {
            unityActivityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            activityInstance = unityActivityClass.GetStatic<AndroidJavaObject>("currentActivity");
        }
        #endregion
        #region ISocialWrapper

        public bool IsAuthorizationSuccess()
        {
            if (activityInstance != null)
                return activityInstance.Call<bool>("isAuthorizationSuccess");
            else
                return false;
        }

        public string GetToken()
        {
            return activityInstance.Call<string>("getToken");
        }

        public string GetAppID()
        {
            return activityInstance.Call<string>("getVKAppId");
        }

        public void Share(string message, int usedID)
        {
            activityInstance.Call("share", message, usedID);
        }

        public void RequestUserList(string requestParams, Action<List<User>, bool> onDataRecieved)
        {
            if (!requestInProcess)
            {
                requestInProcess = true;
                this.onDataRecieved = onDataRecieved;
                activityInstance.Call("requestUserList", requestParams);
                StartCoroutine(Listening());
            }
        }
        #endregion
        #region Functional

        private IEnumerator Listening()
        {
            while (true)
            {
                if (requestInProcess && IsRequestComplited())
                {
                    requestInProcess = false;
                    var success = IsRequestSuccess();
                    List<User> users = null;
                    if (success)
                    {
                        users = new List<User>();
                        var userList = activityInstance.Call<AndroidJavaObject>("getUserList");

                        var size = userList.Call<int>("size");
                        for (int i = 0; i < size; i++)
                        {
                            var user = userList.Call<AndroidJavaObject>("get", i);
                            users.Add(new User(user));
                        }
                    }

                    onDataRecieved(users, success);
                    StopCoroutine(Listening());
                }
                yield return new WaitForSeconds(COROUTINE_TICK_RATE);
            }
        }

        private bool IsRequestComplited()
        {
            return activityInstance.Call<bool>("isRequestComplited");
        }

        private bool IsRequestSuccess()
        {
            return activityInstance.Call<bool>("isRequestSuccess");
        }
        #endregion
    }
}