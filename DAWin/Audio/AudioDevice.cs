using DAWin.Core;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAWin.Audio
{
    public class AudioDevice
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

        private MMDevice? device_;
        private DeviceStatus deviceStatus_ = DeviceStatus.Uninitialized;
        private WasapiPlayer? wOut_;
        private BufferedWaveProvider? bufferedWP_;
        public string DeviceName => device_?.FriendlyName ?? "Unknown";


        public AudioDevice(MMDevice device)
        {
            // Store device
            device_ = device;
        }
        public void InitializeDevice(AudioEngine.EngineSettings eSettings) 
        {
            //  Dont allow reinitialization if already initialized
            if (deviceStatus_ != DeviceStatus.Uninitialized) 
            {
                Logger.LogWarning("Device cannot be reinitialized.", 2100);
                return;
            }

            wOut_ = new WasapiPlayerBuilder().WithDevice(device_).Build();
            bufferedWP_ = new BufferedWaveProvider(new WaveFormat(eSettings.sampleRate_, 16, eSettings.channelCount_));
            wOut_.Init(bufferedWP_);

            deviceStatus_ = DeviceStatus.Initialized;
        }

        public void StartDevice() 
        {
            if (deviceStatus_ != DeviceStatus.Initialized && deviceStatus_ != DeviceStatus.Off) 
            {
                return;
            }

            // Start 
        }
        public void StopDevice() 
        {
            if (deviceStatus_ != DeviceStatus.On) 
            {
                return;
            }
        }
        public void DisposeDevice() 
        {
            if(device_ == null) 
            {
                return;
            }

            device_.Dispose();
        }
    }
}
