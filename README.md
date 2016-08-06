
# GeocodeFarm-dotnet

Unofficial .NET client for https://www.geocode.farm/.

You must comply with [geocode.farm's terms of service](https://geocode.farm/geocoding/free-api-documentation/).

License: LGPL.

## Install

Dependencies: Newtonsoft.Json.

Here is the nuget package: [GeocodeFarm](https://www.nuget.org/packages/GeocodeFarm/).

```
> Install-Package GeocodeFarm
```

## Use

```
var geocoder = new GeocodeFarmGeocoder();
GeocodeFarmModel result;
try
{
    result = geocoder.Forward("Friedrichstr. 123, 10117 Berlin, Deutschland", 3);
}
catch (GeocodeFarmGeocoderException ex)
{
    if (ex.IsQuotaReached)
    {
        // request quota has been reached
        Trace.WriteLine("GeocodeFarmGeocoder quota reached");
    }
    else
    {
        Trace.WriteLine("GeocodeFarmGeocoder error: " + ex.Message);
    }
}
//catch (Exception ex)
//{
//    // you should not catch the general exception type
//    // if you encounter an exception of a type different from GeocodeFarmGeocoderException
//    // please create an issue with a ToString() of the exception
//    // I will make sure it is wrapped into a GeocodeFarmGeocoderException exception
//}

result.Results[0].FormattedAddress             // Friedrichstraße 123, 10117 Berlin, Germany
result.Results[0].Accuracy                     // EXACT_MATCH
result.Results[0].Address.StreetNumber         // 123
result.Results[0].Address.StreetName           // Friedrichstraße
result.Results[0].Address.Locality             // Berlin
result.Results[0].Address.Neighborhood         // 
result.Results[0].Address.Admin1               // Berlin
result.Results[0].Address.Admin2               // 
result.Results[0].Address.PostalCode           // 10117
result.Results[0].Address.Country              // Germany
result.Results[0].LocationDetails.Elevation    // UNAVAILABLE   
result.Results[0].LocationDetails.TimezoneName // UNAVAILABLE
result.Results[0].LocationDetails.TimezoneCode // Europe/Berlin
result.Results[0].Coordinates.Latitude         // 52.5268605910630
result.Results[0].Coordinates.Longitude        // 13.3868521933020
result.Result.Status.Access                    // FREE_USER, ACCESS_GRANTED
result.Result.Status.Status                    // SUCCESS
result.Result.Account.DistributionLicense      // NONE, UNLICENSED
result.Result.Account.UsageLimit               // 250
result.Result.Account.UsedToday                // 247
result.Result.Account.UsedTotal                // 512
result.Result.Account.Remaining                //   3
result.Result.Account.FirstUsed                // 26 Jul 201
result.Copyright["copyright_notice"]           // Copyright (c) 2016 Geocode.Farm - All Rights Reserved.
result.Copyright["copyright_logo"]             // https://www.geocode.farm/images/logo.png
result.Copyright["terms_of_service"]           // https://www.geocode.farm/policies/terms-of-service/
result.Copyright["privacy_policy"]             // https://www.geocode.farm/policies/privacy-policy/
```

## Contribute

Feel free to make pull requests. 

Code must pass StyleCop. 

## To Do list

- [x] Forward queries
- [x] Reverse queries
- [x] Free Usage calls
- [ ] Paid Usage calls
- [x] nuget package
- [x] Target framework: .NET 4.6.1
- [ ] Target framework: .NET core
- [ ] Target framework: PCL
- [ ] Target framework: Universal apps



