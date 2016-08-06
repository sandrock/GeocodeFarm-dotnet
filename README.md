
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

### Free user

```
var geocoder = new GeocodeFarmGeocoder();
GeocodeFarmModel result;
try
{
    result = geocoder.Forward("Friedrichstr. 123, 10117 Berlin, Deutschland", 3);
}
catch (GeocodeFarmGeocoderException ex)
{
    // handle error
}
```

### Paid user

```
var geocoder = new GeocodeFarmGeocoder("PAID USER API KEY");
GeocodeFarmModel result;
try
{
    result = geocoder.Forward("Friedrichstr. 123, 10117 Berlin, Deutschland", 3);
}
catch (GeocodeFarmGeocoderException ex)
{
    // handle error
}
```

### Exceptions

* `WebException`s are wrapped into a `GeocodeFarmGeocoderException`
* Some result errors will throw a `GeocodeFarmGeocoderException`
* Deserialization errors are not handled by the library

You should not catch the general exception type. If you encounter an exception of a type different from `GeocodeFarmGeocoderException`, **please create an issue with a ToString() of the exception**. I will make sure it is wrapped into a `GeocodeFarmGeocoderException` exception

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
//catch (Exception ex) // you should not do that
//{
//    ...
//}
```


### Result

```
result.Result.Results[0].FormattedAddress             // Friedrichstraße 123, 10117 Berlin, Germany
result.Result.Results[0].Accuracy                     // EXACT_MATCH
result.Result.Results[0].Address.StreetNumber         // 123
result.Result.Results[0].Address.StreetName           // Friedrichstraße
result.Result.Results[0].Address.Locality             // Berlin
result.Result.Results[0].Address.Neighborhood         // 
result.Result.Results[0].Address.Admin1               // Berlin
result.Result.Results[0].Address.Admin2               // 
result.Result.Results[0].Address.PostalCode           // 10117
result.Result.Results[0].Address.Country              // Germany
result.Result.Results[0].LocationDetails.Elevation    // UNAVAILABLE   
result.Result.Results[0].LocationDetails.TimezoneName // UNAVAILABLE
result.Result.Results[0].LocationDetails.TimezoneCode // Europe/Berlin
result.Result.Results[0].Coordinates.Latitude         // 52.5268605910630
result.Result.Results[0].Coordinates.Longitude        // 13.3868521933020
result.Result.Status.Access                           // FREE_USER, ACCESS_GRANTED
result.Result.Status.Status                           // SUCCESS
result.Result.Account.DistributionLicense             // NONE, UNLICENSED
result.Result.Account.UsageLimit                      // 250
result.Result.Account.UsedToday                       // 247
result.Result.Account.UsedTotal                       // 512
result.Result.Account.Remaining                       //   3
result.Result.Account.FirstUsed                       // 26 Jul 201
result.Result.Copyright["copyright_notice"]           // Copyright (c) 2016 Geocode.Farm - All Rights Reserved.
result.Result.Copyright["copyright_logo"]             // https://www.geocode.farm/images/logo.png
result.Result.Copyright["terms_of_service"]           // https://www.geocode.farm/policies/terms-of-service/
result.Result.Copyright["privacy_policy"]             // https://www.geocode.farm/policies/privacy-policy/
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
- [ ] Handle deserialization errors (and throw GeocodeFarmGeocoderException)



