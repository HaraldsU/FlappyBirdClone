using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public Sprite[] backgrounds = new Sprite[2];
    private SpriteRenderer backgroundRenderer;
    private int rand;
    public bool isNight;
    // Start is called before the first frame update
    void Start()
    {
        rand = Random.Range(0, 2);
        Debug.Log(rand);
        backgroundRenderer = gameObject.GetComponent<SpriteRenderer>();
        backgroundRenderer.sprite = backgrounds[rand];
        if (rand == 0)
        {
            isNight = false;
        }
        else
        {
            isNight = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
