using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private Animator transitionAnim;
    public float transitionTime = 1f;

    void Awake() {
        transitionAnim = GetComponent<Animator>();
    }
    public void LoadNextScene() {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadPreviousScene() {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
    }

    public void QuitGame() {
        Application.Quit();
    }

    IEnumerator LoadLevel(int levelIndex) {
        transitionAnim.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
