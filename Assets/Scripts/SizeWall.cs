using UnityEngine;
using System.Collections;

public class SizeWall : MonoBehaviour
{

    private TunnelController sizeTunnel;

    void Awake()
    {
        sizeTunnel = GetComponentInParent<TunnelController>();
    }

    void Update()
    {

        if (transform.localScale.z != sizeTunnel.depth)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, sizeTunnel.depth);
        }
    }
}
