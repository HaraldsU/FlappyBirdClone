using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMousePosition : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    // Update is called once per frame
    void Update()
    {
        Debug.Log(mainCamera.ScreenToWorldPoint(Input.mousePosition));
    }
}
