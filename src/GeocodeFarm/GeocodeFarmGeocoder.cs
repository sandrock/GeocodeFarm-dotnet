﻿// GeocodeFarm-dotnet
// Copyright (C) 2015 SandRock
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace GeocodeFarm
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Runtime.Serialization;

    /// <summary>
    /// https://geocode.farm/geocoding/free-api-documentation/
    /// </summary>
    /// <remarks>
    /// Bad address:   <status>FAILED, NO_RESULTS</status>
    /// OK address:    <status>SUCCESS</status>
    /// Quota reached: <status>FAILED, ACCESS_DENIED</status>
    /// </remarks>
    public class GeocodeFarmGeocoder : IGeocodeFarmGeocoder
    {
        private const string StatusOverQueryLimitStatusCode = "OVER_QUERY_LIMIT";
        private const string AccessDeniedStatus = "ACCESS_DENIED";

        private static readonly NumberFormatInfo culture = NumberFormatInfo.InvariantInfo;
        private static string[] throwStatusNames = new string[]
        {
            "FAILED",
            AccessDeniedStatus,
        };

        /// <summary>
        /// Forward geocoding takes a provided address or location and returns the coordinate set for the requested location.
        /// </summary>
        /// <param name="location">The string to search for. Usually a street address. Just be sure to include the country at the end of the address to ensure accurate results.</param>
        /// <returns></returns>
        /// <exception cref="GeocodeFarmGeocoderException"></exception>
        public GeocodeFarmModel Forward(string location)
        {
            return this.Forward(location, 1);
        }

        /// <summary>
        /// Forward geocoding takes a provided address or location and returns the coordinate set for the requested location.
        /// </summary>
        /// <param name="location">The string to search for. Usually a street address. Just be sure to include the country at the end of the address to ensure accurate results.</param>
        /// <param name="count">Default is 1.</param>
        /// <returns></returns>
        /// <exception cref="GeocodeFarmGeocoderException"></exception>
        public GeocodeFarmModel Forward(string location, int count)
        {
            if (string.IsNullOrEmpty(location))
                throw new ArgumentNullException("location");

            var geocodeFarmRequestAddress = "https://www.geocode.farm/v3/json/forward/?addr=" + Uri.EscapeUriString(location) + "&count=" + count;

            var httpRequest = (HttpWebRequest)WebRequest.Create(geocodeFarmRequestAddress);
            try
            {
                using (var httpResponse = (HttpWebResponse)httpRequest.GetResponse())
                using (var responseStream = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var json = responseStream.ReadToEnd();
                    var result = JsonConvert.DeserializeObject<GeocodeFarmModel>(json);
                    VerifyResult(result);
                    return result;
                }
            }
            catch (WebException ex)
            {
                Trace.TraceError("GeocodeFarmGeocoder: web exception for location '" + location + "' " + ex.Message);
                var ex1 = new GeocodeFarmGeocoderException(ex.Message, ex);
                throw ex1;
            }
        }

        /// <summary>
        /// Reverse geocoding takes a provided coordinate set and returns the address for the requested coordinates.
        /// </summary>
        /// <param name="latitude">The numerical latitude value for which you wish to obtain the closest, human-readable address.</param>
        /// <param name="longitude">The numerical longitude value for which you wish to obtain the closest, human-readable address.</param>
        /// <param name="count">Default is 1.</param>
        /// <returns></returns>
        /// <exception cref="GeocodeFarmGeocoderException"></exception>
        public GeocodeFarmModel Reverse(double latitude, double longitude)
        {
            return this.Reverse(latitude, longitude, 1);
        }

        /// <summary>
        /// Reverse geocoding takes a provided coordinate set and returns the address for the requested coordinates.
        /// </summary>
        /// <param name="latitude">The numerical latitude value for which you wish to obtain the closest, human-readable address.</param>
        /// <param name="longitude">The numerical longitude value for which you wish to obtain the closest, human-readable address.</param>
        /// <returns></returns>
        /// <exception cref="GeocodeFarmGeocoderException"></exception>
        public GeocodeFarmModel Reverse(double latitude, double longitude, int count)
        {
            var geocodeFarmRequestAddress = "https://www.geocode.farm/v3/json/reverse/"
                + "?lat=" + latitude.ToString("R", culture)
                + "&lon=" + longitude.ToString("R", culture)
                + "&count=" + count;

            var httpRequest = (HttpWebRequest)WebRequest.Create(geocodeFarmRequestAddress);
            try
            {
                using (var httpResponse = (HttpWebResponse)httpRequest.GetResponse())
                using (var responseStream = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = JsonConvert.DeserializeObject<GeocodeFarmModel>(responseStream.ReadToEnd());
                    VerifyResult(result);
                    return result;
                }
            }
            catch (WebException ex)
            {
                Trace.TraceError("GeocodeFarmGeocoder: web exception for reverse '" + latitude.ToString("R", culture) + ", " + longitude.ToString("R", culture) + "' " + ex.Message);
                var ex1 = new GeocodeFarmGeocoderException(ex.Message, ex);
                throw ex1;
            }
        }

        private static void VerifyResult(GeocodeFarmModel result)
        {
            if (result == null || result.Result == null || result.Result.Status == null)
            {
                var ex1 = new GeocodeFarmGeocoderException("Service response does not contain the expected data.");
                ex1.Result = result;

                throw ex1;
            }

            if ("FAILED, NO_RESULTS".Equals(result.Result.Status.Status))
            {
                return;
            }

            var status = result.Result.Status;
            if (status.Status != null)
            {
                var statuses = status.Status.Split(new char[] { ',', ' ', }, StringSplitOptions.RemoveEmptyEntries);
                status.Statuses = statuses;
                if (statuses.Any(s => throwStatusNames.Contains(s)))
                {
                    var ex1 = new GeocodeFarmGeocoderException("Query failed with service status: " + status.Status + " and access:" + status.Access);
                    ex1.Result = result;
                    ex1.RequestStatus = status.Status;
                    ex1.RequestAccess = status.Access;

                    if (StatusOverQueryLimitStatusCode.Equals(status.Access))
                    {
                        ex1.IsQuotaReached = true;
                    }

                    throw ex1;
                }
            }
        }
    }
}
