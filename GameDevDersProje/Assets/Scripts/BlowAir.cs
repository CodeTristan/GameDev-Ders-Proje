using System;
using UnityEngine;

public class BlowAir : MonoBehaviour
{
    [Header("References")]
    public Transform cam;
    public Transform aimPoint;
    public ParticleSystem windParticle;

    [Header("Settings")]
    public float coolDown;
    public int blowDuration;
    public float pushForce;

    [Header("Blow")]
    public KeyCode blowKey = KeyCode.Mouse1;

    bool isBlowing = false;
    float blowTimer = 0f;

    bool isOnCooldown = false;
    float cooldownTimer = 0f;

    void Start()
    {
        windParticle.Stop();    
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isOnCooldown && !isBlowing)
        {
            StartWindEffect();
        }

        else if(Input.GetMouseButtonUp(1) && isBlowing)
        {
            StopWindEffect();
        }

        if (isBlowing)
        {
            blowTimer += Time.deltaTime;
            if (blowTimer >= blowDuration)
            {
                StopWindEffect();
                StartCooldown();
            }
        }

        if (isOnCooldown)
        {
            cooldownTimer += Time.deltaTime;
            // Debug.Log(cooldownTimer);
            if (cooldownTimer >= blowDuration)
            {
                isOnCooldown = false; 
                cooldownTimer = 0f;
            }
        }

        if (isBlowing && windParticle != null)
        {
            UpdateWindPosition();
        }
    }

    private void StartWindEffect()
    {
        if (windParticle != null)
        {
            windParticle.Play(); 
            isBlowing = true; 
            blowTimer = 0f;
        }
    }

    private void StopWindEffect()
    {
        if (windParticle != null)
        {
            windParticle.Stop();
            isBlowing = false;
        }
    }

    private void StartCooldown()
    {
        isOnCooldown = true; 
        cooldownTimer = 0f;
    }

    private void UpdateWindPosition()
    {
        if (cam != null && windParticle != null)
        {
            Vector3 offsetPosition = cam.position + cam.forward - (cam.up * 0.7f);


            windParticle.transform.position = offsetPosition;
            windParticle.transform.rotation = cam.rotation;
        }
    }
}
