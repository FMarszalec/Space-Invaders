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
	public GameObject fight;

	void Update(){

		if(waitTime <= 0 && spawnedBoss == false){
			for (int i = 0; i < rooms.Count; i++) {
				if (i < 5) {
					rooms[i].GetComponent<RoomColour>().canBeVisited = true;
					if(i != 0) {
						Instantiate(fight, rooms[i].transform.position, Quaternion.identity);
					}
				}

				if(i == rooms.Count-1){
					Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
					spawnedBoss = true;
				}

				random = Random.Range(0,10);

				if(random < 2 && i != rooms.Count-1 && i >=5) {
					Instantiate(health, rooms[i].transform.position, Quaternion.identity);
				}

				if(random >= 2 && random <= 6 && i != rooms.Count-1 && i >= 5) {
					Instantiate(fight, rooms[i].transform.position, Quaternion.identity);
				}

				if(random > 6 && i != rooms.Count-1 && i >= 5) {
					Instantiate(ammo, rooms[i].transform.position, Quaternion.identity);
				}
			}
		} else {
			waitTime -= Time.deltaTime;
		}
	} 
}