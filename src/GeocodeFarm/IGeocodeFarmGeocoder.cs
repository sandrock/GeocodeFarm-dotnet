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
    public interface IGeocodeFarmGeocoder
    {
        /// <summary>
        /// Forward geocoding takes a provided address or location and returns the coordinate set for the requested location.
        /// </summary>
        /// <param name="location">The string to search for. Usually a street address. Just be sure to include the country at the end of the address to ensure accurate results.</param>
        /// <returns></returns>
        /// <exception cref="GeocodeFarmGeocoderException"></exception>
        GeocodingResults Forward(string location);

        /// <summary>
        /// Forward geocoding takes a provided address or location and returns the coordinate set for the requested location.
        /// </summary>
        /// <param name="location">The string to search for. Usually a street address. Just be sure to include the country at the end of the address to ensure accurate results.</param>
        /// <param name="count">Default is 1.</param>
        /// <returns></returns>
        /// <exception cref="GeocodeFarmGeocoderException"></exception>
        GeocodingResults Forward(string location, int count);

        /// <summary>
        /// Reverse geocoding takes a provided coordinate set and returns the address for the requested coordinates.
        /// </summary>
        /// <param name="latitude">The numerical latitude value for which you wish to obtain the closest, human-readable address.</param>
        /// <param name="longitude">The numerical longitude value for which you wish to obtain the closest, human-readable address.</param>
        /// <param name="count">Default is 1.</param>
        /// <returns></returns>
        /// <exception cref="GeocodeFarmGeocoderException"></exception>
        GeocodingResults Reverse(double latitude, double longitude);

        /// <summary>
        /// Reverse geocoding takes a provided coordinate set and returns the address for the requested coordinates.
        /// </summary>
        /// <param name="latitude">The numerical latitude value for which you wish to obtain the closest, human-readable address.</param>
        /// <param name="longitude">The numerical longitude value for which you wish to obtain the closest, human-readable address.</param>
        /// <returns></returns>
        /// <exception cref="GeocodeFarmGeocoderException"></exception>
        GeocodingResults Reverse(double latitude, double longitude, int count);
    }
}
