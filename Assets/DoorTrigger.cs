using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Animator DoorAnim;
    [SerializeField]
    GameObject door;
   
    bool isOpened = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isOpened == false)
        {
            isOpened = true;
            DoorAnim.SetTrigger("open");
            //door.transform.position += new Vector3(0, 10, 0);
        }
    }
}
