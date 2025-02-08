using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 4.0f;
    private Rigidbody2D rb;
    [SerializeField] private Vector2 dir;
    public bool hiding = false;
    public bool minigaming = false;
    public bool scissors, item2, item3, item4, item5 = false;
    [SerializeField] GameObject scissorsHitbox;
    [SerializeField] GameObject scissorsPopup;
    private SpriteRenderer sr;
    private int intdirection = 0;
    [SerializeField] private Sprite[] sprites;

    private float animationSpeed = 0.15f; // Time per frame
    private int frameIndex = 0; // Tracks animation frame

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Movement Logic
        if (!hiding && !minigaming)
        {
            dir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            dir = Vector3.Normalize(dir);
            rb.velocity = dir * speed;

            UpdateDirection();
            AnimateSprite();
        }
    }

    private void UpdateDirection()
    {
        if (dir.magnitude > 0) // Only update direction if moving
        {
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                intdirection = (dir.x > 0) ? 2 : 1; // Right = 2, Left = 1
            }
            else
            {
                intdirection = (dir.y > 0) ? 3 : 0; // Up = 3, Down = 0
            }
        }
    }

    private void AnimateSprite()
    {
        if (dir.magnitude > 0) // Only animate when moving
        {
            frameIndex = (int)(Time.time / animationSpeed) % 4; // Loops 0-3
            int spriteIndex = intdirection * 4 + frameIndex; // Selects the correct sprite
            sr.sprite = sprites[spriteIndex];
        }else{
            if(intdirection == 2){
                frameIndex = (int)(Time.time / (animationSpeed * 4)) % 4; // Loops 0-3
                int spriteIndex = 28 + frameIndex; // Selects the correct sprite
                sr.sprite = sprites[spriteIndex];
            }else if(intdirection == 1){
                frameIndex = (int)(Time.time / (animationSpeed * 4)) % 4; // Loops 0-3
                int spriteIndex = 24 + frameIndex; // Selects the correct sprite
                sr.sprite = sprites[spriteIndex];
            }else if(intdirection == 0){
                frameIndex = (int)(Time.time / (animationSpeed * 4)) % 4; // Loops 0-3
                int spriteIndex = 16 + frameIndex; // Selects the correct sprite
                sr.sprite = sprites[spriteIndex];
            }else if(intdirection == 3){
                frameIndex = (int)(Time.time / (animationSpeed * 4)) % 4; // Loops 0-3
                int spriteIndex = 20 + frameIndex; // Selects the correct sprite
                sr.sprite = sprites[spriteIndex];
            }
        }
    }

    public void GainObject(string item)
    {
        minigaming = false;
        if (item == "Scissors")
        {
            Destroy(scissorsHitbox);
            Destroy(scissorsPopup);
            scissors = true;
        }
    }
}
