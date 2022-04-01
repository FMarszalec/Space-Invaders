using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMarker : MonoBehaviour {
    public float markerSpeed = 100.0f;
    private Vector3 target;
    private int maxFuel = 20; // value can change
    private int currentFuel;
    public TextMeshProUGUI displayCurrentFuel;
    public TextMeshProUGUI displayMaxFuel;
    private Vector3 previousPosition;
    private float distance;

    void Start() {
        target = transform.position;
        previousPosition = transform.position;
        this.displayMaxFuel.text = maxFuel.ToString(); // this is just for testing now
        this.currentFuel = maxFuel; // this is to counteract the collision with 4 spawners and room when spawned
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
           CastRay();
        }
    }

    void OnTriggerEnter2D(Collider2D other){
		target = other.transform.position;
        transform.position = target;
        distance = Mathf.Sqrt(((previousPosition.x - transform.position.x) * (previousPosition.x - transform.position.x)) + ((previousPosition.y - transform.position.y) * (previousPosition.y - transform.position.y))) / 100;
        distance = Mathf.Ceil(distance);
        currentFuel -= (int) distance;
        displayCurrentFuel.text = currentFuel.ToString();
        // do something in relation to event
        if(other.CompareTag("MapEvent")) {
            Destroy(other.gameObject);
        }
	}

     void CastRay() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit && hit.collider.gameObject.GetComponent<RoomColour>().canBeVisited == true && currentFuel > 0) {
            previousPosition = transform.position;
            transform.position = hit.collider.gameObject.transform.position;
        }
    }    
}
