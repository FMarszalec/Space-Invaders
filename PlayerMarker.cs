using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMarker : MonoBehaviour {
    public float markerSpeed = 100.0f;
    private Vector3 target;
    private int maxFuel = GameManager.maxFuel; // value can change
    private int currentF;
    public TextMeshProUGUI displayCurrentFuel;
    public TextMeshProUGUI displayMaxFuel;
    public TextMeshProUGUI displayCurrentHealth;
    public TextMeshProUGUI displayMaxHealth;
    public TextMeshProUGUI displayMaxAmmo;
    private Vector3 previousPosition;
    private float distance; 

    void Start() {
        transform.position = GameManager.targetManager;
        previousPosition = GameManager.previousPositionManager;
        GameManager.currentFuel += (int) GameManager.distanceTraveledManager;
        this.displayMaxFuel.text = maxFuel.ToString(); // this is just for testing now
        this.displayMaxHealth.text = GameManager.playerTotalHealthManager.ToString();
        this.displayCurrentHealth.text = GameManager.playerCurrentHealthManager.ToString();
        this.displayMaxAmmo.text = GameManager.maximumAmmoManager.ToString();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
           CastRay();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag != "Fight" && other.tag != "Spawner" && other.tag != "Health" && other.tag != "Special") {
            target = other.transform.position;
            transform.position = target;
            GameManager.targetManager = target;
            distance = Mathf.Sqrt(((previousPosition.x - transform.position.x) * (previousPosition.x - transform.position.x)) + ((previousPosition.y - transform.position.y) * (previousPosition.y - transform.position.y))) / 100;
            distance = Mathf.Ceil(distance);
            GameManager.distanceTraveledManager = distance;
            GameManager.currentFuel -= (int) distance;

            displayCurrentFuel.text = GameManager.currentFuel.ToString();
        }

        if(other.CompareTag("Fight")) {
            Destroy(other.gameObject);
            SceneManager.LoadScene("SpaceInvaders");
        }

        if(other.CompareTag("Health")) {
            Destroy(other.gameObject);
            GameManager.playerCurrentHealthManager += (int) (GameManager.playerTotalHealthManager * 0.3);
            
            if (GameManager.playerCurrentHealthManager > GameManager.playerTotalHealthManager) {
                GameManager.playerCurrentHealthManager = GameManager.playerTotalHealthManager;
            }

            this.displayCurrentHealth.text = GameManager.playerCurrentHealthManager.ToString();
        }

        if(other.CompareTag("Special")) {
            Destroy(other.gameObject);
            SceneManager.LoadScene("Special");
        }
	}

     void CastRay() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit && hit.collider.gameObject.GetComponent<RoomColour>().canBeVisited == true && GameManager.currentFuel > 0) {
            previousPosition = transform.position;
            GameManager.previousPositionManager = transform.position;
            transform.position = hit.collider.gameObject.transform.position;
        }
    }    
}
