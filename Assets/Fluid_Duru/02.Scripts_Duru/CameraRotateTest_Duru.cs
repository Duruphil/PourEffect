using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CameraRotateTest_Duru : MonoBehaviour
{
    public Camera cameraObj;
    public GameObject myGameObj;
    private Vector3 pos;
    private Quaternion rotate;
    public Transform tr;
    Button defaultBtn;
    public float speed;
    private void Start()
    {
        pos = this.GetComponent<Transform>().position;
        rotate = this.GetComponent<Transform>().rotation;
        // tr.position = cameraObj.transform.position;
        defaultBtn = GameObject.FindWithTag("defaultBtn").GetComponent<Button>();
        defaultBtn.onClick.AddListener(BackToOriginPos);
    }
    void FixedUpdate()
    {
        
        RotateCamera();
    }
    void RotateCamera()
    {
        if (Input.GetMouseButton(0))
        {
            cameraObj.transform.RotateAround(myGameObj.transform.position,
                                            cameraObj.transform.up,
                                            -Input.GetAxis("Mouse X") * speed);
            cameraObj.transform.RotateAround(myGameObj.transform.position,
                                            cameraObj.transform.right,
                                            -Input.GetAxis("Mouse Y") * speed);
        }
    }
   public void BackToOriginPos()
    {
        cameraObj.transform.position = pos;
        cameraObj.transform.rotation = rotate;
        //cameraObj.transform.position = tr.position;
       // cameraObj.transform.rotation = tr.rotation;
        cameraObj.transform.LookAt(myGameObj.transform);
    }

    public void ReStart()
    {
        SceneManager.LoadScene("Scene");
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}

