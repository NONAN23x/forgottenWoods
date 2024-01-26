using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class PlayerManager : MonoBehaviour
{
    // Creating up quick and easy drag and drop references
    [SerializeField] float hitPoints = 100f;
    [SerializeField] PlayableDirector timeline;
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] GameObject gunReticleUI;
    [SerializeField] GameObject ammoDisplay;

    [SerializeField] AudioSource audioSource;

    bool isMoving = false;
    float movementThreshold = 0.2f;
    private CharacterController characterController;
    float stopDelay = 0.5f;
    NotificationsManager notifyToast;

    // Cuz why not?
    bool captureMouse = false;

    // Function is called before the first frame update
    void Start() {

        notifyToast = FindAnyObjectByType<NotificationsManager>();

        // Capture the mouse screen and hide it.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Check if save data exists
        bool saveDataExists = CheckpointManager.CheckSavedDataExists();

        if (saveDataExists)
        {
            // Load saved player data
            CheckpointManager.LoadPlayerData(out Vector3 savedLocation, out bool checkpointReached);

            // Set player position to the saved location
            GameObject player = GameObject.FindGameObjectWithTag("Player");
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

        characterController = GetComponent<CharacterController>();

    }

    void Update()
    {
        ProcessDataFormat();
        HandlePauseScreen();
        HandleFootSteps();
        DebugToastNotifications();
    }

    private void DebugToastNotifications() {
        if (Input.GetKeyDown(KeyCode.L)) {
            notifyToast.SendToastNotification("This is a test Notification for a Big L");
        }
    }

    private void HandleFootSteps()
    {
         bool shouldBeMoving = IsCharacterMoving();

        // Play or stop the footsteps audio based on the movement state
        if (shouldBeMoving && !audioSource.isPlaying)
        {
            audioSource.Play();
            isMoving = true;
        }
        else if (!shouldBeMoving && isMoving)
        {
            StartCoroutine(StopFootstepsAudioDelayed());
            isMoving = false;
        }
    }

    private bool IsCharacterMoving() {
        return characterController.velocity.magnitude > movementThreshold;
    }

    IEnumerator StopFootstepsAudioDelayed()
    {
        // Delay stopping the footsteps audio to prevent immediate cutoff
        yield return new WaitForSeconds(stopDelay);
        audioSource.Stop();
    }


    public void TakeDamage(float damage) {
        hitPoints -= damage;
        if (hitPoints<=0) {
            GetComponent<DeathHandler>().HandleDeath();
        }
    }


    private void ProcessDataFormat()
    {
        if (Input.GetKeyDown(KeyCode.I)) {
            PlayerPrefs.DeleteAll();
            Debug.Log("Flushed Memory");
        }
    }

    void HandlePauseScreen() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            
           // Capture Mouse 
           Cursor.lockState = CursorLockMode.None;
           Cursor.visible = true;

           // Activate the Pause Menu UI
           pauseCanvas.SetActive(true);
           gunReticleUI.SetActive(false);
           ammoDisplay.SetActive(false);

           // ga.SetActive(false);
           // Weapon could still be active and process logic,
           // so disable it when the game is paused.
           // Also we need to stop the time.
           FindObjectOfType<WeaponSwitcher>().enabled = false;
           FindObjectOfType<Weapon>().enabled = false;
           FindObjectOfType<WeaponZoom>().enabled = false;
        //    FindObjectOfType<AudioManager>().Stop("backgroundTheme");
        //    FindObjectOfType<FirstPersonController>().enabled = false;
           Time.timeScale = 0.0f;
        }   
    }

    //Dont know why is this here but it needs to stay here :)
    private void UpdateMouseCapture()
    {
        Cursor.lockState = captureMouse ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !captureMouse;
    }

    IEnumerator SetPlayerPositionDelayed(Vector3 savedLocation, GameObject player)
    {
        // Introduce a slight delay before setting the player's position
        yield return null;

        // Set the player's position
        player.transform.position = savedLocation;
    }
}
