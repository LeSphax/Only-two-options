using UnityEngine;
using System.Collections;

public class TunnelPart : MonoBehaviour {

    public GameObject leftWall;
    public GameObject rightWall;

    public void DesactivateWalls()
    {
        leftWall.SetActive(false);
        rightWall.SetActive(false);
    }

    public void Desactivate()
    {
        gameObject.SetActive(false);
    }
}
