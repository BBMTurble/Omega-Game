using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] runningSprites;
    public Sprite[] slidingSprites;

    private SpriteRenderer spriteRenderer;

    private int frame;
    private bool isSliding = false;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        Invoke(nameof(Animate), 0f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void Animate()
    {
        if (!isSliding)
        {
            frame++;

            if (frame >= runningSprites.Length)
            {
                frame = 0;
            }

            if (frame >= 0 && frame < runningSprites.Length)
            {
                spriteRenderer.sprite = runningSprites[frame];
            }
        }
        else
        {
            frame++;

            if (frame >= slidingSprites.Length)
            {
                frame = 0;
            }

            if (frame >= 0 && frame < slidingSprites.Length)
            {
                spriteRenderer.sprite = slidingSprites[frame];
            }
        }

        Invoke(nameof(Animate), 1f / GameManager.Instance.gameSpeed);
    }

    public void SetSliding(bool isSliding)
    {
        this.isSliding = isSliding;
        if (!isSliding)
        {
            frame = 0;
        }
    }
}
