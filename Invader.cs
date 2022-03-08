using UnityEngine;

public class Invader : MonoBehaviour {
    public Sprite[] animationSprites;
    public float animationTime = 1.0f;
    public System.Action invaderKilled;

    private SpriteRenderer spriteRenderer;
    private int animationFrame;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Start() {
        InvokeRepeating(nameof(AnimateSprite), this.animationTime, this.animationTime);
    }

    private void AnimateSprite() {
        animationFrame++;

        if (animationFrame >= this.animationSprites.Length) {
            animationFrame = 0;
        }

        spriteRenderer.sprite = this.animationSprites[animationFrame];
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Laser")) {
            this.invaderKilled.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
