using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveEntrance : MonoBehaviour
{
    public Light sun;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            sun.transform.rotation = Quaternion.Euler(200f, sun.transform.rotation.eulerAngles.y, sun.transform.rotation.eulerAngles.z);
        }
    }


}
