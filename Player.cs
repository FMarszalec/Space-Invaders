using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    public float playerSpeed = 8.0f;
    public Projectile laserPrefab;
    public bool isLaserOnScreen;
    public int playerTotalHealth = GameManager.playerTotalHealthManager;
    public int playerCurrentHealth;

    public HealthBar playerHealthBar;

    public AmmoBar ammoBar;
    public int maximumAmmo = GameManager.maximumAmmoManager; // to be changed later
    public int currentAmmo;
    public float reloadTime = 3.0f;
    private bool isReloading = false;

    public void Start() {
        this.playerCurrentHealth = GameManager.playerCurrentHealthManager;
        this.playerHealthBar.SetTotalHealth(playerTotalHealth);
        this.playerHealthBar.SetHealth(playerCurrentHealth);
        this.currentAmmo = this.maximumAmmo;
        this.ammoBar.SetTotalAmmo(maximumAmmo);
        if (GameManager.upgradesAcquired.Contains("Red")) {
            this.playerSpeed *= 2;
        }
        if (GameManager.upgradesAcquired.Contains("Blue")) {
            this.reloadTime = 1.0f;
        }
    }

    public void Update() {
        if (Input.GetKey(KeyCode.A)) {
            this.transform.position += Vector3.left * playerSpeed * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.D)) {
            this.transform.position += Vector3.right * playerSpeed * Time.deltaTime;
        }
        if(!isReloading) {
            if(currentAmmo > 0) {
                if(Input.GetKeyDown(KeyCode.Space)) {
                    Shoot();
                }
            } else {
                StartCoroutine(Reload());
            }
        }
        if (Input.GetKey(KeyCode.W)) {
            SceneManager.LoadScene("Map");
        }
        if (Input.GetKey(KeyCode.Q)) {
            takeDamage(1);
        }
    }

    private void Shoot() {
        if (!isLaserOnScreen && !GameManager.upgradesAcquired.Contains("Yellow")) {
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
        GameManager.playerCurrentHealthManager -= damage;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Missile")) {
            takeDamage(20); // this will be changed to dynamic values based on invader
        }
    }
}
