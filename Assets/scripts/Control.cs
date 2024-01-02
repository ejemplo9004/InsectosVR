using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
	public Transform Personaje;
    public static Control singleton;

	public List<Vector3> posiciones;

	public GameObject[] bichos;
	public bool enJuego = true;

	public Vector2 tiempos;

	private void Awake()
	{
        singleton = this;
	}
	void Start()
	{
		StartCoroutine(CrearBichos());
	}


	IEnumerator CrearBichos()
	{
		while (enJuego)
		{
			yield return new WaitForSeconds(Random.Range(tiempos.x, tiempos.y));
			GameObject bicho = Instantiate(bichos[Random.Range(0, bichos.Length)],
											posiciones[Random.Range(0, posiciones.Count)],
											Quaternion.identity);
		}
	}


}
