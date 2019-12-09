using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
	public float multiplier = 1f;
	
	public GameObject pickupEffect;

	
	void OnTriggerEnter (Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Pickup(other);
		}
	}

	void Pickup(Collider player)
	{
		Instantiate(pickupEffect, transform.position, transform.rotation);

		player.transform.localScale *= multiplier;

		Destroy(gameObject);
	}
}