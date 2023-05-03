using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemAutoDestroy : MonoBehaviour
{
    private ParticleSystem _ps;


    public void Start()
    {
        _ps = GetComponent<ParticleSystem>();

        if (_ps && !_ps.IsAlive())
        {
            Destroy(gameObject, 2f);
        }
    }

}
