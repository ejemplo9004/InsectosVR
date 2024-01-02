using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistola : MonoBehaviour
{
    public GameObject bala;
    public float velocidad = 5;
    public ParticleSystem particulas;
    public AudioSource sonido;
    public Animator animacionDisparo;
    void Update()
    {
		if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
		{
            print("Disparo!");
            GameObject disparo = Instantiate(bala, transform.position, transform.rotation);
            Rigidbody disparoRB = disparo.GetComponent<Rigidbody>();
            disparoRB.velocity = transform.forward*velocidad;
            particulas.Play();
            sonido.Play();
            animacionDisparo.SetTrigger("Disparar");
		}
    }
}
