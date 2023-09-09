using UnityEngine;

public class PipeController : MonoBehaviour
{
    public float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
        if (transform.position.x < -1)
        {
            Destroy(gameObject);
        }
    }
}
