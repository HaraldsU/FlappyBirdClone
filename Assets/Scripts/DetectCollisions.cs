using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    //private int score = 0;
    private bool isColliding = false;
    public GameManager gameManager;
    public AudioClip dieSound;
    public GameObject pipe;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //if (isColliding)
        //{
        //    gameManager.GameOver();
        //}
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Score"))
        {
            gameManager.Score();
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Ceiling"))
        {
            //Debug.Log(collision.gameObject.name);
            isColliding = true;
            gameManager.GameOver();
        }
        if (collision.gameObject.CompareTag("Pipe"))
        {
            if (pipe)
            {
                AudioSource pipeAudioSource = pipe.GetComponent<AudioSource>();
                pipeAudioSource.clip = dieSound;
                pipeAudioSource.Play();
            }
        }
    }
}
