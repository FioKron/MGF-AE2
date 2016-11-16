using UnityEngine;
using System.Collections;

interface IPoolableObject
{
    void ReturnToPool();
    void Reset();
}