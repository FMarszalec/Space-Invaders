using UnityEngine;

public class Projectile : MonoBehaviour {
    public Vector3 direction;
    public float projectileSpeed;
    public System.Action projectileDestroyed;

    private void Update() {
        this.transform.position += this.direction * this.projectileSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (this.projectileDestroyed != null) {
            this.projectileDestroyed.Invoke();
        }
        Destroy(this.gameObject);
    }
}
