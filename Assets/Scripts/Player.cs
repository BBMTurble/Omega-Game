using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController character;

    private Vector3 direction;

    public float gravity = 9.81f * 2f;
    public float jumpForce = 9f;

    private float originalHeight;
    private Vector3 originalCenter;

    private bool isSliding = false;
    private AnimatedSprite animatedSprite;


    private void Awake()
    {
        character = GetComponent<CharacterController>();
        animatedSprite = GetComponent<AnimatedSprite>();
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
        originalHeight = character.height;
        originalCenter = character.center;
    }

    private void Update()
    {
        if (!isSliding)
        {
            direction += Vector3.down * gravity * Time.deltaTime;

            if (character.isGrounded)
            {
                direction = Vector3.down;

                if (Input.GetButton("Jump"))
                {
                    direction = Vector3.up * jumpForce;
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    isSliding = true;
                    direction = Vector3.down;
                    character.height -= 0.6f;
                    character.center = new Vector3(originalCenter.x, -0.4f, originalCenter.z);
                    animatedSprite.SetSliding(true);
                }
            }
        }
        else
        {
            direction = Vector3.down * gravity;

            if (Input.GetKeyUp(KeyCode.S))
            {
                character.height = originalHeight;
                character.center = originalCenter;
                isSliding = false;
                animatedSprite.SetSliding(false);
            }
        }

        character.Move(direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle")) {
            if (!GameManager.Instance.isInvincible) {
                GameManager.Instance.GameOver();
            }
            else {
                Destroy(other.gameObject);
            }
        }
        else if (other.CompareTag("Coin")) {
            GameManager.Instance.coins++;
            GameManager.Instance.coinsText.text = GameManager.Instance.coins.ToString();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Booster")) {
            Destroy(other.gameObject);
            GameManager.Instance.ActivateInvincibility();
        }
    }
}
