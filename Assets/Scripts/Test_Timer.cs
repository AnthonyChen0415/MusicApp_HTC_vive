using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Timer : MonoBehaviour {
    public float timeLeft;
    private float duration;
	// Use this for initialization
	void Start () {
        duration = timeLeft;
	}
	
	// Update is called once per frame
	void Update () {
        duration -= Time.deltaTime;
        if (duration < 0)
        {
            detect();
            duration = timeLeft;
        }
    }
    void detect()
    {
        Debug.Log("Time is up");
    }
}
