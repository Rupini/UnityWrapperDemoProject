using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using VKSdkAndroidWrapper;
using UnityEngine.UI;

public class GameInstance : MonoBehaviour
{
    private static GameInstance instance;
    public static GameInstance I { get { return instance; } }

    public MenuInstance menuInstance;
    public Text debugText;

    private bool hasError;

    private void Awake()
    {
        instance = this;
        AndroidWrapper.Initialize();
        menuInstance.OpenListMenu();
        if (!AndroidWrapper.I.IsAuthorizationSuccess())
        {
            WriteError(AndroidWrapper.I.GetJaveExeption());
        }
    }

    public void WriteError(string errorMessage)
    {
        if (!hasError) { hasError = true; debugText.gameObject.SetActive(true); }
        debugText.text += errorMessage + Environment.NewLine;
    }
}
