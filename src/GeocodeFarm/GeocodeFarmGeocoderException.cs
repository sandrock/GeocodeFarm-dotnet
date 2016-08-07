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

    [Serializable]
    public class GeocodeFarmGeocoderException : Exception
    {
        public GeocodeFarmGeocoderException()
        {
        }

        public GeocodeFarmGeocoderException(string message)
            : base(message)
        {
        }

        public GeocodeFarmGeocoderException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected GeocodeFarmGeocoderException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public bool IsQuotaReached { get; set; }

        public GeocodingResults Result { get; set; }

        public string RequestStatus { get; set; }

        public string RequestAccess { get; set; }
    }
}
