using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    [SerializeField] private Transform preRooom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraController cam;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.transform.position.x < transform.position.x)
                cam.moveToNewRoom(nextRoom);
            else
                cam.moveToNewRoom(preRooom);
        }
    }
}
