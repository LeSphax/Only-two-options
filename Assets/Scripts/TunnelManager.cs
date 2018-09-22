using UnityEngine;
using System.Collections;

public class TunnelManager : MonoBehaviour
{
    private enum DirectionPlayer : int
    {
        FORWARD = 0,
        BACK = 1,
        SIDEWAY= 2
    }

    public int numberBlocksBeforeSideTunnel;
    public int sizeLittleTunnel;
    public int sizeBigTunnel;
    public GameObject tunnelObject;
    public float verticalSize = 5f;

    private DirectionPlayer directionPlayer;
    private TunnelController tunnel;
    private TunnelController littleTunnel;
    private Vector3 previousPlayerPos;
    private GameObject player;

    private Camera myCamera;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        previousPlayerPos = player.transform.position;
        myCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = false;
            Screen.fullScreen = true;
        }
    }

    void OnPointerDown()
    {
        
    }


    void Start()
    {
        // Vector3 tunnelPosition = player.transform.position + Vector3.up * verticalSize / 2;
        tunnelObject.GetComponent<TunnelController>().depth = sizeBigTunnel;
        tunnel = ((GameObject)Instantiate(tunnelObject, player.transform.position, Quaternion.identity)).GetComponent<TunnelController>();
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

    void CreateLittleTunnel()
    {
        int posSideTunnelInBlocks = numberBlocksBeforeSideTunnel+1;
        tunnelObject.GetComponent<TunnelController>().depth = sizeLittleTunnel;
        Vector3 tunnelPosition = tunnel.SuppressWallsOnCenter(posSideTunnelInBlocks, directionPlayer == DirectionPlayer.FORWARD);
        littleTunnel = ((GameObject)Instantiate(tunnelObject, tunnelPosition, Quaternion.Euler(new Vector3(0, 90, 0)))).GetComponent<TunnelController>();
        littleTunnel.SuppressPartOnCenter();
    }

    void FixedUpdate()
    {
        SetDirectionPlayer();
        if (!littleTunnel)
        {

            Vector3 playerMovement = player.transform.position - previousPlayerPos;
            // If the player is going backward and not looking at the wall
            if (Vector3.Angle(player.transform.forward, playerMovement) > 90 && directionPlayer != DirectionPlayer.SIDEWAY)
            {
                if (!tunnel.IsCentered(player.transform.position.z, numberBlocksBeforeSideTunnel))
                {
                    CreateLittleTunnel();
                }
            }
            else if (Vector3.Angle(player.transform.forward, playerMovement) < 90 && playerMovement != Vector3.zero && directionPlayer != DirectionPlayer.SIDEWAY)
            {
                if (directionPlayer == DirectionPlayer.BACK)
                {

                }
                tunnel.CenterTunnel(player.transform.position.z);
            }
            else
            {
                tunnel.CenterTunnel(player.transform.position.z);
            }

        }
        else
        {

        }
        previousPlayerPos = player.transform.position;
    }


}
