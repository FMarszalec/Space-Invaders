using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static int maxFuel = 20;
    public static int currentFuel = 20;
    public static int playerTotalHealthManager = 100;
    public static int playerCurrentHealthManager = 100;
    public static int maximumAmmoManager = 5;
    public static List<GameObject> roomsManager = new List<GameObject>();
    public static bool roomsSpawned = false;
    public static Vector3 targetManager = new Vector3(0,0,1);
    public static Vector3 previousPositionManager = new Vector3(0,0,1); 
}
