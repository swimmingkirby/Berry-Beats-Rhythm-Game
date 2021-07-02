using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menumovement : MonoBehaviour
{
    //public GameObject uptween;
    //public GameObject downtween;

    public ScrollRect scrollBar;
    public float storypos = 0.75f;
    public float onlinepos = 0.5f;
    public float freemodepos = 0.25f;

    public bool storyselected = false;
    public bool onlineselected = false;
    public bool freemodeselected = false;
    // Start is called before the first frame update
    void Start()
    {
        onlinePressed();

    }

    // Update is called once per frame  
    void Update()
    {
        if (storyselected)
        {
            storyPressed(); 
        }
        if (onlineselected)
        {
            onlinePressed();
        }
        if (freemodeselected) {
            freemodePressed();
        }
        Debug.Log("online- " + onlineselected.ToString() + " story-" + storyselected.ToString() + " freemode-" + freemodeselected.ToString());
        
           

        


    }
 


    public void storyPressed (){
        onlineselected = false;
        freemodeselected = false;
        storyselected = true;
        scrollBar.verticalNormalizedPosition = Mathf.Lerp(scrollBar.verticalNormalizedPosition,storypos,0.2f);
    }
    public void onlinePressed()
    {
        freemodeselected = false;
        storyselected = false;
        onlineselected = true;
        scrollBar.verticalNormalizedPosition = Mathf.Lerp(scrollBar.verticalNormalizedPosition, onlinepos, 0.2f);
    }
    public void freemodePressed()
    {
        storyselected = false;
        onlineselected = false;
        freemodeselected = true;
        scrollBar.verticalNormalizedPosition = Mathf.Lerp(scrollBar.verticalNormalizedPosition, freemodepos, 0.2f);
    }
}


