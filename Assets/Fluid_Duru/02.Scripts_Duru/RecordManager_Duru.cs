//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using VoxelBusters.ReplayKit;


//public class RecordManager_Duru : MonoBehaviour
//{



//    #region Record Method 정리.
//    //디바이스가 가능한 지 안하는 지 알려줌.
//    public void IsAvailable()
//    {
//        bool isRecordingAPIAvailable = ReplayKitManager.IsRecordingAPIAvailable();//레코딩 가능 여부 확인.
//        string message = isRecordingAPIAvailable ? "Replay Kit recording API is available!" : "Replay Kit recording API is not available.";
//        Debug.Log(message);
//    }


//    //플러그인과 DidInitialize event  초기화.
//    public void Initailize()
//    {
//        ReplayKitManager.Initialise();
//    }

//    //DidInitialize event 등록했는 지 확인해야 함. 그렇게 해야 콜백받을 수 있음.
//    void OnEnable()
//    {
//        // Register to the events
//        ReplayKitManager.DidInitialise += DidInitialise;
//        ReplayKitManager.DidRecordingStateChange += DidRecordingStateChange;

//    }
//    void OnDisable()
//    {
//        // Deregister the events
//        ReplayKitManager.DidInitialise -= DidInitialise;
//        ReplayKitManager.DidRecordingStateChange -= DidRecordingStateChange;

//    }

//    private void DidInitialise(ReplayKitInitialisationState state, string message)
//    {
//        Debug.Log("Received Event Callback : DidInitialise [State:" + state.ToString() + " " + "Message:" + message);
//        switch (state)
//        {
//            case ReplayKitInitialisationState.Success:
//                Debug.Log("ReplayKitManager.DidInitialise : Initialisation Success");
//                break;
//            case ReplayKitInitialisationState.Failed:
//                Debug.Log("ReplayKitManager.DidInitialise : Initialisation Failed with message[" + message + "]");
//                break;
//            default:
//                Debug.Log("Unknown State");
//                break;
//        }
//    }

//    /*
//    * DidRecordingStateChanges event를 통해 레코딩 상태를 받을 수 있다. 이벤트는 현재 상태를 알려준다. 
//    * ReplayKitRecordingState.Started : 비디오 시작하면 트리거됨.
//ReplayKitRecordingState.Stopped : 비디오 멈추면 트리거됨.
//ReplayKitRecordingState.Failed : 비디오 녹화 실패하면 뜸.
//ReplayKitRecordingState.Available : 프리뷰 얻으면 트리거됨.
//    */
//    private void DidRecordingStateChange(ReplayKitRecordingState state, string message)
//    {
//        Debug.Log("Received Event Callback : DidRecordingStateChange [State:" + state.ToString() + " " + "Message:" + message);
//        switch (state)
//        {
//            case ReplayKitRecordingState.Started:
//                Debug.Log("ReplayKitManager.DidRecordingStateChange : Video Recording Started");
//                break;
//            case ReplayKitRecordingState.Stopped:
//                Debug.Log("ReplayKitManager.DidRecordingStateChange : Video Recording Stopped");
//                break;
//            case ReplayKitRecordingState.Failed:
//                Debug.Log("ReplayKitManager.DidRecordingStateChange : Video Recording Failed with message [" + message + "]");
//                break;
//            case ReplayKitRecordingState.Available:
//                Debug.Log("ReplayKitManager.DidRecordingStateChange : Video Recording available for preview / file access");
//                break;
//            default:
//                Debug.Log("Unknown State");
//                break;
//        }
//    }

//    //레코딩되고 있으면 참이 뜸.
//    public bool IsRecording()
//    {
//        return ReplayKitManager.IsRecording();
//    }
//    //프리뷰할 것이 이전에 있으면 프리뷰할 수 있음. 
//    public bool IsPreviewAvailable()
//    {
//        return ReplayKitManager.IsPreviewAvailable();
//    }

//    //이걸로 레코딩하고 만약에 마이크가 필요하면 그걸 패스할 수 있는 옵션이 있음. 안드로이드의 경우 마이크를 통해서 녹음된 게임음악만 녹화된다.(내장사운드로 녹화안된다는 뜻)
//    public void StartRecording()
//    {
//        ReplayKitManager.SetMicrophoneStatus(enable: true);
//        ReplayKitManager.StartRecording();
//    }


//    //이 콜은 현재 레코딩을 멈추고 프리뷰를 위해 필요한 녹화된 비디오를 틀어줄 준비를 한다.
//    public void StopRecording()
//    {
//        ReplayKitManager.StopRecording((filePath, error) =>
//        {
//            Debug.Log("File path available : " + ReplayKitManager.GetPreviewFilePath());
//        });
//    }




//    //프리뷰 열 수 있게 해주는 메소드. 없으면 false 내놓음.

//    bool Preview()
//    {
//        if (ReplayKitManager.IsPreviewAvailable())
//        {
//            return ReplayKitManager.Preview();
//        }
//        // Still preview is not available. Make sure you call preview after you receive ReplayKitRecordingState.Available status from DidRecordingStateChange
//        return false;
//    }

//    //레코딩된 거 삭제하는 기능. 아마 우린 필요없겠지만. 잘 처리되면 true를 뱉음.
//    bool Discard()
//    {
//        if (ReplayKitManager.IsPreviewAvailable())
//        {
//            return ReplayKitManager.Discard();
//        }
//        return false;
//    }


//    //레코딩 파일 위치문자열.

//    public string GetRecordingFile()
//    {
//        if (ReplayKitManager.IsPreviewAvailable())
//        {
//            return ReplayKitManager.GetPreviewFilePath();
//        }
//        else
//        {
//            string a = "NotAvailable";
//            Debug.LogError("File not yet available. Please wait for ReplayKitRecordingState.Available status");
//            return a;
//        }
//    }
//    //갤러리에 저장.
//    public void SavePreview() //Saves preview to gallery
//    {
//        if (ReplayKitManager.IsPreviewAvailable())
//        {
//            ReplayKitManager.SavePreview((error) =>
//            {
//                Debug.Log("Saved preview to gallery with error : " + ((error == null) ? "null" : error));
//            });
//        }
//        else
//        {
//            Debug.LogError("Recorded file not yet available. Please wait for ReplayKitRecordingState.Available status");
//        }
//    }

//    //공유
//    public void SharePreview()
//    {
//        if (ReplayKitManager.IsPreviewAvailable())
//        {
//            ReplayKitManager.SharePreview();
//            Debug.Log("Shared video preview");
//        }
//        else
//        {
//            Debug.LogError("Recorded file not yet available. Please wait for ReplayKitRecordingState.Available status");
//        }
//    }
//    #endregion

//}
