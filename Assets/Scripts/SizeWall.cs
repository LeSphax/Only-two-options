using UnityEngine;
using System.Collections;

public class SizeWall : MonoBehaviour
{

    private TunnelManager sizeTunnel;

    void Awake()
    {
        sizeTunnel = GetComponentInParent<TunnelManager>();
    }

    void Update()
    {

        if (transform.localScale.z != sizeTunnel.depth)
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, sizeTunnel.depth);
        }
    }
}
