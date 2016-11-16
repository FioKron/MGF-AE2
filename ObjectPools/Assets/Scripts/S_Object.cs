using UnityEngine;
using System.Collections;

public class S_Object : MonoBehaviour, IPoolableObject
{
    public void Reset()
    {
        gameObject.SetActive(true);
    }

    /**
        Return the object to the pool here: (Keep this in mind)
    */
    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }

    public void Spawn(GameObject ObjectToSpawn)
    {
        float RandX = Random.Range(-20.0f, 20.0f);
        float RandZ = Random.Range(-20.0f, 20.0f);

        transform.position.Set(RandX, 45.0f, RandZ);
        
        GetComponent<Rigidbody>().velocity.Set(0.0f, 0.0f, 0.0f);

        Reset();
    }

    void OnTriggerEnter(Collider other)
    {
        ReturnToPool();
    }
}
