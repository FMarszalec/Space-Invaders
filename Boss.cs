using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    public int bossHealth = 300;
    public int bossCurrentHealth;
    public HealthBar bossHealthBar;
    private Vector3 direction = Vector2.right;

    void Start() {
        this.bossCurrentHealth = bossHealth;
        this.bossHealthBar.SetTotalHealth(bossHealth);
        this.bossHealthBar.SetHealth(bossCurrentHealth);
    }

    private void Update() {
        this.transform.position += direction * 1 * Time.deltaTime;
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);
        if(direction == Vector3.right && this.transform.position.x >= (rightEdge.x - 10.0f)) {
            AdvanceRow();
        } else if (direction == Vector3.left && this.transform.position.x <= (leftEdge.x + 10.0f)) {
                AdvanceRow();
        }
    }

    private void AdvanceRow() {
        direction.x *= -1.0f;
        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
    }

    public void takeDamage(int damage) {
        int cannonCount = GameObject.FindGameObjectsWithTag("Cannon").Length;

        if(cannonCount * 50 < bossCurrentHealth) {
            this.bossCurrentHealth -= damage;
            this.bossHealthBar.SetHealth(bossCurrentHealth);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser")) {
            takeDamage(20);
        }
    }
}
