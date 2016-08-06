
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
            var geocoder = new GeocodeFarmGeocoder();
        }

        [TestMethod]
        public void Forward_ValidAddress_ValidResult()
        {
            var location = "Friedrichstr. 123, 10117 Berlin, Deutschland";
            var target = GetTarget();
            var result = target.Forward(location, 3);
            Assert.IsNotNull(result);

            Assert.AreEqual("Friedrichstraße 123, 10117 Berlin, Germany", result.Result.Results[0].FormattedAddress);
            Assert.AreEqual("EXACT_MATCH", result.Result.Results[0].Accuracy);
            Assert.AreEqual("123", result.Result.Results[0].Address.StreetNumber);
            Assert.AreEqual("Friedrichstraße", result.Result.Results[0].Address.StreetName);
            Assert.AreEqual("Berlin", result.Result.Results[0].Address.Locality);
            Assert.AreEqual(null, result.Result.Results[0].Address.Neighborhood);
            Assert.AreEqual("Berlin", result.Result.Results[0].Address.Admin1);
            Assert.AreEqual(null, result.Result.Results[0].Address.Admin2);
            Assert.AreEqual("10117", result.Result.Results[0].Address.PostalCode);
            Assert.AreEqual("Germany", result.Result.Results[0].Address.Country);
            Assert.AreEqual("UNAVAILABLE", result.Result.Results[0].LocationDetails.Elevation);
            Assert.AreEqual("UNAVAILABLE", result.Result.Results[0].LocationDetails.TimezoneName);
            Assert.AreEqual("Europe/Berlin", result.Result.Results[0].LocationDetails.TimezoneCode);
            Assert.AreEqual(52.5268605910630M, result.Result.Results[0].Coordinates.Latitude);
            Assert.AreEqual(13.3868521933020M, result.Result.Results[0].Coordinates.Longitude);
            Assert.AreEqual("FREE_USER, ACCESS_GRANTED", result.Result.Status.Access);
            Assert.AreEqual("SUCCESS", result.Result.Status.Status);
            Assert.AreEqual("NONE, UNLICENSED", result.Result.Account.DistributionLicense);
            Assert.AreEqual("Copyright (c) 2016 Geocode.Farm - All Rights Reserved.", result.Result.Copyright["copyright_notice"]);
            Assert.AreEqual("https://www.geocode.farm/images/logo.png", result.Result.Copyright["copyright_logo"]);
            Assert.AreEqual("https://www.geocode.farm/policies/terms-of-service/", result.Result.Copyright["terms_of_service"]);
            Assert.AreEqual("https://www.geocode.farm/policies/privacy-policy/", result.Result.Copyright["privacy_policy"]);
        }

        private static GeocodeFarmGeocoder GetTarget()
        {
            var target = new GeocodeFarmGeocoder();
            return target;
        }
    }
}
