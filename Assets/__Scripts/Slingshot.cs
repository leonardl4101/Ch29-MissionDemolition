using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Slingshot : MonoBehaviour {

    // class level variable
    public GameObject launchPoint;

    private void Awake()
    {
        Transform launchPointTrans = transform.Find("LaunchPoint");
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
    }

    // Use this for initialization

    void OnMouseEnter ()

    {
        //print("Slingshot:OnMouseEnter()");
        launchPoint.SetActive(true);
    }

    // Update is called once per frame

    private void OnMouseExit()
    {
        //print("Slingshot:OnMouseEnter()");
        launchPoint.SetActive(false);
    }


}
