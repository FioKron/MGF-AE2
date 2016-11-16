using UnityEngine;
using System.Collections;

public class S_SpawnedObject : MonoBehaviour
{
    /**
        When the trigger for this object enters a collider...
    */
    void OnTriggerEnter(Collider Other)
    {
        Destroy(this.gameObject);
    }
}
