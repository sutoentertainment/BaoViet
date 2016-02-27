using BaoVietCore.Helpers;
using BaoVietCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Media.Capture;
using Windows.Media.Devices;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace BaoVietCore.Services
{
    public class BasicCameraService : ServiceBase, ICameraService
    {
        CaptureElement captureElement;
        MediaCapture mediaCapture;
        private MediaEncodingProfile activeProfile;
        private bool isRecording = false;
        private bool prepareRecording = false;
        private bool prepareStopRecording = false;
        private bool isPreviewing = false;
        private bool preparePreviewing = false;

        private bool initialized = false;
        private MediaCaptureInitializationSettings captureInitSettings;
        private LowLagMediaRecording lowlag;

        public bool IsBackCamera { get; set; } = true;

        public List<DeviceInformation> CameraDeviceList { get; private set; }
        public List<DeviceInformation> AudioDeviceList { get; private set; }
        private bool isFocusing = false;

        public bool IsCameraAvailable
        {
            get
            {
                return CameraDeviceList?.Count > 0;
            }
        }

        public BasicCameraService(Manager man) : base(man)
        {

        }

        public BasicCameraService()
        {

        }

        ~BasicCameraService()
        {
            Dispose(false);
        }

        /// <summary>
        /// To save time for camera opening, call this before <see cref="InitialAsync"/> at any point in life cycle of application
        /// </summary>
        /// <returns></returns>
        public async Task PrepareInitAsync()
        {
            await EnumerateCameras();
        }

        public async Task InitialAsync(CaptureElement captureElement, MediaEncodingProfile profile = null, bool BackCamera = false)
        {
            if (initialized)
                Dispose(true);
            initialized = true;

            this.captureElement = captureElement;
            this.activeProfile = profile;
            if (CameraDeviceList == null)
                await EnumerateCameras();

            mediaCapture = null;
            mediaCapture = new MediaCapture();

            IsBackCamera = BackCamera;
            captureInitSettings = InitCaptureSettings(IsBackCamera);
            await mediaCapture.InitializeAsync(captureInitSettings);
            mediaCapture.CameraStreamStateChanged += MediaCapture_CameraStreamStateChanged;
            mediaCapture.Failed += MediaCapture_Failed;
            mediaCapture.FocusChanged += MediaCapture_FocusChanged;
            mediaCapture.PhotoConfirmationCaptured += MediaCapture_PhotoConfirmationCaptured;
            mediaCapture.RecordLimitationExceeded += MediaCapture_RecordLimitationExceeded;
            mediaCapture.ThermalStatusChanged += MediaCapture_ThermalStatusChanged;
            await SetResolutionAsync();

            if (Manager.DeviceService.CurrentDevice() == DeviceTypes.Mobile)
            {
                if (IsBackCamera)
                {
                    mediaCapture.SetPreviewRotation(VideoRotation.Clockwise90Degrees);
                }
                else
                {
                    mediaCapture.SetPreviewRotation(VideoRotation.Clockwise270Degrees);
                }
            }
            activeProfile = CreateProfile();
            mediaCapture.VideoDeviceController.PrimaryUse = CaptureUse.Video;

            captureElement.Source = mediaCapture;
        }

        /// <summary>
        /// Create activeProfile here
        /// </summary>
        /// <param name="isBackCamera"></param>
        protected virtual MediaEncodingProfile CreateProfile()
        {
            var _activeProfile = MediaEncodingProfile.CreateMp4(VideoEncodingQuality.Vga);
            int MFVideoRotation;
            Guid MFVideoRotationGuid = new Guid("C380465D-2271-428C-9B83-ECEA3B4A85C1"); // MF_MT_VIDEO_ROTATION in Mfapi.h


            if (IsBackCamera)
            {
                MFVideoRotation = ConvertVideoRotationToMFRotation(VideoRotation.Clockwise90Degrees);
            }
            else
            {
                MFVideoRotation = ConvertVideoRotationToMFRotation(VideoRotation.Clockwise270Degrees);
            }

            _activeProfile.Video.FrameRate.Denominator = 1;
            _activeProfile.Video.FrameRate.Numerator = 30;
            _activeProfile.Video.ProfileId = H264ProfileIds.Baseline;
            if (Manager.DeviceService.CurrentDevice() == DeviceTypes.Mobile)
                _activeProfile.Video.Properties.Add(MFVideoRotationGuid, PropertyValue.CreateInt32(MFVideoRotation));

            _activeProfile.Video.Bitrate = 1048576 * 2;
            _activeProfile.Video.Width = 1024;
            _activeProfile.Video.Height = 768;
            _activeProfile.Audio.Bitrate = 128000;

            return _activeProfile;
        }

        int ConvertVideoRotationToMFRotation(VideoRotation rotation)
        {
            int MFVideoRotation = 0; // MFVideoRotationFormat::MFVideoRotationFormat_0 in Mfapi.h
            switch (rotation)
            {
                case VideoRotation.Clockwise90Degrees:
                    MFVideoRotation = 90; // MFVideoRotationFormat::MFVideoRotationFormat_90;
                    break;
                case VideoRotation.Clockwise180Degrees:
                    MFVideoRotation = 180; // MFVideoRotationFormat::MFVideoRotationFormat_180;
                    break;
                case VideoRotation.Clockwise270Degrees:
                    MFVideoRotation = 270; // MFVideoRotationFormat::MFVideoRotationFormat_270;
                    break;
            }

            return MFVideoRotation;
        }

        public async Task SetResolutionAsync(uint width = 1024, uint height = 768)
        {
            uint defaultWidth = 640;
            uint defaultheight = 480;

            var previewRes = this.mediaCapture.VideoDeviceController.GetAvailableMediaStreamProperties(MediaStreamType.VideoPreview);
            int index640Resolution = 0;
            int index480Resolution = 0;

            if (previewRes.Count >= 1)
            {
                for (int i = 0; i < previewRes.Count; i++)
                {
                    VideoEncodingProperties vp = (VideoEncodingProperties)previewRes[i];
                    if (vp.Width == width && vp.Height == height)
                    {
                        index640Resolution = i;
                        break;
                    }
                    if (vp.Width == defaultWidth && vp.Height == defaultheight)
                    {
                        index480Resolution = i;
                    }
                }

                await this.mediaCapture.VideoDeviceController.SetMediaStreamPropertiesAsync(MediaStreamType.VideoPreview, previewRes[index640Resolution]);
            }

            var res = this.mediaCapture.VideoDeviceController.GetAvailableMediaStreamProperties(MediaStreamType.VideoRecord);
            if (res.Count >= 1)
            {
                for (int i = 0; i < res.Count; i++)
                {
                    VideoEncodingProperties vp = (VideoEncodingProperties)res[i];
                    if (vp.Width == width && vp.Height == height)
                    {
                        index640Resolution = i;
                        break;
                    }
                    if (vp.Width == defaultWidth && vp.Height == defaultheight)
                    {
                        index640Resolution = i;
                        break;
                    }
                }
                await this.mediaCapture.VideoDeviceController.SetMediaStreamPropertiesAsync(MediaStreamType.VideoRecord, res[index640Resolution]);
            }


            var Photores = this.mediaCapture.VideoDeviceController.GetAvailableMediaStreamProperties(MediaStreamType.Photo);
            if (Photores.Count >= 1)
            {
                for (int i = 0; i < Photores.Count; i++)
                {
                    VideoEncodingProperties vp = (VideoEncodingProperties)Photores[i];
                    Debug.WriteLine(vp.Width + " x " + vp.Height);

                    //1024 x 768
                    if (vp.Width == width && vp.Height == height)
                    {
                        index640Resolution = i;
                        break;
                    }

                    //default quality
                    if (vp.Width == defaultWidth && vp.Height == defaultheight)
                    {
                        index640Resolution = i;
                        break;
                    }
                }
                await this.mediaCapture.VideoDeviceController.SetMediaStreamPropertiesAsync(MediaStreamType.Photo, Photores[index640Resolution]);
            }
        }


        #region MediaCapture event
        private void MediaCapture_ThermalStatusChanged(MediaCapture sender, object args)
        {
            //TODO: Handle device too hot
        }

        private void MediaCapture_RecordLimitationExceeded(MediaCapture sender)
        {
            //TODO: Handle record limitation exceeded
        }

        private void MediaCapture_PhotoConfirmationCaptured(MediaCapture sender, PhotoConfirmationCapturedEventArgs args)
        {

        }

        private void MediaCapture_FocusChanged(MediaCapture sender, MediaCaptureFocusChangedEventArgs args)
        {

        }

        private void MediaCapture_Failed(MediaCapture sender, MediaCaptureFailedEventArgs errorEventArgs)
        {
            //TODO: Handle failure during media capture
        }

        private void MediaCapture_CameraStreamStateChanged(MediaCapture sender, object args)
        {

        }

        #endregion

        /// <summary>
        /// always return false if camera is not initialized
        /// </summary>
        /// <returns></returns>
        public bool IsFlashAvailable()
        {
            if (mediaCapture != null)
                return mediaCapture.VideoDeviceController.TorchControl.Supported;
            return false;
        }

        private MediaCaptureInitializationSettings InitCaptureSettings(bool isBackCamera)
        {
            var initSettings = new MediaCaptureInitializationSettings();
            initSettings.VideoDeviceId = "";
            initSettings.StreamingCaptureMode = StreamingCaptureMode.AudioAndVideo;
            initSettings.PhotoCaptureSource = PhotoCaptureSource.VideoPreview;

            if (CameraDeviceList.Count > 0)
            {
                if (IsBackCamera)
                    initSettings.VideoDeviceId = CameraDeviceList[0].Id;
                else
                    initSettings.VideoDeviceId = CameraDeviceList[1].Id;
            }
            else
                initSettings.VideoDeviceId = CameraDeviceList[0].Id;
            return initSettings;
        }

        public void SetActiveProfile(MediaEncodingProfile profile)
        {
            this.activeProfile = profile;
        }


        #region Camera Action
        public async Task StartPreviewAsync()
        {
            preparePreviewing = true;
            await mediaCapture.StartPreviewAsync();
            isPreviewing = true;
            preparePreviewing = false;
        }
        public async Task StopPreviewAsync()
        {
            isPreviewing = false;
            await mediaCapture.StopPreviewAsync();
        }

        public async Task StartRecordingToFileAsync(StorageFile file)
        {
            prepareRecording = true;
            await mediaCapture?.StartRecordToStorageFileAsync(activeProfile, file);
            prepareRecording = false;
            isRecording = true;
        }

        public async Task PauseRecordingAsync(MediaCapturePauseBehavior b)
        {
            await mediaCapture.PauseRecordAsync(b);
        }
        public async Task ResumeRecordingAsync()
        {
            await mediaCapture.ResumeRecordAsync();
        }
        public async Task StopRecordingAsync()
        {
            prepareStopRecording = true;
            await mediaCapture?.StopRecordAsync();
            prepareStopRecording = false;
            isRecording = false;
        }

        public async Task PrepareLowLagRecordingToFileAsync(StorageFile file)
        {
            lowlag = await mediaCapture.PrepareLowLagRecordToStorageFileAsync(activeProfile, file);
        }

        public async Task StartLowLagRecordingAsync()
        {
            await lowlag.StartAsync();
        }
        public async Task StopLowLagRecordingAsync()
        {
            await lowlag.StopAsync();
        }

        public async Task PauseLowLagAsync(MediaCapturePauseBehavior b)
        {
            await lowlag.PauseAsync(b);
        }
        public async Task ResumeLowLagAsync()
        {
            await lowlag.ResumeAsync();
        }
        public async Task DisposeLowLagAsync()
        {
            await lowlag.FinishAsync();
        }
        public void SetFlashStatus(bool flashStatus)
        {
            if (prepareRecording || prepareStopRecording || isRecording)
                return;

            if (IsFlashAvailable())
            {
                mediaCapture.VideoDeviceController.TorchControl.Enabled = flashStatus;
            }
        }

        public async void FocusOnTap(TappedRoutedEventArgs e)
        {
            if (isFocusing || !IsBackCamera)
                return;
            isFocusing = true;
            Point tappoint = e.GetPosition(captureElement);
            Point positionPercentage = new Point(tappoint.X / captureElement.ActualWidth, tappoint.Y / captureElement.ActualHeight);
            bool FocusAtPointSupported = mediaCapture.VideoDeviceController.RegionsOfInterestControl.AutoFocusSupported && mediaCapture.VideoDeviceController.RegionsOfInterestControl.MaxRegions > 0;
            if (!FocusAtPointSupported)
            {
                //throw new InvalidOperationException("Focus at point is not supported by the current device.");
            }

            if (positionPercentage.X < 0.0 || positionPercentage.X > 1.0)
            {
                //throw new ArgumentOutOfRangeException("focusPoint", positionPercentage, "Horizontal location in the viewfinder should be between 0.0 and 1.0.");
            }

            if (positionPercentage.Y < 0.0 || positionPercentage.Y > 1.0)
            {
                //throw new ArgumentOutOfRangeException("focusPoint", positionPercentage, "Vertical location in the viewfinder should be between 0.0 and 1.0.");
            }

            double x = positionPercentage.X;
            double y = positionPercentage.Y;
            double epsilon = 0.01;


            if (x >= 1.0 - epsilon)
            {
                x = 1.0 - 2 * epsilon;
            }

            if (y >= 1.0 - 0.01)
            {
                y = 1.0 - 2 * epsilon;
            }

            FocusSettings focusSettings = new FocusSettings { Mode = FocusMode.Single, AutoFocusRange = AutoFocusRange.FullRange };

            this.mediaCapture.VideoDeviceController.FocusControl.Configure(focusSettings);

            await this.mediaCapture.VideoDeviceController.RegionsOfInterestControl.SetRegionsAsync(
                new[]
                {
                    new RegionOfInterest
                    {
                        Type             = RegionOfInterestType.Unknown,
                        Bounds           = new Windows.Foundation.Rect(x, y, epsilon, epsilon),
                        BoundsNormalized = true,
                        AutoFocusEnabled = true,
                        Weight           = 1
                    }
                });

            await this.mediaCapture.VideoDeviceController.FocusControl.FocusAsync();
            isFocusing = false;
        }

        #endregion

        private async Task EnumerateCameras()
        {
            var devices = await DeviceInformation.FindAllAsync(DeviceClass.VideoCapture);

            var audioDevices = await DeviceInformation.FindAllAsync(DeviceClass.AudioCapture);

            CameraDeviceList = new List<DeviceInformation>();
            AudioDeviceList = new List<DeviceInformation>();
            // Add the devices to deviceList
            if (devices.Count > 0)
            {
                for (var i = 0; i < devices.Count; i++)
                {
                    CameraDeviceList.Add(devices[i]);
                    //Debug.WriteLine("Added new device: " + devices[i].Name);
                }
                //Debug.WriteLine("Initialization complete.");

                if (devices.Count >= 2)
                {
                    if (devices[0].EnclosureLocation.Panel == Windows.Devices.Enumeration.Panel.Front)
                    {
                        CameraDeviceList[0] = devices[1];
                        CameraDeviceList[1] = devices[0];
                    }
                }
            }
            else
            {
                //Debug.WriteLine("No camera device is found ");
            }

            if (audioDevices.Count > 0)
            {
                for (var i = 0; i < audioDevices.Count; i++)
                {
                    AudioDeviceList.Add(audioDevices[i]);
                    //Debug.WriteLine("Added new device: " + audioDevices[i].Name);
                }
                //Debug.WriteLine("Initialization complete.");

                if (audioDevices.Count >= 2)
                {
                    for (int i = 0; i < audioDevices.Count; i++)
                    {
                        if (audioDevices[i].Name.StartsWith("Microphone", StringComparison.Ordinal))
                        {
                            AudioDeviceList[0] = audioDevices[i];
                        }
                    }
                }
            }
            else
            {
                //Debug.WriteLine("No camera device is found ");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool threadSafe)
        {
            //Called at run time by user, use to clear managed resource
            if (threadSafe)
            {
                if (isRecording)
                {
                    //try
                    //{
                    //    await StopRecordingAsync();
                    //}
                }
                if (isPreviewing)
                {
                    //try
                    //{
                    //    await StopPreviewAsync();
                    //}
                }
                if (this.captureElement != null)
                {
                    this.captureElement.Source = null;
                    this.captureElement = null;
                }


                if (lowlag != null)
                {
                    //try
                    //{
                    //    await lowlag.FinishAsync();
                    //}
                    lowlag = null;
                }
            }
            else
            {
                //Clear unmanaged resource here
            }

            if (this.mediaCapture != null)
            {
                mediaCapture.CameraStreamStateChanged -= MediaCapture_CameraStreamStateChanged;
                mediaCapture.Failed -= MediaCapture_Failed;
                mediaCapture.FocusChanged -= MediaCapture_FocusChanged;
                mediaCapture.PhotoConfirmationCaptured -= MediaCapture_PhotoConfirmationCaptured;
                mediaCapture.RecordLimitationExceeded -= MediaCapture_RecordLimitationExceeded;
                mediaCapture.ThermalStatusChanged -= MediaCapture_ThermalStatusChanged;
                mediaCapture.Dispose();
                this.mediaCapture = null;
            }

            //Hack to prevent green screen
            GC.Collect();
        }
    }

    public enum FlashState
    {
        On,
        Off,
        Auto
    }

    /// <summary>
    /// Sample of how to exten the basic camera
    /// </summary>
    public class AdvanceCamera : BasicCameraService
    {
        protected override MediaEncodingProfile CreateProfile()
        {
            Debug.WriteLine("Advance");
            return null;
        }
    }
}
