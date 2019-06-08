using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace FaceSample.Models.Request
{
    public class FaceDetectRequest
    {
        public bool FaceId => true;
        public bool FaceLandmarks => true;
        public FaceAttribute FaceAttributes { get; set; }
        public RecognitionModel RecognitionModel { get; set; }

        public FaceDetectRequest()
        {
            FaceAttributes = new FaceAttribute();
            RecognitionModel = RecognitionModel.recognition_02;
        }
    }

    public class FaceAttribute : IEnumerable
    {
        private void Set(bool value)
        {
            StackTrace stackTrace = new StackTrace();
            var prop = stackTrace.GetFrame(1).GetMethod().Name.Split('_')[1];
            if (value)
                attributes.Add(prop);
        }
        private List<string> attributes;
        public FaceAttribute()
        {
            attributes = new List<string>();
        }
        private bool age;
        private bool gender;
        private bool headPose;
        private bool smile;
        private bool facialHair;
        private bool glasses;
        private bool emotion;
        private bool hair;
        private bool makeup;
        private bool occlusion;
        private bool accessories;
        private bool blur;
        private bool exposure;
        private bool noise;

        public bool Age
        {
            get
            { return age; }
            set
            {
                age = value;
                Set(age);
            }
        }
        public bool Gender { get => gender;
            set
            {
                gender = value;
                Set(gender);
            }
        }
        public bool HeadPose { get => headPose;
            set
            {
                headPose = value;
                Set(headPose);
            }
        }
        public bool Smile { get => smile;
            set
            {
                smile = value;
                Set(smile);
            }
        }
        public bool FacialHair { get => facialHair;
            set
            {
                facialHair = value;
                Set(facialHair);
            }
        }
        public bool Glasses { get => glasses;
            set
            {
                glasses = value;
                Set(glasses);
            }
        }
        public bool Emotion { get => emotion;
            set
            {
                emotion = value;
                Set(emotion);
            }
        }
        public bool Hair { get => hair;
            set
            {
                hair = value;
                Set(hair);
            }
        }
        public bool Makeup { get => makeup;
            set
            {
                makeup = value;
                Set(makeup);
            }
        }
        public bool Occlusion { get => occlusion;
            set
            {
                occlusion = value;
                Set(occlusion);
            }
        }
        public bool Accessories { get => accessories;
            set
            {
                accessories = value;
                Set(accessories);
            }
        }
        public bool Blur { get => blur;
            set
            {
                blur = value;
                Set(blur);
            }
        }
        public bool Exposure { get => exposure;
            set
            {
                exposure = value;
                Set(exposure);
            }
        }
        public bool Noise { get => noise;
            set
            {
                noise = value;
                Set(noise);
            }
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var item in attributes)
            {
                yield return item;
            }
        }
    }

    public enum RecognitionModel
    {
        recognition_01,
        recognition_02
    }
}