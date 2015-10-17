using UnityEngine;
using System.Collections;

public class Moving : MonoBehaviour {

    public int speed;

	// Use this for initialization
	void Start () {
       // GetComponent<Rigidbody>().velocity = Vector3.back * speed;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += Vector3.back * Time.deltaTime * speed;
	}
}
