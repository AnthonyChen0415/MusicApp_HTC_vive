using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class Sequencer : MonoBehaviour {
    public CanvasController control;
    [HideInInspector]
    public float bpm = 900.0F;
    //[HideInInspector]
    public int numBeatsPerSegment=16;
    public AudioClip[] clips = new AudioClip[2];
    public int index;
    public bool flag;
    private double nextEventTime;
    private int flip = 0;
    private AudioSource[] audioSources = new AudioSource[2];
    private bool running = false;
    void Awake()
    {
        int i = 0;
        while (i < 2)
        {
            GameObject child = new GameObject("Player" + i);
            child.transform.parent = gameObject.transform;
            audioSources[i] = child.AddComponent<AudioSource>();
            audioSources[i].playOnAwake = false;
            i++;
        }
        
        running = true;
        flip = 0;
    }
    private void Start()
    {
        
        flag = true;
    }
    int setup(int temp)
    {
        int step = 0;
        switch (temp)
        {
            case 1:
                step = 18;
                break;
            case 2:
                step = 17;
                break;
            case 3:
                step = 15;
                break;
            case 4:
                step = 14;
                break;
            case 5:
                step = 11;
                break;
            case 6:
                step = 10;
                break;
        }
        return step;
    }
    void Update()
    {
        if (control.moved[index])
        {
            running = true;
            if(flag == true) { nextEventTime = AudioSettings.dspTime + 2.0F;  flag = false; }
        }
        else
        {
            running = false;
            flag = true;
        }
        if (!running)
            return;
        if (flip == 2) { flip = 0; }
        double time = AudioSettings.dspTime;
        
        if (time + 1.0F > nextEventTime)
        {
            audioSources[flip].clip = clips[flip];
            audioSources[flip].volume = control.ma[index].volume;
            
            audioSources[flip].PlayScheduled(nextEventTime);
            Debug.Log("played");
            //Debug.Log("Scheduled source " + flip + " to start at time " + nextEventTime);
            numBeatsPerSegment = setup(control.ma[index].complex);
            nextEventTime += 60.0F / bpm * numBeatsPerSegment;
            flip++;
        }
    }
}
