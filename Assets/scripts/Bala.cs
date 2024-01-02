using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    public GameObject particulas;
	private void OnTriggerEnter(Collider other)
	{
		Destroy(Instantiate(particulas, transform.position, transform.rotation), 5);
		Destroy(gameObject);
	}
}
