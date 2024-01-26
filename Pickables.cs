using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pickables : MonoBehaviour
{
    [SerializeField] string itemName; // Change this to a unique identifier for each pickable item
    // [SerializeField] TextMeshProUGUI stats;
    string message;

    void Start()
    {
        // Check if the item has already been picked up
        
        if (PlayerPrefs.GetInt(itemName + "_PickedUp", 0) == 1)
        {
            message="key Picked Up";
            // stats.text = message;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Player has picked up the item
            SetItemPickedUp();
        }
    }

    void SetItemPickedUp()
    {
        // Check if the item has not been picked up before setting PlayerPrefs
        if (PlayerPrefs.GetInt(itemName + "_PickedUp", 0) != 1)
        {
            // Set the PlayerPrefs key for this item to indicate it has been picked up
            PlayerPrefs.SetInt(itemName + "_PickedUp", 1);
            message = "Key Picked Up";
            // stats.text = message;
            Destroy(gameObject); // Destroy the object after it's picked up
        }
    }
}
