using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_Test_Duru : MonoBehaviour
{
    //Angular Velocity 
    Quaternion previousRotation;
    //전 프레임의 로테이션 값
    Vector3 angularVelocity;
    //각속도를 관리할 변수 
    //이 함수를 업데이트에서 굴려줍니다.
    private void Update()
    {
        GetPedestrianAngularVelocity();
       Debug.Log(angularVelocity);
    }
    public Vector3 GetPedestrianAngularVelocity()
    {
        Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(previousRotation);
        previousRotation = transform.rotation;
        deltaRotation.ToAngleAxis(out var angle, out var axis);
        //각도에서 라디안으로 변환 
        angle *= Mathf.Deg2Rad;
        angularVelocity = (1.0f / Time.deltaTime) * angle * axis; //각속도 반환 
        return angularVelocity;
    }


}
