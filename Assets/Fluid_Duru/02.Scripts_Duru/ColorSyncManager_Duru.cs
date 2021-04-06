using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LiquidVolumeFX;

public enum Tea
{
    tea_water,
    tea_flower,
    tea_root,
    tea_leaf,
    tea_seed
};



public class ColorSyncManager_Duru : MonoBehaviour
{

    Tea tea;

    public Color currentColor;//배합 전 색
    public Color previousColor;//배합 후 색

    LiquidVolume liquid;
    //StreamManager stream;
    // Start is called before the first frame update


    void Start()
    {
        tea = Tea.tea_water;
        // liquid.liquidColor1 = new Color(0, 0, 0, 0);
        currentColor = new Color(0, 0, 0, 1);
        previousColor = new Color(0, 0, 0, 0);
        liquid = gameObject.GetComponent<LiquidVolume>();
        //stream = gameObject.GetComponent<PourDetectingManager>().streamPrefab.GetComponent<StreamManager>();





        Debug.Log("tea is " + tea + liquid.liquidColor1);
    }
    void Update()
    {

        TeaSelect();


        liquid.liquidColor1 = currentColor;






    }
    void TeaSelect()
    {
        switch (tea)
        {
            case Tea.tea_water:
                liquid.liquidColor1 = new Color(1 / 255f, 1 / 255f, 1 / 255f, 1 / 255f);
                break;
            case Tea.tea_flower:
                liquid.liquidColor1 = new Color(0 / 255f, 183 / 255f, 81 / 255f, 170 / 255f);

                //  stream.lr.colorGradient 
                break;
            case Tea.tea_root:
                liquid.liquidColor1 = new Color(239 / 255f, 203 / 255f, 115 / 255f, 255 / 255f);
                break;
            case Tea.tea_leaf:
                liquid.liquidColor1 = new Color(247 / 255f, 241 / 255f, 167 / 255f, 255 / 255f);
                break;
            case Tea.tea_seed:
                liquid.liquidColor1 = new Color(218 / 255f, 159 / 255f, 157 / 255f, 255 / 255f);
                break;
            default:
                liquid.liquidColor1 = new Color(1 / 255f, 1 / 255f, 1 / 255f, 1 / 255f);
                break;

        }
        currentColor = liquid.liquidColor1;
    }
    public void ButtonDown(GameObject target)
    {


        if (target == GameObject.FindGameObjectWithTag("tea_leaf"))
        {
            tea = Tea.tea_leaf;
            Debug.Log("ButtonClick_leaf is now"+currentColor);


        }
        if (target == GameObject.FindGameObjectWithTag("tea_flower"))
        {
            tea = Tea.tea_flower;
            Debug.Log("ButtonClick_flower is now" + currentColor);

        }
        if (target == GameObject.FindGameObjectWithTag("tea_root"))
        {
            tea = Tea.tea_root;
            Debug.Log("ButtonClick_root is now" + currentColor);
        }
        if (target == GameObject.FindGameObjectWithTag("tea_seed"))
        {
            tea = Tea.tea_seed;
            Debug.Log("ButtonClick_seed is now" + currentColor);
        }

        // ColorMix(i);


    }
    //믹스버튼.
    //기존값에 버튼이 눌릴 때마다 그 값의 tea종류를 파악하고 색을 배합한다.
    //배합한 값은 currentColor로 들어간다.
    //currentColor를 previousColor에 넣는다.(이거 순서가 중요할 텐데?)
    public void ColorMix(GameObject target)
    {
        //버튼 클릭시 그 값을 받아와서 색상으로 표현.ok.
        //그 색상을 previousColor에 저장한다.
        //만약, 새로운 버튼값이 들어올 경우 previous Color와 병합한 후 평균화한다.
        //그 값을 다시 previousColor에 적용한다.
        
        Debug.Log("previousColor is " + previousColor);

        if (target == GameObject.FindGameObjectWithTag("leaf_Mix"))
        {
            Debug.Log("ColorMix_leaf is Clicked");
            currentColor = (previousColor + new Color(247 / 255f, 241 / 255f, 167 / 255f, 255 / 255f)) / 2f;
            Debug.Log("currentColor_leafmix is" + currentColor);
        }

        if (target == GameObject.FindGameObjectWithTag("flower_Mix"))
        {
            currentColor = (previousColor + new Color(0 / 255f, 183 / 255f, 81 / 255f, 170 / 255f)) / 2f;
            Debug.Log("ColorMix_flower is Clicked");
            Debug.Log("currentColor_leafmix is" + currentColor);
        }
        if (target == GameObject.FindGameObjectWithTag("root_Mix"))
        {
            currentColor = (previousColor + new Color(239 / 255f, 203 / 255f, 115 / 255f, 255 / 255f)) / 2f;
            Debug.Log("ColorMix_root is Clicked");
            Debug.Log("currentColor_leafmix is" + currentColor);
        }
        if (target == GameObject.FindGameObjectWithTag("seed_Mix"))
        {
            currentColor = (previousColor + new Color(218 / 255f, 159 / 255f, 157 / 255f, 255 / 255f)) / 2f;
            Debug.Log("ColorMix_seed is Clicked");
            Debug.Log("currentColor_leafmix is" + currentColor);
        }
        

     



    }

   




    //차의 농도 조절(시간이 지남에 따라 변화)
    void TeaConcentration()
    {
        //Empty
    }




}
