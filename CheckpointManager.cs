using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    private const string PlayerPrefLocationKey = "PlayerLocation";
    private const string PlayerPrefCheckpointKey = "PlayerCheckpointReached";

    // Save player location and checkpoint reached status
    public static void SavePlayerData(Vector3 location, bool checkpointReached)
    {
        PlayerPrefs.SetFloat(PlayerPrefLocationKey + "X", location.x);
        PlayerPrefs.SetFloat(PlayerPrefLocationKey + "Y", location.y);
        PlayerPrefs.SetFloat(PlayerPrefLocationKey + "Z", location.z);
        PlayerPrefs.SetInt(PlayerPrefCheckpointKey, checkpointReached ? 1 : 0);
        PlayerPrefs.Save();
    }

    // Load player data (location and checkpoint reached status)
    public static void LoadPlayerData(out Vector3 location, out bool checkpointReached)
    {
        float x = PlayerPrefs.GetFloat(PlayerPrefLocationKey + "X");
        float y = PlayerPrefs.GetFloat(PlayerPrefLocationKey + "Y");
        float z = PlayerPrefs.GetFloat(PlayerPrefLocationKey + "Z");
        location = new Vector3(x, y, z);

        int checkpoint = PlayerPrefs.GetInt(PlayerPrefCheckpointKey);
        checkpointReached = checkpoint == 1;
    }

    public static bool CheckSavedDataExists() {

        // Check if the PlayerPrefs for saved data exist
        return PlayerPrefs.HasKey("PlayerLocationX") &&
               PlayerPrefs.HasKey("PlayerLocationY") &&
               PlayerPrefs.HasKey("PlayerLocationZ");
    }
}
