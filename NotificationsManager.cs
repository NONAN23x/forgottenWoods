using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationsManager : MonoBehaviour
{
    public static NotificationsManager instance;

    public NotificationsTemplate[] notificationsTemplates;

    [SerializeField] TextMeshProUGUI textPlaceHolder;
    [SerializeField] TextMeshProUGUI status;
    [SerializeField] TextMeshProUGUI tips;

    void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
    }

    public void SendToastNotification(String toast) {
            textPlaceHolder.text = toast;
    }

    // public void statusIndicator {
    //     // Check if the item has been picked up
    //     if (PlayerPrefs.GetInt(itemName + "_PickedUp", 0) == 1)
    //     {
    //         // Display the item picked up message
    //         messageText.text = itemName + " has been picked up!";
    //     }
    //     else
    //     {
    //         // Clear the message if the item has not been picked up
    //         messageText.text = "";
    //     }
    // }
}
