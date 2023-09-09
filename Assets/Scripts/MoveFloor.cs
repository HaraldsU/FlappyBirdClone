using System;
using UnityEngine;
using static Unity.Collections.AllocatorManager;

public class MoveFloor : MonoBehaviour
{
    [Range(0f, 5f)]
    public float speed = 1f;
    private float offset;
    private Material mat;
    public UnityEngine.UI.Image black;
    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }
    void Update()
    {
        offset += (Time.deltaTime * speed) / 10f;
        mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
