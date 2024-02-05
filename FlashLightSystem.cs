using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlashLightSystem : MonoBehaviour
{
    [SerializeField] float lightDecay = .07f;
    [SerializeField] float angleDecay = 1f;
    [SerializeField] float minimumAngle = 42f;
    [SerializeField] float minimumIntensity = 0.7f;

    Light myLight;

    private void Start() {
        myLight = GetComponent<Light>();
    }

    private void Update() {
        // Check for a key press (for example, the 'D' key)
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            // Call a method to delete PlayerPrefs data
            PlayerPrefs.DeleteAll();
            Debug.Log("Deleted Player Data");
        }

        DecreaseLightAngle();
        DecreaseLightIntensity();
    }

    public void RestoreLightAngle(float restoreAngle) {
        myLight.spotAngle = restoreAngle;
    }

    public void RestoreLightIntensity(float intensityAmount) {
        myLight.intensity += intensityAmount;
    }

    private void DecreaseLightAngle()
    {
        if (myLight.spotAngle<=minimumAngle) {
            return;
        } else {
            myLight.spotAngle -= angleDecay * Time.deltaTime;
        }
    }

    private void DecreaseLightIntensity()
    {
        if (myLight.intensity <= minimumIntensity) {
            return;
        } else {
            myLight.intensity -= lightDecay * Time.deltaTime;
        }
    }
}
