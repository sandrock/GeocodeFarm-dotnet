// GeocodeFarm-dotnet
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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    //
    // Documentation: https://geocode.farm/geocoding/free-api-documentation/
    // 

    [DataContract]
    public class GeocodeFarmModel
    {
        /// <summary>
        /// The root node. Never null.
        /// </summary>
        [DataMember(Name = "geocoding_results")]
        public GeocodingResults Result { get; set; }
    }

    [DataContract]
    public class GeocodingResults
    {
        /// <summary>
        /// Contains copyright information. Never null.
        /// </summary>
        [DataMember(Name = "LEGAL_COPYRIGHT")]
        public IDictionary<string, string> Copyright { get; set; }

        /// <summary>
        /// Contains query status. Never null.
        /// </summary>
        [DataMember(Name = "STATUS")]
        public GeocodingStatus Status { get; set; }

        /// <summary>
        /// Contains quota and account information. Never null.
        /// </summary>
        [DataMember(Name = "ACCOUNT")]
        public GeocodingAccount Account { get; set; }

        /// <summary>
        /// The results. This is null when status is "FAILED, NO_RESULT".
        /// </summary>
        [DataMember(Name = "RESULTS")]
        public IList<GeocodingResult> Results { get; set; }
    }

    [DataContract]
    public class GeocodingStatus
    {
        /// <summary>
        /// Value on free usage:   "FREE_USER, ACCESS_GRANTED"
        /// Valid API key:         "KEY_VALID, ACCESS_GRANTED"
        /// Invalid API key:       "API_KEY_INVALID"
        /// Account not confirmed: "ACCOUNT_NOT_ACTIVE"
        /// Bill past due:         "BILL_PAST_DUE"
        /// Quota reached:         "OVER_QUERY_LIMIT"
        /// </summary>
        [DataMember(Name = "access")]
        public string Access { get; set; }

        /// <summary>
        /// OK address:    "SUCCESS"
        /// Bad address:   "FAILED, NO_RESULTS"
        /// Quota reached: "FAILED, ACCESS_DENIED"
        /// </summary>
        [DataMember(Name = "status")]
        public string Status { get; set; }

        /// <summary>
        /// Null when "NO_RESULTS".
        /// </summary>
        [DataMember(Name = "address_provided")]
        public string AddressProvided { get; set; }

        /// <summary>
        /// Null when "NO_RESULTS".
        /// </summary>
        [DataMember(Name = "result_count")]
        public int? ResultCount { get; set; }

        [IgnoreDataMember]
        public string[] Statuses { get; set; }
    }

    [DataContract]
    public class GeocodingAccount
    {
        /// <summary>
        /// Empty when "NO_RESULTS".
        /// </summary>
        [DataMember(Name = "ip_address")]
        public string IpAddress { get; set; }

        /// <summary>
        /// Free usage: "NONE, UNLICENSED"
        /// </summary>
        [DataMember(Name = "distribution_license")]
        public string DistributionLicense { get; set; }

        [DataMember(Name = "usage_limit")]
        public int? UsageLimit { get; set; }

        [DataMember(Name = "used_today")]
        public int? UsedToday { get; set; }

        [DataMember(Name = "used_total")]
        public int? UsedTotal { get; set; }

        [DataMember(Name = "first_used")]
        public string FirstUsed { get; set; }

        /// <summary>
        /// Computed number of remaining queries. Returns null when no quota.
        /// </summary>
        [IgnoreDataMember]
        public int? Remaining
        {
            get
            {
                if (this.UsedToday != null && this.UsageLimit != null)
                {
                    return this.UsageLimit.Value - this.UsedToday.Value;
                }
                else
                {
                    return null;
                }
            }
        }
    }

    /// <summary>
    /// Represents a geocode result line.
    /// </summary>
    [DataContract]
    public class GeocodingResult
    {
        [DataMember(Name = "result_number")]
        public int ResultNumber { get; set; }

        [DataMember(Name = "formatted_address")]
        public string FormattedAddress { get; set; }

        [DataMember(Name = "accuracy")]
        public string Accuracy { get; set; }

        public GeocodeFarmAccuracy? AccuracyValue
        {
            get
            {
                GeocodeFarmAccuracy val;
                if (Enum.TryParse(this.Accuracy, out val))
                    return val;

                return default(GeocodeFarmAccuracy?);
            }
        }

        [DataMember(Name = "ADDRESS")]
        public GeocodingAddress Address { get; set; }

        [DataMember(Name = "LOCATION_DETAILS")]
        public GeocodingDetails LocationDetails { get; set; }

        [DataMember(Name = "COORDINATES")]
        public GeocodingCoordinates Coordinates { get; set; }

        [DataMember(Name = "BOUNDARIES")]
        public GeocodingBoundaries Boundaries { get; set; }
    }

    [DataContract]
    public class GeocodingCoordinates
    {
        [DataMember(Name = "latitude")]
        public decimal Latitude { get; set; }

        [DataMember(Name = "longitude")]
        public decimal Longitude { get; set; }
    }

    [DataContract]
    public class GeocodingAddress
    {
        [DataMember(Name = "street_number")]
        public string StreetNumber { get; set; }

        [DataMember(Name = "street_name")]
        public string StreetName { get; set; }

        /// <summary>
        /// Indicates an incorporated city or town political entity.
        /// </summary>
        [DataMember(Name = "locality")]
        public string Locality { get; set; }

        [DataMember(Name = "NEIGHBORHOOD")]
        public string Neighborhood { get; set; }

        /// <summary>
        /// Indicates a first-order civil entity below the country level.
        /// </summary>
        [DataMember(Name = "admin_1")]
        public string Admin1 { get; set; }

        /// <summary>
        /// Indicates a second-order civil entity below the country level.
        /// </summary>
        [DataMember(Name = "admin_2")]
        public string Admin2 { get; set; }

        [DataMember(Name = "postal_code")]
        public string PostalCode { get; set; }

        [DataMember(Name = "country")]
        public string Country { get; set; }
    }

    [DataContract]
    public class GeocodingDetails
    {
        /// <summary>
        /// Indicates the elevation of the point in US Standard Feet.
        /// The value may be 'UNAVAILABLE'.
        /// </summary>
        [DataMember(Name = "elevation")]
        public string Elevation { get; set; }

        /// <summary>
        /// The full human-readable timezone name for the returned point.
        /// </summary>
        [DataMember(Name = "timezone_long")]
        public string TimezoneName { get; set; }

        /// <summary>
        /// The short or abreviated timezone name. Mostly as used by *NIX systems.
        /// </summary>
        [DataMember(Name = "timezone_short")]
        public string TimezoneCode { get; set; }
    }

    [DataContract]
    public class GeocodingBoundaries
    {
        [DataMember(Name = "northeast_latitude")]
        public double? NorthEastLatitude { get; set; }

        [DataMember(Name = "northeast_longitude")]
        public double? NorthEastLongitude { get; set; }

        [DataMember(Name = "southwest_latitude")]
        public double? SouthWestLatitude { get; set; }

        [DataMember(Name = "southwest_longitude")]
        public double? SouthWestLongitude { get; set; }
    }

    public enum GeocodeFarmAccuracy
    {
        /// <summary>
        /// This is the highest level of accuracy and usually indicates a spot-on match.
        /// </summary>
        EXACT_MATCH,

        /// <summary>
        /// This is the second highest level of accuracy and usually indicates a range match, within a few hundred feet most.
        /// </summary>
        HIGH_ACCURACY,

        /// <summary>
        /// This is the third level of accuracy and usually indicates a geographical area match, such as the metro area, town, or city.
        /// </summary>
        MEDIUM_ACCURACY,

        /// <summary>
        /// The accuracy of this result is unable to be determined and an exact match may or may not have been obtained.
        /// </summary>
        UNKNOWN_ACCURACY,
    }
}
