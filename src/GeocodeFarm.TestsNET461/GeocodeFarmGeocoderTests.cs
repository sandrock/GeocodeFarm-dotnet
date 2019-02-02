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

#if NET
namespace GeocodeFarm.TestsNET461
#else
namespace GeocodeFarm.TestsDNC
#endif
{
    using System;
    using Xunit;

    public class GeocodeFarmGeocoderTests
    {
        [Fact]
        public void Ctor_DoesNotCrash()
        {
            var geocoder = new GeocodeFarmGeocoder(false);
        }

        [Fact]
        public void Forward_ValidAddress_ValidResult()
        {
            var location = "Friedrichstr. 123, 10117 Berlin, Deutschland";
            var target = GetTarget();
            var result = target.Forward(location, 3);

            Assert.NotNull(result);

            Assert.Equal("Friedrichstraße 123, 10117 Berlin, Germany, Germany", result.Results[0].FormattedAddress);
            Assert.Equal("EXACT_MATCH", result.Results[0].Accuracy);
            Assert.Equal("123", result.Results[0].Address.StreetNumber);
            Assert.Equal("Friedrichstraße", result.Results[0].Address.StreetName);
            Assert.Equal("Berlin", result.Results[0].Address.Locality);
            Assert.Null(result.Results[0].Address.Neighborhood);
            Assert.Equal("BE", result.Results[0].Address.Admin1);
            Assert.Equal("Stadt Berlin", result.Results[0].Address.Admin2);
            Assert.Equal("10117", result.Results[0].Address.PostalCode);
            Assert.Equal("Germany", result.Results[0].Address.Country);
            Assert.Equal("UNAVAILABLE", result.Results[0].LocationDetails.Elevation);
            Assert.Equal("UNAVAILABLE", result.Results[0].LocationDetails.TimezoneName);
            Assert.Equal("Europe/Berlin", result.Results[0].LocationDetails.TimezoneCode);
            Assert.Equal(52.5268605894246M, result.Results[0].Coordinates.Latitude);
            Assert.Equal(13.3862193324441M, result.Results[0].Coordinates.Longitude);

            Assert.Equal("FREE_USER, ACCESS_GRANTED", result.Status.Access);
            Assert.Equal("SUCCESS", result.Status.Status);
            Assert.Equal("NONE, UNLICENSED", result.Account.DistributionLicense);

            Assert.Equal("Copyright (c) 2019 Geocode.Farm - All Rights Reserved.", result.Copyright["copyright_notice"]);
            Assert.Equal("https://www.geocode.farm/images/logo.png", result.Copyright["copyright_logo"]);
            Assert.Equal("https://www.geocode.farm/policies/terms-of-service/", result.Copyright["terms_of_service"]);
            Assert.Equal("https://www.geocode.farm/policies/privacy-policy/", result.Copyright["privacy_policy"]);
        }

        [Fact]
        public void Forward_InvalidAddress_NoException()
        {
            var location = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
            var target = GetTarget();
            var result = target.Forward(location, 3);

            Assert.NotNull(result);
            
            Assert.Equal("FREE_USER, ACCESS_GRANTED", result.Status.Access);
            Assert.Equal("FAILED, NO_RESULTS", result.Status.Status);
            Assert.Equal("NONE, UNLICENSED", result.Account.DistributionLicense);

            Assert.Equal("Copyright (c) 2019 Geocode.Farm - All Rights Reserved.", result.Copyright["copyright_notice"]);
            Assert.Equal("https://www.geocode.farm/images/logo.png", result.Copyright["copyright_logo"]);
            Assert.Equal("https://www.geocode.farm/policies/terms-of-service/", result.Copyright["terms_of_service"]);
            Assert.Equal("https://www.geocode.farm/policies/privacy-policy/", result.Copyright["privacy_policy"]);
        }

        private static GeocodeFarmGeocoder GetTarget()
        {
            var target = new GeocodeFarmGeocoder(false);
            return target;
        }
    }
}
