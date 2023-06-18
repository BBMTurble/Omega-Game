using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    public TextMeshProUGUI highscoreText; 

    void Start()
    {
        float highscore = PlayerPrefs.GetFloat("highscore", 0);
        highscoreText.text = Mathf.FloorToInt(highscore).ToString();
    }
}
