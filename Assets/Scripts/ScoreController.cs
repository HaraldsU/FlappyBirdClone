using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public static int score = 0;
    public TextMeshProUGUI highScoreText;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        GetComponent<TMP_Text>().text = score.ToString();
        if (highScoreText)
        {
            highScoreText.text = $"{PlayerPrefs.GetInt("HighScore")}";
        }
    }
}
