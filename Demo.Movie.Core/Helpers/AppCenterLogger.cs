using System;
using System.Collections.Generic;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace Demo.Movie.Core.Helpers
{
    public static class AppCenterLogger
    {
        /// <summary>
        /// Log event
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="action"></param>
        /// <param name="additionalDetails"></param>
        public static void TrackEvent(string obj, string action, Dictionary<string, string> additionalDetails = null)
        {
            if (additionalDetails == null)
            {
                additionalDetails = new Dictionary<string, string>();
            }

            additionalDetails.Add("action", action);

            Analytics.TrackEvent(obj, additionalDetails);
        }

        /// <summary>
        /// Log error
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="action"></param>
        /// <param name="exception"></param>
        /// <param name="additionalDetails"></param>
        public static void TrackError(string obj, string action, Exception exception, Dictionary<string, string> additionalDetails = null)
        {
            if (additionalDetails == null)
            {
                additionalDetails = new Dictionary<string, string>();
            }

            additionalDetails.Add("object", obj);
            additionalDetails.Add("action", action);

            Crashes.TrackError(exception, additionalDetails);
        }
    }
}
