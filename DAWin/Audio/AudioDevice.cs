using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAWin.Audio
{
    internal class AudioDevice
    {
        enum DeviceType 
        {
            Microphone,
            Speaker
        }
        enum DeviceStatus 
        {
            Uninitialized,
            Initialized,
            On,
            Off,
            Error
        }
        struct DeviceIdentity 
        {
            string deviceName_;
            int deviceID_;
        }

        DeviceIdentity deviceIdentity_;
        DeviceType deviceType_;
        DeviceStatus deviceStatus_ = DeviceStatus.Uninitialized;

        WasapiPlayerBuilder? wOut_;
        BufferedWaveProvider? bufferedWP_;

        public AudioDevice(int sampleR_, int channelC_, int bufferS_)
        {
            InitializeDevice(new AudioEngine.EngineSettings { sampleRate_ = sampleR_, channelCount_ = sampleR_, bufferSize_ = sampleR_ });
        }
        void InitializeDevice(AudioEngine.EngineSettings eSettings) 
        {
            //  Dont allow reinitialization if already initialized
            if (deviceStatus_ != DeviceStatus.Uninitialized) 
            {
                return;
            }

            wOut_ = new WasapiPlayerBuilder();

            bufferedWP_ = new BufferedWaveProvider(new WaveFormat(eSettings.sampleRate_, 16, eSettings.channelCount_));
            
            wOut_.Build();

            deviceStatus_ = DeviceStatus.Initialized;
        }

        void StartDevice() 
        {
            if (deviceStatus_ != DeviceStatus.Initialized && deviceStatus_ != DeviceStatus.Off) 
            {
                return;
            }

            // Start 
        }
        void StopDevice() 
        {
            if (deviceStatus_ != DeviceStatus.On) 
            {
                return;
            }
        }
        void DisposeDevice() 
        {

        }
    }
}
