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

    [DataContract]
    public class GeocodeFarmModel
    {
        [DataMember(Name = "geocoding_results")]
        public GeocodingResults Result { get; set; }
    }

    [DataContract]
    public class GeocodingResults
    {
        [DataMember(Name = "LEGAL_COPYRIGHT")]
        public IDictionary<string, string> Copyright { get; set; }

        [DataMember(Name = "STATUS")]
        public GeocodingStatus Status { get; set; }

        [DataMember(Name = "ACCOUNT")]
        public GeocodingAccount Account { get; set; }

        [DataMember(Name = "RESULTS")]
        public IList<GeocodingResult> Results { get; set; }
    }

    [DataContract]
    public class GeocodingStatus
    {
        [DataMember(Name = "access")]
        public string Access { get; set; }

        [DataMember(Name = "status")]
        public string Status { get; set; }

        [DataMember(Name = "address_provided")]
        public string AddressProvided { get; set; }

        [DataMember(Name = "result_count")]
        public int? ResultCount { get; set; }

        [IgnoreDataMember]
        public string[] Statuses { get; set; }
    }

    [DataContract]
    public class GeocodingAccount
    {
        [DataMember(Name = "ip_address")]
        public string IpAddress { get; set; }

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

        [DataMember(Name = "locality")]
        public string Locality { get; set; }

        [DataMember(Name = "NEIGHBORHOOD")]
        public string Neighborhood { get; set; }

        [DataMember(Name = "admin_1")]
        public string Admin1 { get; set; }

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

        [DataMember(Name = "timezone_long")]
        public string TimezoneName { get; set; }

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
        EXACT_MATCH,
        HIGH_ACCURACY,
        MEDIUM_ACCURACY,
        UNKNOWN_ACCURACY
    }
}
