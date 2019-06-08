using FaceSample.Models.Request;
using System;

namespace FaceSample.Helpers
{
    /// <summary>
    /// You must use the Request property for generate a url
    /// </summary>
    public class FaceDetectUrlHelper : UrlGenerator
    {
        public FaceDetectRequest Request { get; set; }
        public override string GenerateUrl()
        {
            string returnFaceAttributes = "";
            foreach (string item in Request.FaceAttributes)
                returnFaceAttributes += $"{item},";

            //lol
            returnFaceAttributes = returnFaceAttributes.Remove(returnFaceAttributes.Length - 1);

            var url = $"detect?returnFaceId={Request.FaceId}&returnFaceLandmarks={Request.FaceLandmarks}&" +
                $"returnFaceAttributes={returnFaceAttributes }&recognitionModel={Request.RecognitionModel}&" +
                $"returnRecognitionModel=true";

            if (String.IsNullOrEmpty(returnFaceAttributes))
                throw new Exception("You must use the Request property for generate a url");

            return url;
        }
    }
}