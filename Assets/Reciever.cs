using UnityEngine;
using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using VVVV_OSC;

public class Reciever : MonoBehaviour
{
    public Recognizor recognizor;
    private Thread thread;
    private OSCReceiver oscin;
    //[HideInInspector]
    public new bool[] selected;
    //[HideInInspector]
    public bool esc;
    public float horizontal;
    public float vertical;
    public bool flag_horizontal;
    public bool flag_vertical;
    bool _up_slide_start = false;
    bool _down_slide_start = false;
    bool _left_slide_start = false;
    bool _right_slide_start = false;
    public bool left_slide;
    public bool right_slide;
    public bool up_slide;
    public bool down_slide; 
    //[HideInInspector]
    public bool flag_rec;
    public int tap;
    private bool esc_enable;
    void OnEnable()
    {
        oscin = new OSCReceiver( 7000 );
        thread = new Thread(new ThreadStart(UpdateOSC));
        thread.IsBackground = true;
        thread.Start();
        flag_horizontal = false;
        flag_vertical = false;
        esc_enable = false;
    }

    void OnApplicationQuit()
    {
        oscin.Close();
        thread.Interrupt();
        if (!thread.Join(2000))
            thread.Abort();
    }


    void UpdateOSC()
    {
        while (true)
        {
            OSCPacket msg = oscin.Receive();
            if (msg != null)
            {
                OSCBundle b = (OSCBundle)msg;
                foreach (OSCPacket subm in b.Values)
                {
                    parseMessage(subm, true);
                }
                //Debug.Log("message");
                recognizor.received = true;
                //gotAMessage = false;
            }
            #region receive
            /*if (msg.IsBundle())
            {
                OSCBundle b = (OSCBundle)msg;
                foreach (OSCPacket subm in b.Values)
                {
                    parseMessage(subm, flag_rec);
                    //Debug.Log("here");
                }
            }
            else
            {
                parseMessage(msg, flag_rec);
            }
            //            }*/
            #endregion
        }
    }
    
    void parseMessage(OSCPacket msg, bool flag)
    {
        if ((int)msg.Values[1] != 3 & (int)msg.Values[1] != 4 & (int)msg.Values[1] != 5)
        {
            flag_horizontal = false;
            flag_vertical = false;
            detect((int)msg.Values[1]);
        }
        else
        {
            flag_horizontal = true;
            flag_vertical = true;
            direction((int)msg.Values[1], (float)msg.Values[2], (float)msg.Values[3], (int)msg.Values[4], (int)msg.Values[5]);
        }
    }
    void detect(int index)
    {
        switch (index)
        {
            case 11:
                selected[1] = true;
                tap = 1;
                esc_enable = true;
                break;
            case 10:
                selected[2] = true;
                tap = 2;
                esc_enable = true;
                break;
            case 9:
                selected[3] = true;
                tap = 3;
                esc_enable = true;
                break;
            case 8:
                selected[4] = true;
                tap = 4;
                esc_enable = true;
                break;
            case 7:
                selected[5] = true;
                tap = 5;
                esc_enable = true;
                break;
            case 6:
                selected[6] = true;
                tap = 6;
                esc_enable = true;
                break;
            case 13:
                if (esc_enable) {
                    esc = true;
                    tap = 7;
                    esc_enable = false;
                }
                break;
            case 14:
                if (esc_enable) {
                    esc = true;
                    tap = 7;
                    esc_enable = false;
                }
                break;
        }
    }
    void direction(int index, float height, float angle, int _horizontal_direction, int _vertical_direction)
    {
        
        #region simple_detect_direction
        if (index == 4)
        {
            height += 0.7f;
        }
        horizontal = angle/ 1.5f;
        vertical = height/ 1.4f;
        #endregion
        // Slide Down & Up
        if(index ==  4 & _vertical_direction == 0 &!_down_slide_start & !_up_slide_start){
            _up_slide_start = true;
        }
        if(index == 5 & _vertical_direction == 0 & !_down_slide_start & !_up_slide_start)
        {
            _down_slide_start = true;
        }
        if( index == 5 & _vertical_direction == 1 & _up_slide_start)
        {
            up_slide = true;
            Debug.Log("Up Slide");
            _up_slide_start = false;
        }
        if(index == 4 & _vertical_direction == 2 & _down_slide_start)
        {
            down_slide = true;
            Debug.Log("Down Slide");
            _down_slide_start = false;
        }
        // Slide Left & Right
        if (index == 3)
        {
            if(_horizontal_direction == 0 & !_left_slide_start & !_right_slide_start)
            {
                _left_slide_start = true;
                _right_slide_start = true;
            }
            if(_horizontal_direction == 1 & _left_slide_start)
            {
                left_slide = true;
                Debug.Log("Left Slide");
                _left_slide_start = false;
                _right_slide_start = false;
            }
            if (_horizontal_direction == 2 & _right_slide_start)
            {
                right_slide = true;
                Debug.Log("Right Slide");
                _left_slide_start = false;
                _right_slide_start = false;
            }
        }
    }
}