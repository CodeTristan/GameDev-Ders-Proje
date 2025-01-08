using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveExit : MonoBehaviour
{
    public Light sun;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            sun.transform.rotation = Quaternion.Euler(0f, sun.transform.rotation.eulerAngles.y, sun.transform.rotation.eulerAngles.z);
        }
    }
}
