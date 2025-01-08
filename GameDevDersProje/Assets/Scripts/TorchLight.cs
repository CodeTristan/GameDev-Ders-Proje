using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchLight : MonoBehaviour
{
    public Light torchLight;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Fireball")
        {
            torchLight.enabled = true;
        }
    }

    private void Start() {
        torchLight.enabled = false;
    }
}
