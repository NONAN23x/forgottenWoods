using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    AmmoType ammoType;

    private void OnTriggerEnter(Collider other)
    {
        ProcessCheckpoint(other);
    }

    private void ProcessCheckpoint(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointManager.SavePlayerData(transform.position, true);
            
            // Destroy the object after saving
            // Destroy(gameObject);
            Debug.Log("DATA IS SAVED");
        }
    }
}
