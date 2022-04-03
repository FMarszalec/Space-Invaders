using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    public Animator transition;
    
    public void LoadNewScene(int scenePosition) {
        StartCoroutine(LoadLevel(scenePosition));
    }

    IEnumerator LoadLevel(int scenePosition) {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + scenePosition);
    }
}
