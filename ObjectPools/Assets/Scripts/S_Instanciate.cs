using UnityEngine;
using System.Collections;

public class S_Instanciate : MonoBehaviour
{
    public GameObject Obj;
    public float WaitTime = 0.5f;

	// Initiate the coroutine:
	void Start ()
    {
        StartCoroutine(Spawn());
	}
	
	IEnumerator Spawn()
    {
        while (true)
        {
            float RandX = Random.Range(-20.0f, 20.0f);
            float RandZ = Random.Range(-20.0f, 20.0f);
            Instantiate(Obj, new Vector3(RandX, transform.position.y, RandZ), this.transform.rotation);
            yield return new WaitForSeconds(WaitTime);
        }
    }
}
