// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Microsoft.Azure.Management.Security.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Possible operation in the REST API of Microsoft.Security
    /// </summary>
    public partial class Operation
    {
        /// <summary>
        /// Initializes a new instance of the Operation class.
        /// </summary>
        public Operation()
        {
            CustomInit();
        }

        /// <summary>
        /// Initializes a new instance of the Operation class.
        /// </summary>
        /// <param name="name">Name of the operation</param>
        /// <param name="origin">Where the operation is originated</param>
        public Operation(string name = default(string), string origin = default(string), OperationDisplay display = default(OperationDisplay))
        {
            Name = name;
            Origin = origin;
            Display = display;
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets name of the operation
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; private set; }

        /// <summary>
        /// Gets where the operation is originated
        /// </summary>
        [JsonProperty(PropertyName = "origin")]
        public string Origin { get; private set; }

        /// <summary>
        /// </summary>
        [JsonProperty(PropertyName = "display")]
        public OperationDisplay Display { get; set; }

    }
}
