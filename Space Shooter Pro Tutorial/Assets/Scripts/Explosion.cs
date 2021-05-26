using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    AudioSource _as;

    void Start()
    {
        _as = GetComponent<AudioSource>();
        if (_as == null)
        {
            Debug.LogError("Audio Source in Explosion is NULL");
        }
        else
        {
            _as.Play();
        }
        Destroy(gameObject, 3);
    }
}
