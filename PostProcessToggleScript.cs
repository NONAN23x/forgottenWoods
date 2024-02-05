using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessToggleScript : MonoBehaviour
{
    public PostProcessLayer postProcessLayer;
    private bool isPostProcessingEnabled = true;

    void Update()
    {
        // Toggle post-processing on/off when the 'P' key is pressed
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            isPostProcessingEnabled = !isPostProcessingEnabled;
            TogglePostProcessingEffects();
        }
    }

    void TogglePostProcessingEffects()
    {
        // Enable or disable post-processing based on the boolean value
        postProcessLayer.enabled = isPostProcessingEnabled;

        // Optionally, you can add additional logic here based on the toggle state
    }
}
