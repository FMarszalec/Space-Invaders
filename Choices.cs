using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Choices : MonoBehaviour {
    private int random;
    private string upgrade;

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

    public void GetRandomUpgrade() {
        random = Random.Range(0, GameManager.upgradesAvailable.Count);
        upgrade = GameManager.upgradesAvailable[random];
        GameManager.upgradesAvailable.RemoveAt(random);
        GameManager.upgradesAcquired.Add(upgrade);
        GameManager.newUpgrade = upgrade;
        SceneManager.LoadScene("Map");
    }
}
