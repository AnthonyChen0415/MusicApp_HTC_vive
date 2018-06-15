using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ViveControllerInputTest : MonoBehaviour {
    [System.Serializable]
    public class Animation_ContropPannel
    {
        [HideInInspector]
        public Vector2 initialSize = new Vector2(0.03f, 0.02f);
        [HideInInspector]
        public Vector2 openedSize = new Vector2(1.2f, 0.8f);
        public Canvas canvas;
        [HideInInspector]
        public RectTransform canvasRect;
        [HideInInspector]
        public Vector2 currentSize;
        public void Initialize()
        {
            canvasRect = canvas.GetComponent<RectTransform>();
            canvasRect.localScale = initialSize;
            //canvas.gameObject.SetActive(false);
        }
    }
    //static float t = 0.0f;
    public Animation_ContropPannel animation_ContropPannel = new Animation_ContropPannel();    
    public GameObject view;
    private SteamVR_TrackedObject trackedObj;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    private bool opening;
    private float alphaSpeed = 2.0f;
    private CanvasGroup cg;
    private void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>(); 
    }
    private void Start()
    {
        animation_ContropPannel.Initialize();
        opening = false;
        cg = animation_ContropPannel.canvas.transform.GetComponent<CanvasGroup>();
        cg.alpha = 0;
        //cg.blocksRaycasts = false;
    }
    void open()
    {
        //animation_ContropPannel.currentSize.x = Mathf.Lerp(animation_ContropPannel.initialSize.x, animation_ContropPannel.openedSize.x,Time.time);
        //t += 0.5f * Time.deltaTime;
        //animation_ContropPannel.canvasRect.localScale = new Vector3 (Mathf.Lerp(0.03f, 1.2f, 0.1f*Time.deltaTime), Mathf.Lerp(0.02f, 0.8f, 0.1f * Time.deltaTime), animation_ContropPannel.canvasRect.localScale.z);
    }
    void Update() {
        RectTransform rectTransform = animation_ContropPannel.canvasRect;
        Vector3 Current_position = Controller.transform.pos;
        rectTransform.position = this.transform.position; // position
        rectTransform.rotation = view.transform.rotation; // rotation
        rectTransform.localScale = new Vector2(1.2f, 0.8f);
        if (Controller.GetHairTriggerDown())
        {
            
            Debug.Log(gameObject.name + "Trigger pressed");

            opening = !opening;
        }
        if (opening)
        {
            // After the trigger is pressed, the control pannel is created at the controller's current position and the 
            // angle should be align with the view camera
            //animation_ContropPannel.canvas.gameObject.SetActive(true);
            
            cg.alpha = Mathf.Lerp(cg.alpha, 1, alphaSpeed * Time.deltaTime);
            if (Mathf.Abs(1 - cg.alpha) <= 0.01)
            {
                cg.alpha = 1;
            }
        }
        else
        {
            
            cg.alpha = Mathf.Lerp(cg.alpha,0, alphaSpeed * Time.deltaTime);
            if (Mathf.Abs(0 - cg.alpha) <= 0.01)
            {
                cg.alpha = 0;
            }
            //cg.blocksRaycasts = false;
            //animation_ContropPannel.canvas.gameObject.SetActive(false);
        }
        /*if (Controller.GetHairTriggerUp())
        {
            Debug.Log(gameObject.name + "Trigger released");
        }
        if(Controller.GetAxis() != Vector2.zero)
        {
            Debug.Log(gameObject.name + Controller.GetAxis());
        }
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + "Grip pressed");
        }
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Grip))
        {
            Debug.Log(gameObject.name + "Grip released");   
        }*/
    }
}
