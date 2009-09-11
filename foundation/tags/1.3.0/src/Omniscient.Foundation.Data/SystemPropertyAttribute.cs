// --------------------------------------------------------------------------------------------------------------------- 
// <copyright file="SystemPropertyAttribute.cs" company="Omniscient Technology Inc">
//   Copyright (c) Harmony Information Systems, Inc. 2009
// </copyright>
// <summary>
//   Defines the SystemProperty type.
// </summary>
// ---------------------------------------------------------------------------------------------------------------------

namespace Omniscient.Foundation.Data
{
    using System;

    /// <summary>
    /// Determines if an object Property is for proper system functions rather than true Domain Driven behavior
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class SystemProperty : Attribute
    {
    }
}