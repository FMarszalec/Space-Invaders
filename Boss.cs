using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {
    public int bossHealth = 300;
    public int bossCurrentHealth;
    public HealthBar bossHealthBar;
    void Start() {
        this.bossCurrentHealth = bossHealth;
        this.bossHealthBar.SetTotalHealth(bossHealth);
        this.bossHealthBar.SetHealth(bossCurrentHealth);
    }

    void Update() {
        
    }

    public void takeDamage(int damage) {
        this.bossCurrentHealth -= damage;
        this.bossHealthBar.SetHealth(bossCurrentHealth);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser")) {
            takeDamage(20);
        }
    }
}
