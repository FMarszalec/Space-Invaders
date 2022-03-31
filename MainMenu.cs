using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayClassic() {
        SceneManager.LoadScene("SpaceInvaders");
    }

    public void PlayRoguelite() {
        SceneManager.LoadScene("Map");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
