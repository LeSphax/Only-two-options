using UnityEngine;
using System.Collections;

public class TunnelMoving : MonoBehaviour
{
    private enum DirectionPlayer
    {
        FORWARD,
        BACK,
        SIDEWAY
    }

    public GameObject backWall;
    public int blocksBeforeBackWall = 10;

    private GameObject instanceBackWall;
    private DirectionPlayer directionPlayer;
    private TunnelManager tunnel;
    private float shrinkingTimer = 0f;
    private Vector3 previousPlayerPos;
    private GameObject player;

    private Camera myCamera;

    void Awake()
    {
        tunnel = GetComponent<TunnelManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        previousPlayerPos = player.transform.localPosition;
        myCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    void SetDirectionPlayer()
    {
        if (Vector3.Angle(player.transform.forward, Vector3.forward) < myCamera.fieldOfView / 2)
        {
            directionPlayer = DirectionPlayer.FORWARD;
        }
        else if (Vector3.Angle(player.transform.forward, Vector3.back) < myCamera.fieldOfView / 2)
        {
            directionPlayer = DirectionPlayer.BACK;
        }
        else
        {
            directionPlayer = DirectionPlayer.SIDEWAY;
        }
    }

    void FixedUpdate()
    {
        SetDirectionPlayer();
        tunnel.UpdateGoals();
        if (!instanceBackWall)
        {

            Vector3 playerMovement = player.transform.localPosition - previousPlayerPos;
            // If the player is going backward
            if (Vector3.Angle(player.transform.forward, playerMovement) > 90 && directionPlayer != DirectionPlayer.SIDEWAY)
            {
                if (tunnel.IsCentered(player.transform.position.z, blocksBeforeBackWall))
                {

                    PutBackWall();
                }
            }
            else
            {
                tunnel.CenterTunnel(player.transform.position.z);
            }

        }
        else
        {
            RaycastHit raycast;
            Physics.Raycast(player.transform.position, player.transform.forward, out raycast);
            if (raycast.collider == instanceBackWall.GetComponent<Collider>())
            {
                Debug.Log("YES");
                tunnel.ShrinkTunnel(blocksBeforeBackWall * 3);
                tunnel.UpdateGoals();
                this.enabled = false;
            }
        }
        previousPlayerPos = player.transform.localPosition;
    }

    public void PutBackWall()
    {
        if (directionPlayer == DirectionPlayer.FORWARD)
        {
            instanceBackWall = (GameObject)Instantiate(backWall, new Vector3(tunnel.transform.localPosition.x, tunnel.transform.localPosition.y, player.transform.position.z - 10f), Quaternion.identity);
        }
        else
        {
            instanceBackWall = (GameObject)Instantiate(backWall, new Vector3(tunnel.transform.localPosition.x, tunnel.transform.localPosition.y, player.transform.position.z + 10f), Quaternion.identity);
        }
    }

    /*
    void ShrinkingTunnel()
    {
        Vector3 playerMovement = player.transform.localPosition - previousPlayerPos;
        if (Vector3.Angle(player.transform.forward, playerMovement) > 90 && directionPlayer != DirectionPlayer.SIDEWAY)
        {
            shrinkingTimer += Time.deltaTime;
            if (shrinkingTimer > shrinkingTime)
            {
                tunnel.ShrinkTunnel();
                shrinkingTimer = 0f;
            }
        }
    }*/


}
