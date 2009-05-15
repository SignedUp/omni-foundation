// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="FriendlyNameAttribute.cs" company="Omniscient Technology Inc">
//   Copyright (c) Harmony Information Systems, Inc. 2009
// </copyright>
// <summary>
//   Defines the TestAttribute type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------
namespace Omniscient.Foundation.Data
{
    using System;

    /// <summary>
    /// Provides an attribute to give a friendly name to a property to help in UI setup
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class FriendlyNameAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FriendlyNameAttribute"/> class.
        /// </summary>
        /// <param name="name">
        /// The properties' friendly name.
        /// </param>
        public FriendlyNameAttribute(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Gets or sets property friendly Name.
        /// </summary>
        public string Name { get; set; }
    }
}