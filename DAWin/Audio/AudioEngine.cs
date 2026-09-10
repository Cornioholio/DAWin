using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using NAudio.Wave;

namespace DAWin.Audio
{
    internal class AudioEngine
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
        struct EngineSettings
        {
            // Number of times per second the audio engine processes audio data. (in Hz)
            public int sampleRate;
            // Number of audio channels. (1 for mono, 2 for stereo)
            public int channelCount;
            // Number of samples processed per audio callback.
            public int bufferSize;
        }

        EngineSettings audioEngineSettings;
        EngineState audioEngineState;

        // Called upon application load
        void InitializeAudioEngine() 
        {
            // Check if the audio engine is already initialized
            if (audioEngineState != EngineState.Uninitialized)
            {
                return;
            }

            // Set uninitialized state before initializing, set initialized after complete
            audioEngineState = EngineState.Uninitialized;

            audioEngineSettings.sampleRate = 48000; // 48.0 kHz
            audioEngineSettings.channelCount = 2; // Stereo
            audioEngineSettings.bufferSize = 256;

            
            audioEngineState = EngineState.Initialized;
        }

        // Called upon playback start
        void StartAudioEngine()
        {
            // Check if the audio engine is anything but initialized
            if (audioEngineState != EngineState.Initialized && audioEngineState != EngineState.Stopped) 
            {
                return;
            }   

            // Logic to add 
            audioEngineState = EngineState.Running;
        }

        // Called upon playback stop
        void StopAudioEngine() 
        {
            // Check if the audio engine is not already running
            if (audioEngineState != EngineState.Running) 
            {
                return;
            }

            // Logic to add 

            audioEngineState = EngineState.Stopped;
        }

        void ShutdownAudioEngine() 
        {
            // Check if the audio engine is even initialized
            if (audioEngineState == EngineState.Uninitialized)
            {
                return;
            }

            // Logic to add, release resources etc.

            audioEngineState = EngineState.Disposed;
        }
    }
}
