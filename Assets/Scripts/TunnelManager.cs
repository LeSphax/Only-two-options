using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

public class TunnelManager : MonoBehaviour
{
    public int depth = 100;
    public float verticalSize = 5f;
    public GameObject[] tunnelPartsTypes;
    public GameObject goalBeginning;
    public GameObject goalEnd;


    private LinkedList<TunnelPart> tunnel;
    private GameObject player;
    private float partDepth;


    void Awake()
    {
        partDepth = tunnelPartsTypes[0].transform.localScale.z;
        player = GameObject.FindGameObjectWithTag("Player");
        tunnel = new LinkedList<TunnelPart>();
    }



    public void ShrinkTunnel(int sizeLittleTunnel)
    {
        int sizeTunnel = tunnel.Count;

        foreach (TunnelPart tunnelPart in tunnel)
        {
            Destroy(tunnelPart.part);
        }
        depth = sizeLittleTunnel;
        tunnel = new LinkedList<TunnelPart>();
        float oldY = transform.localPosition.y;
        Start();
        transform.localPosition = new Vector3(transform.position.x, oldY, transform.position.z);
    }

    public void UpdateGoals()
    {
        Debug.Log(tunnel.Last.Value.part.transform.position.z);
        goalBeginning.transform.position= tunnel.First.Value.part.transform.position;
        goalEnd.transform.position = tunnel.Last.Value.part.transform.position;
        Debug.Log(goalEnd.transform.position.z);
    }

    public void CenterTunnel(float zPos)
    {
        float centerTunnel = (tunnel.Last.Value.part.transform.localPosition.z + tunnel.First.Value.part.transform.localPosition.z) / 2;
        if (zPos > centerTunnel + partDepth)
        {
            MoveBackward();
        }
        else if (zPos < centerTunnel - partDepth)
        {
            MoveForward();
        }
    }

    public void AddSideTunnel()
    {
        tunnel.
    }

    void Start()
    {
        transform.localPosition = player.transform.localPosition;
        transform.Translate((Vector3.back * depth * partDepth) / 2 + Vector3.up * verticalSize / 2);
        float currentLastPos = transform.localPosition.z;

        for (int i = 0; i < depth / tunnelPartsTypes.Length; i++)
        {
            for (int y = 0; y < tunnelPartsTypes.Length; y++)
            {
                tunnel.AddLast(new TunnelPart((GameObject)Instantiate(tunnelPartsTypes[y], new Vector3(transform.localPosition.x, transform.localPosition.y, currentLastPos), Quaternion.identity), y));
                currentLastPos += partDepth;
            }
        }
    }

    void MoveBackward()
    {

        GameObject.Destroy(tunnel.First.Value.part);
        tunnel.RemoveFirst();
        //
        float currentLastPos = tunnel.Last.Value.part.transform.localPosition.z;
        //Debug.Log("MoveToEnd : " + currentLastPos);
        int typeNumber = (tunnel.Last.Value.number + 1) % tunnelPartsTypes.Length;
        GameObject type = tunnelPartsTypes[typeNumber];
        //
        tunnel.AddLast(new TunnelPart((GameObject)Instantiate(type, new Vector3(transform.localPosition.x, transform.localPosition.y, currentLastPos + partDepth), Quaternion.identity), typeNumber));
    }

    void MoveForward()
    {
        GameObject.Destroy(tunnel.Last.Value.part);
        tunnel.RemoveLast();
        //
        float currentFirstPos = tunnel.First.Value.part.transform.localPosition.z;
        //Debug.Log("MoveToBeginning : " + currentFirstPos);
        int typeNumber = (tunnel.First.Value.number + 1) % tunnelPartsTypes.Length;
        GameObject type = tunnelPartsTypes[typeNumber];
        //
        tunnel.AddFirst(new TunnelPart((GameObject)Instantiate(type, new Vector3(transform.localPosition.x, transform.localPosition.y, currentFirstPos - partDepth), Quaternion.identity), typeNumber));
    }

    public bool IsCentered(float zPlayer, int blocksDifference)
    {
        float centerTunnel = (tunnel.Last.Value.part.transform.localPosition.z + tunnel.First.Value.part.transform.localPosition.z) / 2;
        return (Mathf.Abs(zPlayer - centerTunnel) > blocksDifference * partDepth);
    }


    private class TunnelPart
    {
        public GameObject part;
        public int number;

        public TunnelPart(GameObject part, int number)
        {
            this.part = part;
            this.number = number;
        }
    }
}
