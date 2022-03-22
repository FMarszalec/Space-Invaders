using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour {

	public GameObject[] bottomRooms;
	public GameObject[] topRooms;
	public GameObject[] leftRooms;
	public GameObject[] rightRooms;

	public GameObject closedRoom;

	public List<GameObject> rooms;

	public float waitTime;
	private bool spawnedBoss;
	public GameObject boss;
	private int random;
	public GameObject health;
	public GameObject ammo;

	void Update(){

		if(waitTime <= 0 && spawnedBoss == false){
			for (int i = 0; i < rooms.Count; i++) {
				if(i == rooms.Count-1){
					Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
					spawnedBoss = true;
				}
				random = Random.Range(0,10);
				if(random < 5 && i != rooms.Count-1 && i != 0) {
					Instantiate(health, rooms[i].transform.position, Quaternion.identity);
				}
				if(random > 5 && i != rooms.Count-1 && i != 0) {
					Instantiate(ammo, rooms[i].transform.position, Quaternion.identity);
				}
			}
		} else {
			waitTime -= Time.deltaTime;
		}
	} 
}