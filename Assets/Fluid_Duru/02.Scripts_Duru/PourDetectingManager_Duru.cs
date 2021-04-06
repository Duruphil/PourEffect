using System.Collections;
using UnityEngine;
using LiquidVolumeFX;


//PourDetector는 특정각도의 기울기를 넘을 시 자연스럽게 스트림 발생하고 기울기가 다시 특정임계치 미만으로 왔을 때 스트림을 멈추는 스크립트이다.
public class PourDetectingManager_Duru : MonoBehaviour
{

    private ColorSyncManager_Duru colorSyncManager = null;
    public int pourThreshold = 45;//붓기 임계치.
    public Transform origin = null;//입구.
    public GameObject streamPrefab = null;//파티클(부딪힐 때 발생하는)
    private bool isPouring = false;
    public StreamManager_Duru currentStream = null;
    LiquidVolume liquid;
    float waterLevel;
    bool isSpill = false;
   // public bool isStream = false;//스트림 isntantiate 유무 통신용.
    //public GameObject stream;
    private void Start()
    {
    liquid = gameObject.GetComponent<LiquidVolume>();
        colorSyncManager = GameObject.FindObjectOfType<ColorSyncManager_Duru>();

    }
    private void Update()
    {
      
        bool spillCheck = liquid.GetSpillPoint(out Vector3 spillPosition);
        Debug.Log("spillCheck is " + spillCheck);
        origin.transform.position = liquid.spillPos;
        if (isSpill != spillCheck)
        {
            isSpill = spillCheck;
            if (isSpill)
            {
                StartPour();
                
            }
            else
            {
                EndPour();
            }
        }
      
    }

    public void StartPour()//붓기 시작.
    {
        currentStream = CreateStream();
        currentStream.Begin();
      //  isStream = true;
        print("Start");
        //Debug.Log("stream cs is " + currentStream.isActiveAndEnabled);
        //Empty
    }
    public void EndPour()//붓기 끝냄.
    {
        print("End");
        currentStream.End();
        currentStream = null;
   
    }
    private float CalculatePourAngle()//붓는 각도 계산.
    {
        Debug.Log(transform.forward.y * Mathf.Rad2Deg);
        return transform.forward.y * Mathf.Rad2Deg;//라디안 사용에 대한 공부가 좀 필요.
    }
    private StreamManager_Duru CreateStream()//스트림생성.
    {
        GameObject streamObj = Instantiate(streamPrefab, origin.position, Quaternion.identity, transform);
        var mainPs = streamObj.GetComponent<StreamManager_Duru>().splashParticle.main;
        mainPs.startColor = colorSyncManager.currentColor;
        streamObj.GetComponent<StreamManager_Duru>().lr.endColor = colorSyncManager.currentColor;

        return streamObj.GetComponent<StreamManager_Duru>();//이런 형태 처음 봤따..
    }
}