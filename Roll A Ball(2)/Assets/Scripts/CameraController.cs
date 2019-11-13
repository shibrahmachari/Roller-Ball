using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    private Vector3 offset;
    private string position = "far";

    void Start ()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate ()
    {
        if (position=="far")
            transform.position = player.transform.position + offset;
        if (position == "close")
            transform.position = player.transform.position;
        if (Input.GetKey(KeyCode.C))
            position = "close";
        if (Input.GetKey(KeyCode.F))
            position = "far";
    }
}
