using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using VKSdkAndroidWrapper;
using UnityEngine.UI;
using System.Collections;

public class MenuInstance : MonoBehaviour
{
    #region Static
    private static MenuInstance instance;
    public static MenuInstance I { get { return instance; } }
    #endregion
    #region UNITY_FIELDS

    public GameObject listMenu;

    public Text headerText;
    public Button startButton;
    public RectTransform scrollViewPanel;

    public GameObject shareMenu;

    public Image profilePhoto;
    public Text fullNameText;
    public Text idText;
    public Text inputText;

    public UserButton userButtonPrototype;
    #endregion
    #region Definition
    private int selectedUserID;
    #endregion
    #region Methods
    #region Private
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (AndroidWrapper.IsAuthorizationSuccess())
            headerText.text = "Token = " + AndroidWrapper.I.GetToken() + " ID = " + AndroidWrapper.I.GetAppID();
        else
            startButton.gameObject.SetActive(false);
    }

    private void OnUserListRecieved(List<User> users, bool success)
    {
        if (success)
            CreateButtons(users);
        else
        {
            startButton.gameObject.SetActive(true);
            startButton.GetComponentInChildren<Text>().text = "Try Again";
        }
    }

    private void CreateButtons(List<User> users)
    {
        scrollViewPanel.localPosition = new Vector2(scrollViewPanel.localPosition.x, scrollViewPanel.localPosition.y - users.Count * 70);
        scrollViewPanel.sizeDelta = new Vector2(scrollViewPanel.sizeDelta.x, scrollViewPanel.localPosition.y + users.Count * 70 * 2);

        float y = (users.Count - 1) * 70 * 0.5f;
        for (int i = 0; i < users.Count; i++)
        {
            var button = Instantiate(userButtonPrototype);
            button.Initialize(y - 70 * i, users[i], i, scrollViewPanel);
        }
    }

    #endregion

    public void LoadingFriends()
    {
        AndroidWrapper.I.RequestUserList("id,first_name,last_name,photo_200", OnUserListRecieved);
        startButton.gameObject.SetActive(false);
    }

    public void OpenListMenu()
    {
        listMenu.SetActive(true);
        shareMenu.SetActive(false);
    }

    public void OpenShareMenu(User user)
    {
        selectedUserID = user.ID;
        StartCoroutine(LoadShareMenu(user));
    }

    private IEnumerator LoadShareMenu(User user)
    {

        WWW www = new WWW(user.ProfilePhotoUrl);
        yield return www;

        profilePhoto.sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2());
        shareMenu.SetActive(true);
        listMenu.SetActive(false);
        fullNameText.text = user.FullName;
        idText.text = user.ID.ToString();
    }

    public void Share()
    {
        AndroidWrapper.I.Share(inputText.text, selectedUserID);
        OpenListMenu();
    }

    public void Exit()
    {
        Application.Quit();
    }

    #endregion
}
