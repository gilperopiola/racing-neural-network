using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
    public float moveSpeed = 60f;

    public Sprite rightSprite;
    public Sprite leftSprite;
    public Sprite upSprite;
    public Sprite downSprite;

    public BoxCollider2D rightCol;
    public BoxCollider2D leftCol;
    public BoxCollider2D upCol;
    public BoxCollider2D downCol;

    private Vector2 movement = Vector2.zero;

    private SpriteRenderer sr;
    private Rigidbody2D rb;

    private void Start() {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }


    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);

        if (movement.y > 0.1f) {
            sr.sprite = upSprite;
            HandleColliders(true, false, false, false);
        } else if (movement.y < -0.1f) {
            sr.sprite = downSprite;
            HandleColliders(false, false, true, false);
        }

        if (movement.x > 0.1f) {
            sr.sprite = rightSprite;
            HandleColliders(false, true, false, false);
        } else if (movement.x < -0.1f) {
            sr.sprite = leftSprite;
            HandleColliders(false, false, false, true);
        }
    }

    private void HandleColliders(bool up, bool right, bool down, bool left) {
        upCol.enabled = up;
        rightCol.enabled = right;
        downCol.enabled = down;
        leftCol.enabled = left;
    }

}
