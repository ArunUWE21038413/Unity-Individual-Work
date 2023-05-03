using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PowerUp : MonoBehaviour
{
    Input_Manager input_Manager;
    PlayerLocomotion playerLocomotion;
    public GameObject player;
    public GameObject pickupEffect;
    public float multiplier = 1.4f;

    [SerializeField]
    private int speed = 5;
    private int speedBoost = 5;

    public bool speedUp;

    private void Awake()
    {
        playerLocomotion = player.GetComponent<PlayerLocomotion>();
        input_Manager = GetComponent<Input_Manager>();
    }

    public void SpeedUpEnabled()
    {
        speedUp = true;
        speed *= speedBoost;
        StartCoroutine(SpeedUpDisableRoutine());
    }

    IEnumerator SpeedUpDisableRoutine()
    {
        yield return new WaitForSeconds(3.0f);

        speed /= speedBoost;
    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Instantiate(pickupEffect, transform.position, transform.rotation);
            pickupEffect.GetComponent<ParticleSystem>().Play();

            // Apply effect to the player
            player.transform.localScale *= multiplier;

            playerLocomotion.movementSpeed *= multiplier;

            //Remove effect to the player
            Destroy(pickupEffect.GetComponent<ParticleSystem>());
            Destroy(gameObject);



            Debug.Log("PowerUp picked up!");
        }
    }


}
