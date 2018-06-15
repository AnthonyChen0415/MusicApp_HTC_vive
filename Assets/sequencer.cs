using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sequencer : MonoBehaviour {
    [HideInInspector]
    public AudioSource audio;
    public CanvasController control;
    public int index;
	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
	}
	
    void sequence(int index)
    {
        int step = 6;
        if (control.moved[index])
        {
            for (int i = 1; i <= step; i++)
            {
                audio.volume = control.ma[index].volume;
                switch (i) {
                }

                audio.Play();
            }
        }
        
    }
	// Update is called once per frame
	void Update () {
        sequence(index);
	}
}
