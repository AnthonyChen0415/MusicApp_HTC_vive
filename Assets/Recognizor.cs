using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recognizor : MonoBehaviour {
    public Reciever rec;
    public bool received;
    public new bool[] selected_stage;
    public new bool[] selected_canvas;
    public new bool[] selected_audio;
    public new bool[] keydown;
    public bool down_slide_stage;
    public bool down_slide_canvas;
    public bool up_slide_stage;
    public bool up_slide_canvas;
    public bool left_slide_stage;
    public bool left_slide_canvas;
    public bool right_slide_stage;
    public bool right_slide_canvas;
    public bool esc_stage;
    public bool esc_canvas;
    public float horizontal;
    public float vertical;
    public bool direct;
    private float timeLeft;
    private float duration;
    // Use this for initialization
    void Start () {
        timeLeft = 0.2f;
        duration = timeLeft;
        direct = false;
    }
	void Update () {
        
        
        duration -= Time.deltaTime;
        if (duration < 0)
        {
            detect();
            ditect_direction_gesture();
            ditect_direction();
            received = false;
            duration = timeLeft;
            
        }
        
    }

    void detect()
    {
        for (int i = 1; i<= 6; i++)
        {
            if (rec.selected[i])
            {
                keydown[i] = true;
            }
            if (keydown[i])
            {
                if (!received | (received & rec.tap != i))
                {
                    keydown[i] = false;
                    selected_stage[i] = true;
                    selected_canvas[i] = true;
                    selected_audio[i] = true;
                    rec.selected[i] = false;
                }
            }
        }
        if (rec.esc)
        {
            keydown[7] = true;
        }
        if (keydown[7])
        {
            if (!received | (received & rec.tap != 7))
            {
                keydown[7] = false;
                esc_stage = true;
                esc_canvas = true;
                rec.esc = false;
            }
        }
        #region old version for detecting
        /*if (rec.selected[1])
        {
            selected_stage[1] = true;
            selected_canvas[1] = true;
            selected_audio[1] = true;
            rec.selected[1] = false;
        }
        if (rec.selected[2])
        {
            selected_stage[2] = true;
            selected_canvas[2] = true;
            selected_audio[2] = true;
            rec.selected[2] = false;
        }
        if (rec.selected[3])
        {
            selected_stage[3] = true;
            selected_canvas[3] = true;
            selected_audio[3] = true;
            rec.selected[3] = false;
        }
        if (rec.selected[4])
        {
            selected_stage[4] = true;
            selected_canvas[4] = true;
            selected_audio[4] = true;
            rec.selected[4] = false;
        }
        if (rec.selected[5])
        {
            selected_stage[5] = true;
            selected_canvas[5] = true;
            selected_audio[5] = true;
            rec.selected[5] = false;
        }
        if (rec.selected[6])
        {
            selected_stage[6] = true;
            selected_canvas[6] = true;
            selected_audio[6] = true;
            rec.selected[6] = false;
        }*/

        /*if(rec.flag_horizontal & rec.flag_vertical) {
            horizontal = rec.horizontal;
            vertical = rec.vertical;
            direct = true;
        }
        else
        {
            direct = false;
        }*/
        #endregion
    }
    void ditect_direction()
    {
        if (rec.flag_horizontal & rec.flag_vertical)
        {
            horizontal = rec.horizontal;
            vertical = rec.vertical;
            direct = true;
        }
        else
        {
            direct = false;
        }
    }
    void ditect_direction_gesture()
    {
        if (rec.flag_horizontal & rec.flag_vertical)
        {
            if (rec.down_slide)
            {
                down_slide_canvas = true;
                down_slide_stage = true;
                rec.down_slide = false;
            }
            if (rec.up_slide)
            {
                up_slide_canvas = true;
                up_slide_stage = true;
                rec.up_slide = false;
            }
            if (rec.left_slide)
            {
                left_slide_canvas = true;
                left_slide_stage = true;
                rec.left_slide = false;
            }
            if (rec.right_slide)
            {
                right_slide_canvas = true;
                right_slide_stage = true;
                rec.right_slide = false;
            }
        }
    }
}
