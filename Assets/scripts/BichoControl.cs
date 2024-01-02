using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BichoControl : MonoBehaviour
{
    public int vida = 2;
    public ParticleSystem particulasSangre;
    public Animator animator;
    public bool vivo = true;
	public Transform target;

    public AudioSource audioGolpe;
    public AudioSource audioMuerte;
	public float velocidad = 2;
	public Estados estado;
	float tiempoAtacar;

    void Start()
    {
		//Vector3 pos = transform.position + Vector3.up;
		//Ray r = new Ray(pos + Vector3.up, (Control.singleton.Personaje.position - pos + Vector3.up));
		//RaycastHit hit;
		//if (Physics.Raycast(r, out hit, (Control.singleton.Personaje.position - pos + Vector3.up).magnitude))
		//{
		//	if (hit.collider.CompareTag("Pared"))
		//	{
		//		Destroy(gameObject);
		//	}
		//}
		Invoke("Morir", 30f);
		target = Control.singleton.Personaje;
		StartCoroutine(CambioEstados());
	}

	IEnumerator CambioEstados()
	{
		while (vivo)
		{
			yield return new WaitForSeconds(Random.Range(2f, 10f));
			if (estado != Estados.muerto && estado != Estados.atacando)
			{
				if (estado == Estados.idle)
				{
					estado = Estados.seguir;
					animator.SetBool("Walk Forward", true);
				}
				else
				{
					estado = Estados.idle;
					animator.SetBool("Walk Forward", false);
				}
			}
		}
	}

    public void Morir()
	{
		if (!vivo)
		{
			return;
		}
		vivo = false;
		animator.SetTrigger("Die");
		Destroy(gameObject, 10);
		audioMuerte.Play();
		estado = Estados.muerto;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!vivo)
		{
            return;
		}
		if (other.CompareTag("Bala"))
		{
            particulasSangre.Play();
            vida--;
			if (vida > 0)
			{
                animator.SetTrigger("Take Damage");
                audioGolpe.Play();
			}
			else
			{
				Morir();
			}
		}
	}

	private void Update()
	{
		switch (estado)
		{
			case Estados.idle:
				break;
			case Estados.seguir:
				Quaternion rotacion = transform.rotation;
				Vector3 nDirMirar = target.position;
				nDirMirar.y = transform.position.y;
				transform.LookAt(nDirMirar, Vector3.up);
				transform.rotation = Quaternion.Lerp(rotacion, transform.rotation, Time.deltaTime);
				transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
				if ((transform.position - nDirMirar).sqrMagnitude < 0.8f)
				{
					estado = Estados.atacando;
					animator.SetBool("Walk Forward", false);
				}
				break;
			case Estados.muerto:
				break;
			case Estados.atacando:
				Vector3 nDirMirar2 = target.position;
				nDirMirar2.y = transform.position.y;
				if ((transform.position - nDirMirar2).sqrMagnitude > 1.3f)
				{
					estado = Estados.seguir;
					animator.SetBool("Walk Forward", true);
				}
				if (Time.time > tiempoAtacar)
				{
					animator.SetTrigger("Smash Attack");
					tiempoAtacar = Time.time + 1;
				}
				break;
			default:
				break;
		}
	}
}

public enum Estados
{
	idle,
	seguir,
	muerto, 
	atacando
}