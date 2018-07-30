// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.RecoveryServices.Backup.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// The token information details.
    /// </summary>
    public partial class TokenInformation
    {
        /// <summary>
        /// Initializes a new instance of the TokenInformation class.
        /// </summary>
        public TokenInformation()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the TokenInformation class.
        /// </summary>
        /// <param name="token">Token value.</param>
        /// <param name="expiryTimeInUtcTicks">Expiry time of token.</param>
        /// <param name="securityPIN">Security PIN</param>
        public TokenInformation(string token = default(string), long? expiryTimeInUtcTicks = default(long?), string securityPIN = default(string))
        {
            Token = token;
            ExpiryTimeInUtcTicks = expiryTimeInUtcTicks;
            SecurityPIN = securityPIN;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets token value.
        /// </summary>
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets expiry time of token.
        /// </summary>
        [JsonProperty(PropertyName = "expiryTimeInUtcTicks")]
        public long? ExpiryTimeInUtcTicks { get; set; }

        /// <summary>
        /// Gets or sets security PIN
        /// </summary>
        [JsonProperty(PropertyName = "securityPIN")]
        public string SecurityPIN { get; set; }

    }
}
