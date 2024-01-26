using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using cowsins;

public class LevelManager : MonoBehaviour
{
    [SerializeField] PlayableDirector timeline;
    [SerializeField] private Transform player;

    [SerializeField] Animator transition;

    NotificationsManager notifyToast;

    // private void Awake() {
    //     CheckpointManager.LoadPlayerData(out Vector3 savedLocation, out bool checkpointReached);
    //     player.transform.position = savedLocation;
    // }

    void Awake () {
        QualitySettings.vSyncCount = 1;
        // Application.targetFrameRate = 75;

    }

    private void Start() {

        notifyToast = FindObjectOfType<NotificationsManager>();
        DontDestroyOnLoad(gameObject);

        bool saveDataExists = CheckpointManager.CheckSavedDataExists();
        if (saveDataExists)
        {
            // Load saved player data
            CheckpointManager.LoadPlayerData(out Vector3 savedLocation, out bool checkpointReached);

            // Set player position to the saved location
            if (player != null)
            {
                Debug.Log("Player has been moved to previous Location");
                // player.transform.position = savedLocation;
                StartCoroutine(SetPlayerPositionDelayed(savedLocation, player));

            }
            
        } else {
            timeline.Play();
            Debug.Log("SAVED DATA NOT FOUND!");
        }
    }


     IEnumerator SetPlayerPositionDelayed(Vector3 savedLocation, Transform player)
    {
        // Introduce a slight delay before setting the player's position
        yield return null;

        // Set the player's position
        player.transform.position = savedLocation;
    }

    public void Continue() {

        // Cuz I need to make sure that game is not frozen, duh!
        Time.timeScale = 1.0f;
        bool savedDataExists = CheckpointManager.CheckSavedDataExists();
        if (!savedDataExists) {
            Debug.Log("Cannot Continue.");
        } else {
            StartCoroutine(PlayAnim());
            SceneManager.LoadScene(1);
        }
    }
    
    private void ProcessDataFormat()
    {
        if (Input.GetKeyDown(KeyCode.I)) {
            PlayerPrefs.DeleteAll();
            Debug.Log("Flushed Memory");
        }
    }

    private void DebugToastNotifications() {
        if (Input.GetKeyDown(KeyCode.L)) {
            notifyToast.SendToastNotification("This is a test Notification for a Big L");
        }
    }

    public void NewGame() {
        // TODO: Clear out PlayerPrefs Data for Checkpoint and start the next scene
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(1);
        Time.timeScale = 1;
    }

    public void ReloadGame() {
        Time.timeScale = 1;
        Debug.Log("Reloaded Scene!");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Continue();
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
    }
    
    public void QuitGame() {
        Application.Quit();
    }

    IEnumerator PlayAnim() {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
    }

    public void ArenaOne () {
        SceneManager.LoadScene(3);
    }

    public void ArenaTwo () {
        SceneManager.LoadScene(4);
    }
    
    public void ArenaOneHard () {
        SceneManager.LoadScene(5);
    }

    public void ArenaTwoHard () {
        SceneManager.LoadScene(6);
    }
    
}
