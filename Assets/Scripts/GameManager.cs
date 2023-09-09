using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverCanvas;
    public GameObject gameTutorialCanvas;
    public GameObject scoreCanvas;
    public GameObject fadeCanvas;
    public Sprite[] coinSprites = new Sprite[4];

    public GameObject coin;
    public GameObject player;
    public GameObject pipes;
    public GameObject spawnManager;
    public GameObject floor;
    public GameObject backGround;
    public GameObject sparkle;

    public AudioClip scoreAudio;
    public AudioClip hitAudio;
    public AudioClip wingAudio;
    public AudioClip swooshAudio;
    private AudioSource audioSourcePlayer;
    private AudioSource audioSourceGM;

    public Sprite goldCoin;
    public bool isPlayed = false;
    public bool gameStarted = false;
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Sparkle", .5f, .5f);
        gameTutorialCanvas.SetActive(true);
        audioSourcePlayer = player.GetComponent<AudioSource>();
        audioSourceGM = gameObject.GetComponent<AudioSource>();
        pipes.GetComponent<PipeController>().enabled = false;
        spawnManager.GetComponent<SpawnManager>().enabled = false;
        player.GetComponent<Rigidbody2D>().isKinematic = false;
        player.GetComponent<PlayerController>().jumpForce = 0;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            Debug.Log("Bird collided with the floor!");
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }
    public void GameOver()
    {
        player.GetComponent<DetectCollisions>().enabled = false;
        scoreCanvas.active = false;
        gameOver = true;
        if (!isPlayed)
        {
            audioSourcePlayer.clip = hitAudio;
            audioSourcePlayer.Play();
        }
        spawnManager.GetComponent<SpawnManager>().CancelInvoke("SpawnPipe");
        backGround.GetComponent<SpriteRenderer>().sortingOrder = 4;

        PipeController[] objectsWithScript = FindObjectsOfType<PipeController>();
        foreach (PipeController obj in objectsWithScript)
        {
            GameObject pipeBottom = obj.transform.Find("Pipe_Bottom").gameObject;
            GameObject pipeTop = obj.transform.Find("Pipe_Top").gameObject;

            pipeBottom.GetComponent<SpriteRenderer>().sortingOrder = 5;
            pipeTop.GetComponent<SpriteRenderer>().sortingOrder = 5;
            pipeBottom.GetComponent<BoxCollider2D>().enabled = false;
            pipeTop.GetComponent<BoxCollider2D>().enabled = false;
            obj.GetComponent<PipeController>().enabled = false;
        }

        floor.GetComponent<MoveFloor>().enabled = false;
        player.GetComponent<Animator>().enabled = false;

        Rigidbody2D rbPlayer = player.GetComponent<Rigidbody2D>();
        rbPlayer.constraints = RigidbodyConstraints2D.FreezePositionX;
        rbPlayer.drag = 0;

        isPlayed = true;
        gameOverCanvas.SetActive(true);
        if (ScoreController.score >= 20)
        {
            if (backGround.GetComponent<BackgroundController>().isNight == false)
            {
                coin.GetComponent<UnityEngine.UI.Image>().sprite = coinSprites[0];
            }
            else
            {
                coin.GetComponent<UnityEngine.UI.Image>().sprite = coinSprites[2];
            }
            if (ScoreController.score >= 40)
            {
                if (backGround.GetComponent<BackgroundController>().isNight == false)
                {
                    coin.GetComponent<UnityEngine.UI.Image>().sprite = coinSprites[1];
                }
                else
                {
                    coin.GetComponent<UnityEngine.UI.Image>().sprite = coinSprites[3];
                }
            }
        }
    }
    public void ScoreCheck()
    {
        if (ScoreController.score >= 20)
        {
            coin.active = true;
            sparkle.active = true;
        }
    }
    void Sparkle()
    {
        float randX = Random.Range(-322, -158);
        float randY = Random.Range(-155f, 10);
        Vector2 localPosition = new Vector2(randX, randY);
        Vector3 worldPosition = gameOverCanvas.transform.TransformPoint(localPosition);

        sparkle.transform.position = worldPosition;
        sparkle.GetComponent<Animator>().Play("SparkleFadeInNOut", -1, 0f);
    }
    public void ActivateFadeCanvas()
    {
        fadeCanvas.active = true;
    }
    public void TriggerSceneLoad()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void Score()
    {
        ScoreController.score++;
        audioSourcePlayer.clip = scoreAudio;
        audioSourcePlayer.Play();
        CheckHighScore();
    }
    void CheckHighScore()
    {
        if (ScoreController.score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", ScoreController.score);
        }
    }
    public void BirdJump()
    {
        if (gameStarted)
        {
            scoreCanvas.SetActive(true);
            gameTutorialCanvas.GetComponent<PlayableDirector>().Play();
            if (pipes)
            {
                pipes.GetComponent<PipeController>().enabled = true;
            }
            spawnManager.GetComponent<SpawnManager>().enabled = true;
            player.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            player.GetComponent<PlayerController>().jumpForce = player.GetComponent<PlayerController>().jumpForceDefault;
            player.GetComponent<BirdIdleMoving>().enabled = false;
        }
        if (!isPlayed)
        {
            audioSourceGM.clip = wingAudio;
            audioSourceGM.Play();
        }
    }
}
