using UnityEngine;
using UnityEngine.AI;

public class MarkerControl : MonoBehaviour {
    public Camera cam;
    public NavMeshAgent agent;

    void Start() {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit)) {
               agent.SetDestination(hit.point);
            }
        }
    }
}
