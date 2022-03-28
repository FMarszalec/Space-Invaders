using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomColour : MonoBehaviour {
    bool mouseOver = false;
    public Component[] walls;
    public bool visited = false;
    public bool canBeVisited = false;

    void Start() {
        walls = GetComponentsInChildren<SpriteRenderer>();
    }

    void OnMouseEnter() {
        mouseOver = true;
        foreach(SpriteRenderer wall in walls) {
            if (canBeVisited) {
                wall.color = Color.green;
            }
            else wall.color = Color.red;
        }
    }

    void OnMouseExit() {
        mouseOver = false;
        foreach(SpriteRenderer wall in walls) {
            wall.color = Color.white;
        }
    }

    void OnTriggerEnter2D(Collider2D other){
		if(other.tag == "Player") {
            this.visited = true;
        }
	}
}
