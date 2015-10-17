using UnityEngine;
using System.Collections;

public class Launcher : MonoBehaviour {

    public GameObject obstacle;
    public int timeBetweenLaunches;
    private int launchedNumber;

	void Start () {
        launchedNumber = 0;
        Launch();
	}
	
	void Launch () {
        Instantiate(obstacle, transform.position, transform.rotation);
        Invoke("Launch", timeBetweenLaunches);
	}
}
