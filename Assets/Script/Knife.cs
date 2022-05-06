using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knife : MonoBehaviour
{
    public event UnityAction CutBrick;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Brick>(out Brick brick))
        {
            CutBrick?.Invoke();
        }
    }
}
