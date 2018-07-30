// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.KeyVault.Models
{
    using Newtonsoft.Json;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// One property of operation, include log specifications.
    /// </summary>
    public partial class ServiceSpecification
    {
        /// <summary>
        /// Initializes a new instance of the ServiceSpecification class.
        /// </summary>
        public ServiceSpecification()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the ServiceSpecification class.
        /// </summary>
        /// <param name="logSpecifications">Log specifications of
        /// operation.</param>
        public ServiceSpecification(IList<LogSpecification> logSpecifications = default(IList<LogSpecification>))
        {
            LogSpecifications = logSpecifications;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets log specifications of operation.
        /// </summary>
        [JsonProperty(PropertyName = "logSpecifications")]
        public IList<LogSpecification> LogSpecifications { get; set; }

    }
}
