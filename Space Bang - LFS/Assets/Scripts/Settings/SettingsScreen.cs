using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingsScreen : MonoBehaviour
{
    #region Declarations
    public Toggle fullscreenTog;
    public List<ResItem> resolutions = new List<ResItem>();
    private int selectedResolution;
    public TMP_Text resolutionLabel;
    #endregion

    void Start()
    {
        fullscreenTog.isOn = Screen.fullScreen;

        bool foundRes = false;
        for (int i = 0; i < resolutions.Count; i++)
        {
            // Search for player resolution and set it to selected
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundRes = true;

                selectedResolution = i;
                UpdateResLabel();
            }
        }

        // Add player resolution if it doesn't exist
        if (!foundRes)
        {
            ResItem newRes = new ResItem();
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;

            resolutions.Add(newRes);
            selectedResolution = resolutions.Count - 1;

            UpdateResLabel();
        }

    }

    void Update()
    {
        // After pressing Escape Key, the game returns to the previous scene
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.sceneCount > 1)
        {
            SceneManager.UnloadSceneAsync("SettingsMenu");
        }
    }

    #region Resolution
    // When the player searches for a resolution by clicking the left arrow
    public void ResLeft()
    {
        selectedResolution--;
        if (selectedResolution < 0)
        {
            selectedResolution = 0; // Or to infinite scroll mode: selectedResolution = count-1
        }
        UpdateResLabel();
    }

    // When the player searches for a resolution by clicking the right arrow
    public void ResRight()
    {
        selectedResolution++;
        if (selectedResolution > resolutions.Count - 1)
        {
            selectedResolution = resolutions.Count - 1; // Or to infinite scroll mode: selectedResolution = 0
        }
        UpdateResLabel();
    }

    // Update the resolution displayed in the label
    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString() + " X "
        + resolutions[selectedResolution].vertical.ToString();
    }

    // Change the screen resolution
    public void ApplyGraphics()
    {
        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, fullscreenTog.isOn);
    }
    #endregion

}

[System.Serializable]
public class ResItem
{
    public int horizontal, vertical;
}
