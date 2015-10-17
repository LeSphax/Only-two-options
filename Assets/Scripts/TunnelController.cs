using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;

public class TunnelController : MonoBehaviour
{
    public int depth = 100;
    public GameObject[] tunnelPartsTypes;
    public GameObject goal;


    private GameObject goalBeginningTunnel;
    private GameObject goalEndTunnel;
    private List<TunnelPart> tunnel;
    private float partDepth;
    private bool goalsCreated = true;
    public GameObject obstacleLauncherPrefab;

    private GameObject[] obstacleLaunchers;



    void Awake()
    {
        partDepth = tunnelPartsTypes[0].transform.localScale.z;
        tunnel = new List<TunnelPart>();
        CreateTunnel();
    }


    public void UpdateGoals()
    {
        if (goalBeginningTunnel == null)
        {
            goalBeginningTunnel = Instantiate(goal);
            goalBeginningTunnel.transform.parent = transform;
            goalEndTunnel = Instantiate(goal);
            goalEndTunnel.transform.parent = transform;
        }
        goalBeginningTunnel.transform.position = tunnel[0].part.transform.position;
        goalEndTunnel.transform.position = tunnel[tunnel.Count - 1].part.transform.position;
    }

    public void CenterTunnel(float zPos)
    {
        float centerTunnel = (tunnel[tunnel.Count - 1].part.transform.position.z + tunnel[0].part.transform.position.z) / 2;
        if (zPos > centerTunnel + partDepth)
        {
            SuppressTunnelPart(true);
            CreateNewTunnelPart(false);
        }
        else if (zPos < centerTunnel - partDepth)
        {
            SuppressTunnelPart(false);
            CreateNewTunnelPart(true);
        }
        UpdateGoals();
    }

    public Vector3 SuppressWallsOnCenter(int blocksDifference, bool forward)
    {
        int direction = forward ? -1 : 1;
        GameObject tunnelPart = tunnel[depth / 2 + (blocksDifference) * direction].part;
        tunnelPart.BroadcastMessage("DesactivateWalls");
        return tunnelPart.transform.position;
    }

    public Vector3 SuppressPartOnCenter()
    {
        GameObject tunnelPart = tunnel[depth / 2].part;
        tunnelPart.BroadcastMessage("Desactivate");
        return tunnelPart.transform.position;
    }

    void CreateTunnel()
    {
        transform.Translate((Vector3.back * Mathf.Ceil(depth / 2) * partDepth));
        for (int i = 0; i < depth; i++)
        {
            CreateNewTunnelPart(false);
        }
        //UpdateLaunchers();
        UpdateGoals();
    }

    private void UpdateLaunchers()
    {
        if (obstacleLaunchers == null)
        {
            obstacleLaunchers = new GameObject[1];
            for (int i = 0; i < obstacleLaunchers.Length; i++)
            {
                obstacleLaunchers[i] = Instantiate(obstacleLauncherPrefab);
                obstacleLaunchers[i].transform.parent = transform;
            }
        }
        for (int i = 0; i < obstacleLaunchers.Length; i++)
        {
            //goalBeginningTunnel.transform.position = tunnel[0].part.transform.position;
        }
    }


    void CreateNewTunnelPart(bool atBeginning)
    {
        int index;
        Vector3 position;
        int typeNumber;
        if (tunnel.Count == 0)
        {
            index = 0;
            position = transform.position;
            typeNumber = 0;
        }
        else
        {
            index = atBeginning ? 0 : Mathf.Max(tunnel.Count - 1, 0);
            int multiplicator = atBeginning ? -1 : 1;
            position = tunnel[index].part.transform.position + transform.rotation * new Vector3(0, 0, partDepth * multiplicator);
            typeNumber = (tunnel[index].number + 1) % tunnelPartsTypes.Length;
            index = (atBeginning) ? 0 : index + 1;
        }
        GameObject type = tunnelPartsTypes[typeNumber];
        //
        GameObject newTunnelPart = (GameObject)Instantiate(type, position, transform.rotation);
        newTunnelPart.transform.SetParent(transform, true);
        //
        tunnel.Insert(index, new TunnelPart(newTunnelPart, typeNumber));
    }

    void SuppressTunnelPart(bool atBeginning)
    {
        int position = atBeginning ? 0 : Mathf.Max(tunnel.Count - 1, 0);
        //
        GameObject.Destroy(tunnel[position].part);
        tunnel.RemoveAt(position);
    }

    public bool IsCentered(float zPlayer, int blocksDifference)
    {
        float centerTunnel = (tunnel[tunnel.Count - 1].part.transform.position.z + tunnel[0].part.transform.position.z) / 2;
        return (Mathf.Abs(zPlayer - centerTunnel) < blocksDifference * partDepth);
    }

    public void ActivateObstacleLauncher(int number)
    {
        obstacleLaunchers[number].SendMessage("Activate");
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
