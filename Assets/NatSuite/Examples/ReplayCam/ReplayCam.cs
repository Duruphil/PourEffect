/* 
*   NatCorder
*   Copyright (c) 2020 Yusuf Olokoba
*/

namespace NatSuite.Examples {

    using UnityEngine;
    using System.Collections;
    using Recorders;
    using Recorders.Clocks;
    using Recorders.Inputs;
    using NatSuite.Sharing;
    
    public class ReplayCam : MonoBehaviour {

        [Header(@"Recording")]
        public int videoWidth = 1920;
        public int videoHeight = 1080;
        public bool recordMicrophone;

        private IMediaRecorder recorder;
        private CameraInput cameraInput;
        private AudioInput audioInput;
        private AudioSource microphoneSource;

        public string path;

        private IEnumerator Start () {
            // Start microphone
            microphoneSource = gameObject.AddComponent<AudioSource>();
            microphoneSource.mute =
            microphoneSource.loop = true;
            microphoneSource.bypassEffects =
            microphoneSource.bypassListenerEffects = false;
            microphoneSource.clip = Microphone.Start(null, true, 10, AudioSettings.outputSampleRate);
            yield return new WaitUntil(() => Microphone.GetPosition(null) > 0);
            microphoneSource.Play();
            
        }

        private void OnDestroy () {
            // Stop microphone
            microphoneSource.Stop();
            Microphone.End(null);
        }

        public void StartRecording () {
            // Start recording
            var frameRate = 30;
            var sampleRate = recordMicrophone ? AudioSettings.outputSampleRate : 0;
            var channelCount = recordMicrophone ? (int)AudioSettings.speakerMode : 0;
            //var FixedInterval = 10;
            // var clock = new RealtimeClock();
            var clock = new FixedIntervalClock(90);
            recorder = new MP4Recorder(videoWidth, videoHeight, frameRate, sampleRate, channelCount, bitrate:6_000_000, keyframeInterval:3);
            // Create recording inputs
            cameraInput = new CameraInput(recorder, clock, Camera.main);
            audioInput = recordMicrophone ? new AudioInput(recorder, clock, microphoneSource, true) : null;
            // Unmute microphone
            microphoneSource.mute = audioInput == null;
        }

        public async void StopRecording () {
            // Mute microphone
            microphoneSource.mute = true;
            // Stop recording
            audioInput?.Dispose();
            cameraInput.Dispose();
            path = await recorder.FinishWriting();
            // Playback recording
            Debug.Log($"Saved recording to: {path}");
            Handheld.PlayFullScreenMovie($"file://{path}");

            //save media file

            new SavePayload().AddMedia(path).Commit();
        }
    }
}