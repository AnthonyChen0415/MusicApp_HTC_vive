using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
    public int selected = 0;
    public int moving = 0;
    public GameObject[] ins;
    public Recognizor rec;
    public CanvasController control;
    void play(int index)
    {
        GameObject instrument = ins[index];
        AudioSource audio = instrument.GetComponent<AudioSource>();
        audio.Play();
    }
    void Update()
    {
        #region 1st controller
        if (moving == 0 & Input.GetKeyDown(KeyCode.Keypad7) & !control.moved[1])
        {
            selected = 1;
            play(1);
        }
        if (moving == 0 & Input.GetKeyDown(KeyCode.Keypad4) & !control.moved[2])
        {
            selected = 2;
            play(2);
        }
        if (moving == 0 & Input.GetKeyDown(KeyCode.Keypad1) & !control.moved[3])
        {
            selected = 3;
            play(3);
        }
        if (moving == 0 & Input.GetKeyDown(KeyCode.Keypad8) & !control.moved[4])
        {
            selected = 4;
            play(4);
        }
        if (moving == 0 & Input.GetKeyDown(KeyCode.Keypad5) & !control.moved[5])
        {
            selected = 5;
            play(5);
        }
        if (moving == 0 & Input.GetKeyDown(KeyCode.Keypad2) & !control.moved[6])
        {
            selected = 6;
            play(6);
        }
        #endregion
        #region 2nd controller
        if (moving == 0 & rec.selected_audio[1] & !control.moved[1])
        {
            selected = 1;
            play(1);
            rec.selected_audio[1] = false;
        }
        if (moving == 0 & rec.selected_audio[2] & !control.moved[2])
        {
            selected = 2;
            play(2);
            rec.selected_audio[2] = false;
        }
        if (moving == 0 & rec.selected_audio[3] & !control.moved[3])
        {
            selected = 3;
            play(3);
            rec.selected_audio[3] = false;
        }
        if (moving == 0 & rec.selected_audio[4] & !control.moved[4])
        {
            selected = 4;
            play(4);
            rec.selected_audio[4] = false;
        }
        if (moving == 0 & rec.selected_audio[5] & !control.moved[5])
        {
            selected = 5;
            play(5);
            rec.selected_audio[5] = false;
        }
        if (moving == 0 & rec.selected_audio[6] & !control.moved[6])
        {
            selected = 6;
            play(6);
            rec.selected_audio[6] = false;
        }
        #endregion
    }
}

