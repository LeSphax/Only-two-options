using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndOfLevel : MonoBehaviour {

    void OnTriggerEnter()
    {
        Debug.Log("End");
        GameObject.FindGameObjectWithTag("EndGameScreen").GetComponent<Animator>().SetBool("EndOfGame",true);
    }
}
