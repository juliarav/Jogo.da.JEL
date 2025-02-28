using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    // Start is called before the first frame update
    private void FixedUpdate () 
    {
        transform.position = Vector2.Lerp (transform.position, player.position, 0.1f);
    }
}
