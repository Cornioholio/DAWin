using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using NAudio.Wave;
using NAudio.CoreAudioApi;
using DAWin.Core;

namespace DAWin.Audio
{
    public class AudioEngine
    {
        enum EngineState 
        {
            Uninitialized,
            Initialized,
            Running,
            Stopped,
            Error,
            Disposed
        }
        public struct EngineSettings
        {
            // Number of times per second the audio engine processes audio data. (in Hz)
            public int sampleRate_;
            // Number of audio channels. (1 for mono, 2 for stereo)
            public int channelCount_;
            // Number of samples processed per audio callback.
            public int bufferSize_;
        }

        EngineSettings audioEngineSettings_;
        EngineState audioEngineState_ = EngineState.Uninitialized;

        private List<AudioDevice> audioDevices_ = new List<AudioDevice>();
        public IReadOnlyList<AudioDevice> AudioDevices => audioDevices_;

        private AudioDevice? activeDevice_;

        // Called upon application load
        public void InitializeAudioEngine() 
        {
            // Check if the audio engine is already initialized
            if (audioEngineState_ != EngineState.Uninitialized)
            {
                Logger.LogWarning("Audio engine is already initialized.", 2000);
                return;
            }

            Logger.LogInfo("Initializing audio engine...", 1000);

            audioEngineSettings_.sampleRate_ = 48000; // 48.0 kHz
            audioEngineSettings_.channelCount_ = 2; // Stereo
            audioEngineSettings_.bufferSize_ = 256;

            Logger.LogInfo("Engine audio settings configured.", 1001);

            DetectAudioDevices();

            audioEngineState_ = EngineState.Initialized;

            Logger.LogInfo("AudioEngine initialized successfully.", 1002);
        }
        // Called upon playback start
        void StartAudioEngine()
        {
            // Check if the audio engine is anything but initialized and stopped
            if (audioEngineState_ != EngineState.Initialized && audioEngineState_ != EngineState.Stopped) 
            {
                Logger.LogWarning("Audio engine cannot start from its current state.", 2001);
                return;
            }

            Logger.LogInfo("Starting audio engine.", 1003);

            // Logic to add 
            audioEngineState_ = EngineState.Running;

            Logger.LogInfo("Audio engine started successfully and running.", 1004);
        }
        // Called upon playback stop
        void StopAudioEngine() 
        {
            // Check if the audio engine is not already running
            if (audioEngineState_ != EngineState.Running) 
            {
                Logger.LogWarning("Audio engine cannot stop from its current state.", 2002);
                return;
            }

            Logger.LogInfo("Stopping audio engine.", 1005);

            
            // Logic to add 

            audioEngineState_ = EngineState.Stopped;

            Logger.LogInfo("Audio engine stopped.", 1006);
        }
        // Called upon application close
        void ShutdownAudioEngine() 
        {
            // Check if the audio engine is even initialized
            if (audioEngineState_ == EngineState.Uninitialized || audioEngineState_ == EngineState.Disposed)
            {
                Logger.LogWarning("Audio engine cannot shutdown from its current state.", 2003);
                return;
            }

            Logger.LogInfo("Shutting audio engine down.", 1007);

            // Logic to add, release resources etc.

            audioEngineState_ = EngineState.Disposed;

            Logger.LogInfo("Audio engine shutdown and disposed.", 1008);
        }
        // Called upon 
        public void DetectAudioDevices() 
        {
            Logger.LogInfo("Engine detecting audio output devices...", 1009);

            using MMDeviceEnumerator enumerator = new MMDeviceEnumerator();

            var devices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);

            audioDevices_.Clear();

            foreach (MMDevice device in devices) 
            {
                AudioDevice audioDevice = new AudioDevice(device);

                audioDevices_.Add(audioDevice);

                Logger.LogInfo($"Engine detected audio device: {device.FriendlyName}.", 1010);
            }

            Logger.LogInfo($"Detected {audioDevices_.Count} audio output device(s).", 1011);
        }
        public void InitializeActiveDevice(AudioDevice device)
        {
            if (audioEngineState_ != EngineState.Initialized)
            {
                Logger.LogWarning("Audio engine must be initialized before initializing a device.", 2004);
                return;
            }

            if (!audioDevices_.Contains(device)) 
            {
                Logger.LogWarning("The selected device is not registered with the audio engine.", 2005);
                return;
            } 

            if (activeDevice_ != null) 
            {
                Logger.LogInfo("Reselecting audio device to desired device.", 1012);
                activeDevice_ = null;
            }

            device.InitializeDevice(audioEngineSettings_);
            activeDevice_ = device;
                
            Logger.LogInfo($"Audio engine initialized successfully: {device.DeviceName}.", 1013);
        }
    }
}
