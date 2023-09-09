using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject pipePrefab;
    public float interval = 3f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnPipe", interval, interval);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnPipe()
    {
        float randomY = Random.Range(.84f, 1.78f);
        Vector3 spawnPos = new Vector2(2.5f, randomY);

        Instantiate(pipePrefab, spawnPos, Quaternion.identity);
    }
    public void StopInvoke()
    {
        CancelInvoke("SpawnPipe");
    }
}
