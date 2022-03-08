using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public float playerSpeed = 8.0f;
    public Projectile laserPrefab;
    public bool isLaserOnScreen;
    public int playerTotalHealth = 100;
    public int playerCurrentHealth;

    public HealthBar playerHealthBar;

    public AmmoBar ammoBar;
    public int maximumAmmo = 5; // to be changed later
    public int currentAmmo;
    public float reloadTime = 3.0f;
    private bool isReloading = false;

    public void Start() {
        this.playerCurrentHealth = playerTotalHealth;
        this.playerHealthBar.SetTotalHealth(playerTotalHealth);
        this.currentAmmo = this.maximumAmmo;
        this.ammoBar.SetTotalAmmo(maximumAmmo);
    }

    public void Update() {
        if (Input.GetKey(KeyCode.A)) {
            this.transform.position += Vector3.left * playerSpeed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.D)) {
            this.transform.position += Vector3.right * playerSpeed * Time.deltaTime;
        }
        if(!isReloading) {
            if(currentAmmo > 0) {
                if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
                    Shoot();
                }
            } else {
                StartCoroutine(Reload());
            }
        }
    }

    private void Shoot() {
        if (!isLaserOnScreen) {
            Projectile projectile = Instantiate(this.laserPrefab, this.transform.position, Quaternion.identity);
            projectile.projectileDestroyed += LaserDestroyed;
            isLaserOnScreen = true;
            currentAmmo--;
            ammoBar.SubstractAmmo();
        }
    }

    private IEnumerator Reload() {
        isReloading = true;
        yield return new WaitForSeconds(1.0f);
        WaitForSeconds reloadTick = new WaitForSeconds(reloadTime * 0.1f);
        while(currentAmmo < maximumAmmo) {
            currentAmmo++;
            ammoBar.SetAmmo(currentAmmo);
            yield return reloadTick;
        }
        ammoBar.SetTotalAmmo(maximumAmmo);
        isReloading = false;
    }

    private void LaserDestroyed() {
        isLaserOnScreen = false;
    }

    public void takeDamage(int damage) {
        this.playerCurrentHealth -= damage;
        this.playerHealthBar.SetHealth(playerCurrentHealth);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Missile")) {
            takeDamage(20); // this will be changed to dynamic values based on invader
        }
    }
}
