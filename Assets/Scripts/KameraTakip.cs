using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraTakip : MonoBehaviour
{
    [SerializeField]
    Transform hedef;

    Vector3 hedefUzaklik;

    private void Start()
    {
        hedefUzaklik = transform.position - hedef.position;
    }

    private void LateUpdate()
    {
        if (hedef)
        {
            transform.position = Vector3.Lerp(transform.position, hedef.position + hedefUzaklik, 0.1f);
        }
    }
}
