using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreamManager_Duru : MonoBehaviour
{
    public LineRenderer lr = null;//라인렌더러(줄기)
    public ParticleSystem splashParticle = null;//파티클 시스템(바닥면에서 튀기는 물방울들)

    private Coroutine pourRoutine = null;//붓는 패턴
    private Vector3 tp = Vector3.zero;//(0,0,0)
    private WaterAmountManager_Duru wam = null;
    private ColorSyncManager_Duru colorSyncManager = null;

    private void Awake()
    {
        wam = GameObject.FindGameObjectWithTag("glass").GetComponent<WaterAmountManager_Duru>();
        lr = GetComponent<LineRenderer>();
        splashParticle = GetComponentInChildren<ParticleSystem>();
        colorSyncManager = GameObject.FindObjectOfType<ColorSyncManager_Duru>();



    }
    private void Update()
    {
        SetWidth(wam.GetComponent<WaterAmountManager_Duru>().angularVelocity, wam.previousRotation);
    }
    private void Start()
    {
        MoveToPos(0, transform.position);
        MoveToPos(1, transform.position);
        //  Debug.Log(lr.GetPosition(0));



    }
    public void Begin()
    {
        pourRoutine = StartCoroutine(BeginPour());//붓는 코루틴 적용.
        StartCoroutine(UpdateParticle());//파티클 업데이트.
                                         // gameObject.SetActive(true);
    }
    private IEnumerator BeginPour()//어디로 떨어져서 부딪히게 할 지 결정.
    {
        while (gameObject.activeSelf)//while문을 돌리면 해당 조건이 모두 성사될 때까지 돌아감. 스트림 오브젝트가 활성화되어 있는 동안은,
        {
            tp = FindEndPoint();//타겟 포지션에 바닥면 닿는 포인트 적용.
            MoveToPos(0, transform.position);
            AnimateToPos(1, tp);
            //  AnimateToPos(1, tp);
            yield return null;
        }

    }
    //각속도에 맞춰서 물줄기 좁고 넓음 표현.
    public void SetWidth(Vector3 av, Quaternion tr)
    {
        //Mathf.Clamp(av.x, -1f, 1f);
        //Mathf.Clamp(av.z, -1f, 1f);
        float start = 0.001f;
        float end = 0.01f;
        //lr.startWidth = start;
        //lr.endWidth = end;

        Debug.Log("av.x : " +
        Mathf.Clamp(av.x, -2f, 2f));
        Debug.Log("av.z : " +
        Mathf.Clamp(av.z, -2f, 2f));
        float value = 5f;
        float clampX = Mathf.Abs(Mathf.Clamp(av.x, -value, value));
        float clampZ = Mathf.Abs(Mathf.Clamp(av.z, -value, value));

        if (Mathf.Abs(tr.x) > Mathf.Abs(tr.z))
        {
            Debug.Log("X!");

            
            
            lr.startWidth = start * clampX;
            lr.endWidth = end * clampX;
            Debug.Log("라인처음: " + lr.startWidth);
            Debug.Log("라인끝 : " + lr.endWidth);
        }
        else if (Mathf.Abs(tr.x) < Mathf.Abs(tr.z))
        {
            Debug.Log("Z!");
            lr.startWidth = start * clampZ;
            lr.endWidth = end * clampZ;
           
        }
        if (clampX == 0f || clampZ == 0f)
        {
            float speed2 = 5f;
            float rotX = Mathf.Abs(tr.x);
            float rotZ = Mathf.Abs(tr.z);
            clampX = speed2 * rotX;
            clampZ = speed2 * rotZ;
            Debug.Log("0clampX : " + clampX);

            if (rotX > rotZ)
            {
               
                 
                lr.startWidth = start * clampX;
                lr.endWidth = end * clampX;

            }
            else if (rotX < rotZ)
            {
                lr.startWidth = start * clampZ;
                lr.endWidth = end * clampZ;
            }
            //Debug.Log("Same!");
        //    lr.startWidth = start;
        //    lr.endWidth = end;
        }


       

    }


    public void End()
    {
        StopCoroutine(pourRoutine);//이걸 위해서 pourRoutine을 따로 둠.
        pourRoutine = StartCoroutine(EndPour());
        StopCoroutine(UpdateParticle());//추가해본 것. 파티클이 안 사라짐.
                                        // gameObject.SetActive(false);
    }

    private IEnumerator EndPour()
    {
        while (!HasReachedPos(0, tp))
        {
            AnimateToPos(0, tp);
            AnimateToPos(1, tp);
            yield return null;
        }

        Destroy(gameObject);


    }


    private Vector3 FindEndPoint()//타겟포지션 찾기
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);//현재 포지션에서 아래 방향으로 ray를 쏨.

        Physics.Raycast(ray, out hit, 2.0f);//최대 거리르 얼마로 하는 것은, 길이의 문제는..
        Vector3 endPoint = hit.collider ? hit.point : ray.GetPoint(2.0f); //hit.collder일 때 참이면 hit.point를, 참이 아니면 ray.Getpoint(2.0f) 한다.
                                                                          //일정 각도를 넘었을 때 콜라이딩되는 게 없어도 ray가 생기도록.
                                                                          //  Debug.Log("endPoint is " + endPoint);
        return endPoint;

    }

    private void MoveToPos(int index, Vector3 tp)//목표 지점을 인덱스별로. 목표지점으로 이동.
    {
        lr.SetPosition(index, tp);//라인렌더러의 포지션 지정.

    }
    private void AnimateToPos(int index, Vector3 tp)
    {
        Vector3 currentPoint = lr.GetPosition(index);
        Vector3 newPos = Vector3.MoveTowards(currentPoint, tp, Time.deltaTime * 1.75f);
        lr.SetPosition(index, newPos);
    }

    private bool HasReachedPos(int index, Vector3 tp)
    {
        Vector3 currentPos = lr.GetPosition(index);

        return currentPos == tp;//현재 제로에 수
    }

    private IEnumerator UpdateParticle()
    {
        while (gameObject.activeSelf)
        {
            splashParticle.gameObject.transform.position = tp;

            bool isHit = HasReachedPos(1, tp);
            splashParticle.gameObject.SetActive(isHit);
            yield return null;
        }

    }

}
