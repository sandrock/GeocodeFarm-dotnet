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

namespace GeocodeFarm.TestsNET461
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GeocodeFarmGeocoderTests
    {
        [TestMethod]
        public void Ctor_DoesNotCrash()
        {
            var geocoder = new GeocodeFarmGeocoder(false);
        }

        [TestMethod]
        public void Forward_ValidAddress_ValidResult()
        {
            var location = "Friedrichstr. 123, 10117 Berlin, Deutschland";
            var target = GetTarget();
            var result = target.Forward(location, 3);

            Assert.IsNotNull(result);

            Assert.AreEqual("Friedrichstraße 123, 10117 Berlin, Germany", result.Results[0].FormattedAddress);
            Assert.AreEqual("EXACT_MATCH", result.Results[0].Accuracy);
            Assert.AreEqual("123", result.Results[0].Address.StreetNumber);
            Assert.AreEqual("Friedrichstraße", result.Results[0].Address.StreetName);
            Assert.AreEqual("Berlin", result.Results[0].Address.Locality);
            Assert.AreEqual(null, result.Results[0].Address.Neighborhood);
            Assert.AreEqual("Berlin", result.Results[0].Address.Admin1);
            Assert.AreEqual(null, result.Results[0].Address.Admin2);
            Assert.AreEqual("10117", result.Results[0].Address.PostalCode);
            Assert.AreEqual("Germany", result.Results[0].Address.Country);
            Assert.AreEqual("UNAVAILABLE", result.Results[0].LocationDetails.Elevation);
            Assert.AreEqual("UNAVAILABLE", result.Results[0].LocationDetails.TimezoneName);
            Assert.AreEqual("Europe/Berlin", result.Results[0].LocationDetails.TimezoneCode);
            Assert.AreEqual(52.5268605910630M, result.Results[0].Coordinates.Latitude);
            Assert.AreEqual(13.3868521933020M, result.Results[0].Coordinates.Longitude);

            Assert.AreEqual("FREE_USER, ACCESS_GRANTED", result.Status.Access);
            Assert.AreEqual("SUCCESS", result.Status.Status);
            Assert.AreEqual("NONE, UNLICENSED", result.Account.DistributionLicense);

            Assert.AreEqual("Copyright (c) 2016 Geocode.Farm - All Rights Reserved.", result.Copyright["copyright_notice"]);
            Assert.AreEqual("https://www.geocode.farm/images/logo.png", result.Copyright["copyright_logo"]);
            Assert.AreEqual("https://www.geocode.farm/policies/terms-of-service/", result.Copyright["terms_of_service"]);
            Assert.AreEqual("https://www.geocode.farm/policies/privacy-policy/", result.Copyright["privacy_policy"]);
        }

        [TestMethod]
        public void Forward_InvalidAddress_NoException()
        {
            var location = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
            var target = GetTarget();
            var result = target.Forward(location, 3);

            Assert.IsNotNull(result);
            
            Assert.AreEqual("FREE_USER, ACCESS_GRANTED", result.Status.Access);
            Assert.AreEqual("FAILED, NO_RESULTS", result.Status.Status);
            Assert.AreEqual("NONE, UNLICENSED", result.Account.DistributionLicense);

            Assert.AreEqual("Copyright (c) 2016 Geocode.Farm - All Rights Reserved.", result.Copyright["copyright_notice"]);
            Assert.AreEqual("https://www.geocode.farm/images/logo.png", result.Copyright["copyright_logo"]);
            Assert.AreEqual("https://www.geocode.farm/policies/terms-of-service/", result.Copyright["terms_of_service"]);
            Assert.AreEqual("https://www.geocode.farm/policies/privacy-policy/", result.Copyright["privacy_policy"]);
        }

        private static GeocodeFarmGeocoder GetTarget()
        {
            var target = new GeocodeFarmGeocoder(false);
            return target;
        }
    }
}
