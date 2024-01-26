using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResolutionHandler : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown; // Reference to the resolution TextMeshPro Dropdown object
    public TMP_Dropdown fullscreenDropdown; // Reference to the fullscreen TextMeshPro Dropdown object

    Resolution[] resolutions;

    void Start()
    {
        // Get available screen resolutions and populate the resolution dropdown options
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        fullscreenDropdown.ClearOptions();

        foreach (Resolution res in resolutions)
        {
            resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(res.width + "x" + res.height));
        }

        // Populate the fullscreen dropdown options
        fullscreenDropdown.options.Add(new TMP_Dropdown.OptionData("Fullscreen"));
        fullscreenDropdown.options.Add(new TMP_Dropdown.OptionData("Windowed"));

        // Add listeners for when the value of each dropdown changes
        resolutionDropdown.onValueChanged.AddListener(delegate {
            ResolutionDropdownValueChanged(resolutionDropdown);
        });

        fullscreenDropdown.onValueChanged.AddListener(delegate {
            FullscreenDropdownValueChanged(fullscreenDropdown);
        });
    }

    void ResolutionDropdownValueChanged(TMP_Dropdown change)
    {
        int selectedIndex = change.value;
        Resolution selectedResolution = resolutions[selectedIndex];

        // Apply the selected resolution
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, GetFullscreenMode());
    }

    void FullscreenDropdownValueChanged(TMP_Dropdown change)
    {
        // Apply fullscreen or windowed mode based on the fullscreen dropdown selection
        Screen.SetResolution(Screen.width, Screen.height, GetFullscreenMode());
    }

    FullScreenMode GetFullscreenMode()
    {
        // Determine the selected fullscreen mode from the dropdown value
        return (fullscreenDropdown.value == 0) ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
    }
}
