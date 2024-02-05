using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class SimpleFixForUI : MonoBehaviour
{
    // [SerializeField] Animator transition;

    void Start() {
        // StartCoroutine(PlayAnim());
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(2);
        }    
    }

    public void MainMenu() {
        SceneManager.LoadScene(0);
    }
    
    public void QuitGame() {
        Application.Quit();
    }

    // IEnumerator PlayAnim() {
    //     transition.SetTrigger("Start");
    //     yield return new WaitForSeconds(1);
    // }
}
