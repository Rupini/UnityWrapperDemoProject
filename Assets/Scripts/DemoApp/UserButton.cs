using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using VKSdkAndroidWrapper;

public class UserButton : Button
{
    private Text fullName;
    private RectTransform rectT;
    private User user;

    protected override void Awake()
    {
        fullName = GetComponentInChildren<Text>();
        rectT = GetComponent<RectTransform>();
    }

    public void Initialize(float posY, User user, int index, Transform parrent)
    {
        rectT.localPosition = new Vector2(0, posY);
        rectT.SetParent(parrent, false);
        this.user = user;
        if (index == 0)
            fullName.text = "[ " + user.FullName + " ]";
        else
            fullName.text = index + ") " + user.FullName;
    }

    public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (eventData.clickTime > 0.5f)
            MenuInstance.I.OpenShareMenu(user);
    }
}
