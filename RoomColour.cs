using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomColour : MonoBehaviour {
    bool mouseOver = false;
    public Component[] walls;
    public bool visited = false;
    public bool canBeVisited = false;

    private RoomTemplates templates;

    void Start() {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
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
            for (int i = 0; i < templates.rooms.Count; i++) {
                if(gameObject.name.Contains("Right")) {
                    if(templates.rooms[i].name.Contains("Left") && templates.rooms[i].transform.position.x == transform.position.x + 100 && templates.rooms[i].transform.position.y == transform.position.y) {
                        templates.rooms[i].GetComponent<RoomColour>().canBeVisited = true;
                    }
                }
                if(gameObject.name.Contains("Top")) {
                    if(templates.rooms[i].name.Contains("Bot") && templates.rooms[i].transform.position.y == transform.position.y + 100 && templates.rooms[i].transform.position.x == transform.position.x) {
                        templates.rooms[i].GetComponent<RoomColour>().canBeVisited = true;
                    }
                }
                if(gameObject.name.Contains("Bot")) {
                    if(templates.rooms[i].name.Contains("Top") && templates.rooms[i].transform.position.y == transform.position.y - 100 && templates.rooms[i].transform.position.x == transform.position.x) {
                        templates.rooms[i].GetComponent<RoomColour>().canBeVisited = true;
                    }
                }
                if(gameObject.name.Contains("Left")) {
                    if(templates.rooms[i].name.Contains("Right") && templates.rooms[i].transform.position.x == transform.position.x - 100 && templates.rooms[i].transform.position.y == transform.position.y) {
                        templates.rooms[i].GetComponent<RoomColour>().canBeVisited = true;
                    }
                }
            }
        }
	}
}
