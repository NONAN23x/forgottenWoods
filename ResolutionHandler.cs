using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ResolutionHandler : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullscreenToggle;

    private bool isDropdownPopulated = false; // Track if dropdown options are already populated

    void Start()
    {
        LoadSettings(); // Load resolution and fullscreen/windowed settings on start
    }

    void PopulateResolutionsDropdown()
    {
        // Get available resolutions
        Resolution[] resolutions = Screen.resolutions;

        // Clear existing options
        resolutionDropdown.ClearOptions();

        // Create a list of resolution strings in the format "Width x Height"
        var resolutionOptions = new List<string>();
        foreach (var resolution in resolutions)
        {
            resolutionOptions.Add(resolution.width + " x " + resolution.height);
        }

        // Add resolution options to the dropdown
        resolutionDropdown.AddOptions(resolutionOptions);

        // Set the flag to true since the dropdown options are now populated
        isDropdownPopulated = true;
    }

    void LoadSettings()
    {
        // Load resolution and fullscreen/windowed setting from PlayerPrefs
        string savedResolution = PlayerPrefs.GetString("Resolution", "");
        int fullscreenSetting = PlayerPrefs.GetInt("Fullscreen", 1);

        // Check if the dropdown options haven't been populated yet
        if (!isDropdownPopulated)
        {
            PopulateResolutionsDropdown(); // Dynamically populate resolution options
        }

        // Set the dropdown value to the saved resolution if it exists in the dropdown options
        int resolutionIndex = resolutionDropdown.options.FindIndex(option => option.text == savedResolution);
        resolutionDropdown.value = (resolutionIndex != -1) ? resolutionIndex : 0;

        // Set fullscreen/windowed toggle
        fullscreenToggle.isOn = (fullscreenSetting == 1);

        // Apply the loaded settings
        ApplyResolution();
    }

    // Call this method when the user changes the resolution from the dropdown
    public void OnResolutionChanged()
    {
        ApplyResolution();
    }

    // Call this method when the user toggles fullscreen/windowed
    public void OnFullscreenToggle()
    {
        ApplyResolution();
    }

    // Apply the selected resolution and fullscreen/windowed setting
    private void ApplyResolution()
    {
        // Get the selected resolution from the dropdown
        string selectedResolution = resolutionDropdown.options[resolutionDropdown.value].text;

        // Parse the resolution string (assuming it's in the format "Width x Height")
        string[] resolutionParts = selectedResolution.Split('x');
        int width = int.Parse(resolutionParts[0]);
        int height = int.Parse(resolutionParts[1]);

        // Set the resolution
        Screen.SetResolution(width, height, fullscreenToggle.isOn);

        // Save the selected resolution and fullscreen/windowed setting in PlayerPrefs
        PlayerPrefs.SetString("Resolution", selectedResolution);
        PlayerPrefs.SetInt("Fullscreen", fullscreenToggle.isOn ? 1 : 0);
    }
}
