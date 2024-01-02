using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planificador : MonoBehaviour
{
    public Transform SupIz;
    public Transform SupDer;
    public Transform InfIz;
    public Transform InfDer;
    public int iteraciones = 10;
    private float tamaHorizontal;
    private float tamaVertical;
    public TipoPlano tipo;


    void Start()
    {
        tamaHorizontal = SupDer.position.x - SupIz.position.x;
        tamaVertical = SupDer.position.z - InfDer.position.z;

		if (Mathf.Abs(SupIz.position.y - InfIz.position.y) > 1)
		{
            tipo = TipoPlano.pared;
		}
		else
        {
			if (Control.singleton.Personaje.position.y < transform.position.y)
			{
                tipo = TipoPlano.techo;
			}
			else
			{
                tipo = TipoPlano.piso;
				for (int i = 1; i < iteraciones; i++)
				{
					for (int j = 1; j < iteraciones; j++)
					{
						Vector3 pos = SupIz.position + (SupDer.position - SupIz.position) / (float)iteraciones * i;
						pos = pos + (InfIz.position - SupIz.position) / (float)iteraciones * j;

						
							Control.singleton.posiciones.Add(pos);
					}
				}
			}
        }
    }

    void Update()
    {
        
    }

	private void OnDrawGizmos()
	{
		switch (tipo)
		{
			case TipoPlano.pared:
				Gizmos.color = Color.red;
				break;
			case TipoPlano.piso:
				Gizmos.color = Color.green;
				break;
			case TipoPlano.techo:
				Gizmos.color = Color.white;
				break;
			default:
				break;
		}
		if (SupDer == null || SupIz == null || InfIz == null || InfDer == null) return;
		for (int i = 1; i < iteraciones; i++)
		{
			for (int j = 1; j < iteraciones; j++)
			{
                Vector3 pos = SupIz.position + (SupDer.position - SupIz.position) / (float)iteraciones * i;
                pos = pos + (InfIz.position - SupIz.position) / (float)iteraciones * j;
                Gizmos.DrawSphere(pos, 0.08f);
			}
		}
	}
}

public enum TipoPlano
{
    pared,
    piso,
    techo
}
