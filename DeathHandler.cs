using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject gunReticleUI;
    [SerializeField] GameObject ammoDisplay;

    public void Resume () {
        Time.timeScale = 1.0f;
        gunReticleUI.SetActive(true);
        ammoDisplay.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        FindObjectOfType<WeaponSwitcher>().enabled = true;
        FindObjectOfType<Weapon>().enabled = true;
        FindObjectOfType<WeaponZoom>().enabled = true;
        FindObjectOfType<AudioManager>().Play("backgroundTheme");
        pauseMenu.SetActive(false);
    }

    public void HandleDeath() {
        gameOverCanvas.SetActive(true);
        gunReticleUI.SetActive(false);
        ammoDisplay.SetActive(false);
        Debug.Log("Im Dying");
        FindObjectOfType<WeaponSwitcher>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0;
    }
}
