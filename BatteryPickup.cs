using Unity.VisualScripting;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] float restoreAngle = 90f;
    [SerializeField] float addIntensity = 1f;

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player") {
            FindObjectOfType<FlashLightSystem>().RestoreLightAngle(restoreAngle);
            FindObjectOfType<FlashLightSystem>().RestoreLightIntensity(addIntensity);

            Destroy(gameObject);
        }
    }
}
