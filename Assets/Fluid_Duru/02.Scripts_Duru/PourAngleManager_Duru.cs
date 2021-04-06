using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourAngleManager_Duru : MonoBehaviour
{
    public Transform bowl;//물이 담긴 그릇.
    private float pourValue = 0;//기울기 변화값.
    private float currentPourValue = 0;//저장되는 기울기값.
    public bool isUpTilt = false;// 
    public bool isDownTilt = false;
    public Rigidbody rb;
    public float pourSpeed;//각도 변화에 따른 속도구하기.
    Quaternion itemRotation;
    Quaternion previousRotation;
    Vector3 angularVelocity;
    // Start is called before the first frame update
    private void Start()
    {
        previousRotation = bowl.transform.rotation;

    }
    void Update()
    {
       
       //CalculatePourSpeed();
        
     
       
        isUpTilt = CalculatePourAngle() < currentPourValue;
        if (isUpTilt)
        {
            isDownTilt = false;
            currentPourValue = pourValue;
          
        }
        isDownTilt = CalculatePourAngle() > currentPourValue;
        if(isDownTilt)
        {
            isUpTilt = false;
            currentPourValue = pourValue;
        }
        Debug.Log("지금 기울기값 : " + pourValue + " " + "맥시멈값 : " + currentPourValue);
        //Debug.Log("psm.isTilt : " + isUpTilt);

    }

    private float CalculatePourAngle()
    {
       
        Debug.Log("pourValue = " + pourValue);
       
        pourValue = transform.forward.y * Mathf.Rad2Deg;//

        return pourValue;
    }

   

   
}


