using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Choices : MonoBehaviour {

    public void GetAmmo() {
        GameManager.maximumAmmoManager += 5;
        SceneManager.LoadScene("Map");
    }

    public void RefilFuel() {
        GameManager.currentFuel += 10;
        if(GameManager.currentFuel > GameManager.maxFuel) {
            GameManager.currentFuel = GameManager.maxFuel;
        }
        SceneManager.LoadScene("Map");
    }
}
