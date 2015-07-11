using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndOfLevel : MonoBehaviour {

    public GameObject panel;

    void OnTriggerEnter()
    {
        panel.GetComponent<Animator>().SetBool("EndOfGame",true);
    }
}
