using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiquidVolumeFX;

public class WaterAmountManager_Duru : MonoBehaviour
{
   // LiquidVolume liquid;//리퀴드 볼륨
    float waterLevel;//물 변화량
    float maxLevel = 0.808f;//최대량
    public float speed = 1000f;//속도
    LiquidVolume liquid;
    public Quaternion previousRotation;
    //전 프레임의 로테이션 값
    public Vector3 angularVelocity;
    //각속도를 관리할 변수 

    // Start is called before the first frame update
    void Start()
    {

        liquid = gameObject.GetComponent<LiquidVolume>();


    }

    // Update is called once per frame
    void Update()
    {

        //Debug.Log("타임델타 : " + 1f / Time.deltaTime);//30프레임 나옴.
        GetAngularVelocity();

        if (GetSpillPoint())//모델링의 tip에 닿으면 true를 내놓음.
        {
            WaterLevel();//tip에 닿을 시 작동.
        }

    }

    public void InitializeWaterLevel()//물양 초기화.(최대양)
    {
        liquid.level = maxLevel;
        waterLevel = 0f;
    }


    public Vector3 GetAngularVelocity()//각속도 구하기.만약에 중간에 멈춰버리면 0값 되니 이 점 주의해서 스크립팅.
    {
        Quaternion deltaRotation = transform.rotation * Quaternion.Inverse(previousRotation);//1프레임 이전의 값과 현재값간의 델타값 
        previousRotation = transform.rotation;//이전 프레임값에 현재 프레임값 대입.
        deltaRotation.ToAngleAxis(out var angle, out var axis);//deltaRotation의 각도와 축값 구하기.
        //각도에서 라디안으로 변환 
        angle *= Mathf.Deg2Rad;//각도를 라디안으로 변환.
        angularVelocity = (1.0f / Time.deltaTime) * angle * axis; //라디안*축*1초당 프레임수.
        return angularVelocity;
    }

    void WaterLevel()
    {
        float value = .4f;
        float clampX = Mathf.Abs(Mathf.Clamp(angularVelocity.x, -value, value));//
        float clampZ = Mathf.Abs(Mathf.Clamp(angularVelocity.z, -value, value));

        if (clampX == 0f || clampZ == 0f)
        {
            //각도에 비례해서 스피드가 정해졌으면 좋겠음.
            float speed2 = 1f;
            float rotX = Mathf.Abs(previousRotation.x);
            float rotZ = Mathf.Abs(previousRotation.z);
            clampX = speed2 * rotX;
            clampZ = speed2 * rotZ;
            Debug.Log("0clampX : " + clampX);

            if (rotX > rotZ)
            {
                Debug.Log("0&clampX : " + clampX);
                waterLevel += Time.deltaTime * clampX;

            }
            else if (rotX < rotZ)
            {
                Debug.Log("0&clampX : " + clampZ);
                waterLevel += Time.deltaTime * clampZ;
            }

        }

        if (previousRotation.x > previousRotation.z)
        {
            Debug.Log("clampX : " + clampX);
            waterLevel += Time.deltaTime * clampX;

        }
        else if (previousRotation.x < previousRotation.z)
        {
            Debug.Log("clampX : " + clampZ);
            waterLevel += Time.deltaTime * clampZ;
        }


        liquid.level = maxLevel - waterLevel;

    }


    #region LiquidVolumeFx
    void LiquidSurfaceYPosition()
    {
        Debug.Log("LiquidSurfaceYPos : " + liquid.liquidSurfaceYPosition);
    }

    bool GetSpillPoint()
    {

        return liquid.GetSpillPoint(out Vector3 spillPosition);

    }

    void BakeTransform()
    {
        liquid.BakeRotation();

    }

    void CenterPivot()
    {
        liquid.CenterPivot();
    }
    #endregion

}