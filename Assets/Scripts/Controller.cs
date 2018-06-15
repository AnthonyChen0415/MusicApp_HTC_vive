using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Controller : MonoBehaviour {
    public GameObject Kick;
    public GameObject Clap;
    public GameObject Snare;
    public GameObject HiHat;
    public GameObject Shaker;
    public GameObject Cymbal;

    public bool Instrument1;
    public bool Instrument2;
    public bool Instrument3;
    public bool Instrument4;
    public bool Instrument5;
    public bool Instrument6;
    
    public Animator anim;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        Instrument1 = false;
        Instrument2 = false;
        Instrument3 = false;
        Instrument4 = false;
        Instrument5 = false;
        Instrument6 = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            Instrument1 = !Instrument1;
            GameObject on = Kick.transform.GetChild(1).gameObject;
            GameObject off = Kick.transform.GetChild(0).gameObject;
            on.transform.position = new Vector3(off.transform.position.x+(float)(0.02), off.transform.position.y + (float)(0.01), off.transform.position.z);
            anim.Play("Kick_Play");
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            Instrument2 = !Instrument2;
            anim.Play("Snare_Play");
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            Instrument3 = !Instrument3;
            anim.Play("Clap_Play");
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            Instrument4 = !Instrument4;
            anim.Play("HiHats_Play");
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Instrument5 = !Instrument5;
            anim.Play("Cymbals_Play");
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Instrument6 = !Instrument6;
            anim.Play("Maraca_Play");
        }

    }
}
