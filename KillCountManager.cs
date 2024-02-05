using UnityEngine;
using UnityEngine.SceneManagement;

public class KillCountManager : MonoBehaviour
{
    public int requiredKillCount = 10;  // Set the required number of kills
    private int currentKillCount = 0;

    // Call this method when an enemy is killed
    public void EnemyKilled(int sceneIndex)
    {
        currentKillCount++;

        // Check if the required number of kills is reached
        if (currentKillCount >= requiredKillCount)
        {
            FinishGame(sceneIndex);
        }
    }

    // Call this method to finish the game
    private void FinishGame(int sceneIndex)
    {
        // You can add any additional logic here before transitioning to the next scene
        Debug.Log("Game Finished! Transitioning to the next scene.");

        // Release the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        // Load the next scene
        SceneManager.LoadScene(sceneIndex);
    }
}
