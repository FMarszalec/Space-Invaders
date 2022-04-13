using UnityEngine;
using UnityEngine.SceneManagement;

public class Invaders : MonoBehaviour {
    public int rows = 5;
    public int columns = 11;
    public Invader[] prefabs;
    public AnimationCurve invaderSpeed;
    public Projectile invaderAttackPrefab;
    public int numberKilled { get; private set; }
    public int totalInvaders => this.rows * this.columns;
    public int aliveInvaders => this.totalInvaders - this.numberKilled;
    public float percentKilled => (float)this.numberKilled / (float)this.totalInvaders;
    public float invaderAttackRate = 1.0f;

    private Vector3 direction = Vector2.right;

    private void Awake() {
        for (int i = 0; i < rows; i++) {
            float width = 2.0f * (this.columns - 1);
            float height = 2.0f * (this.rows - 1);

            Vector3 center = new Vector2(-width / 2, -height / 2);
            Vector3 rowPosition = new Vector3(center.x, center.y + (i * 2.0f), 0.0f);

            for (int j = 0; j < columns; j++) {
                Invader invader = Instantiate(this.prefabs[i], this.transform);
                invader.invaderKilled += InvaderKilled;
                Vector3 position = rowPosition;
                position.x += j * 2.0f;
                invader.transform.localPosition = position;
            }
        }
    }

    private void Start() {
        InvokeRepeating(nameof(InvaderAttack), this.invaderAttackRate, this.invaderAttackRate);
    }

    private void Update() {
        this.transform.position += direction * this.invaderSpeed.Evaluate(this.percentKilled) * Time.deltaTime;
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach(Transform invader in this.transform) {
            if (!invader.gameObject.activeInHierarchy) {
                continue;
            }
            if(direction == Vector3.right && invader.position.x >= (rightEdge.x - 1.0f)) {
                AdvanceRow();
            } else if (direction == Vector3.left && invader.position.x <= (leftEdge.x + 1.0f)) {
                AdvanceRow();
            }
        }

        if(aliveInvaders <= 0) {
            SceneManager.LoadScene("Map");
        }
    }

    private void AdvanceRow() {
        direction.x *= -1.0f;
        Vector3 position = this.transform.position;
        position.y -= 1.0f;
        this.transform.position = position;
    }

    private void InvaderKilled() {
        this.numberKilled++;
    }

    private void InvaderAttack() {
        foreach(Transform invader in this.transform) {
            if (!invader.gameObject.activeInHierarchy) {
                continue;
            }
            if (Random.value < (1.0f / (float)this.aliveInvaders)) {
                Instantiate(this.invaderAttackPrefab, invader.position, Quaternion.identity);
                break;
            }

        }
    }

}
