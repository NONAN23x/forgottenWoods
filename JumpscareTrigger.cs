using UnityEngine;
using UnityEngine.Playables;

public class JumpscareTrigger : MonoBehaviour
{
    public string jumpscareID;  // Unique identifier for this jumpscare trigger
    public PlayableDirector timeline;

    private bool hasPlayed;

    void Start()
    {
        // Construct a unique PlayerPrefs key based on the jumpscareID
        string playerPrefKey = "JumpscarePlayed_" + jumpscareID;

        hasPlayed = PlayerPrefs.GetInt(playerPrefKey, 0) == 1;

        // If the timeline has already played, disable the trigger collider
        if (hasPlayed)
        {
            Collider collider = GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Construct a unique PlayerPrefs key based on the jumpscareID
        string playerPrefKey = "JumpscarePlayed_" + jumpscareID;

        if (!hasPlayed && other.CompareTag("Player"))
        {
            // Trigger jumpscare timeline when player enters the trigger zone
            PlayJumpscareTimeline();

            // Set the flag to indicate that the timeline has been played
            PlayerPrefs.SetInt(playerPrefKey, 1);
            hasPlayed = true;

            // Optionally, disable the trigger collider to prevent repeated triggers
            Collider collider = GetComponent<Collider>();
            if (collider != null)
            {
                collider.enabled = false;
            }
        }
    }

    void PlayJumpscareTimeline()
    {
        if (timeline != null)
        {
            timeline.Play(); // Play the timeline
        }
    }
}
