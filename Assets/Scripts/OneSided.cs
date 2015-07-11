using UnityEngine;
using System.Collections;

public class OneSided : MonoBehaviour
{

    private float previousYRot = 0f;

    void Awake()
    {
    }

    void Update()
    {

        if (Mathf.Abs(previousYRot - transform.eulerAngles.y) < 100)
        {
            if ((previousYRot > 90 && transform.eulerAngles.y < 90) || (previousYRot < 90 && transform.eulerAngles.y > 90) || (previousYRot > 270 && transform.eulerAngles.y < 270) || (previousYRot < 270 && transform.eulerAngles.y > 270))
            {
                 //fps.Rotate180();
                //sizeTunnel.Rotate180();
            }
        }
        previousYRot = transform.eulerAngles.y;
    }
}
