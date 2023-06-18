using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;

    public TextMeshProUGUI coinsText;

    public Button retryButton;
    public Button homeButton;
    public Button leaderButton;

    private Player player;
    private Spawner spawner;

    private float score;
    private float highscore;
    public int coins;

    public bool isInvincible;
    private float invincibilityDuration = 10f;
    private float invincibilityTimer;

    private Vector3 initialScale;

    public int boosterCost = 10;

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
        }
        else { 
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();

        initialScale = player.transform.localScale;

        NewGame();
    }

    public void NewGame()
    {
        coins = 0;
        coinsText.text = 0.ToString();
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles) {
            Destroy(obstacle.gameObject);
        }
        score = 0f;
        gameSpeed = initialGameSpeed;
        enabled = true;

        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        homeButton.gameObject.SetActive(false);
        leaderButton.gameObject.SetActive(false);

        UpdateHighscore();
    }

    public void GameOver()
    {
        gameSpeed = 0f;
        enabled = false;

        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        homeButton.gameObject.SetActive(true);
        leaderButton.gameObject.SetActive(true);

        UpdateHighscore();
    }

        public void ActivateInvincibility()
    {
        isInvincible = true;
        invincibilityTimer = invincibilityDuration;

        player.transform.localScale = initialScale * 1.5f;
    }

    private void Update()
    {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D6");

        if (isInvincible)
        {
            invincibilityTimer -= Time.deltaTime;

            if (invincibilityTimer <= 0f)
            {
                isInvincible = false;
                player.transform.localScale = initialScale;
            }
        }
    }

    private void UpdateHighscore()
    {
        highscore = PlayerPrefs.GetFloat("highscore", 0);

        if (score > highscore) {
            highscore = score;
            PlayerPrefs.SetFloat("highscore", highscore);
        }

        highscoreText.text = Mathf.FloorToInt(highscore).ToString("D6");
    }

    public void UseBooster()
    {
        if (coins >= boosterCost) {
            coins -= boosterCost;
            coinsText.text = coins.ToString();

            ActivateInvincibility();
        }
    }

}
