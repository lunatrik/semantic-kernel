﻿// Copyright (c) Microsoft. All rights reserved.

using System;

#pragma warning disable CA1720 // Identifier contains type name

#pragma warning disable IDE0130
// ReSharper disable once CheckNamespace - Using the main namespace
namespace Microsoft.SemanticKernel;
#pragma warning restore IDE0130

/// <summary>
/// Represents the JSON type of a parameter.
/// </summary>
public readonly record struct ParameterJsonType(string Name)
{
    /// <summary>
    /// Represents the "string" parameter view type.
    /// </summary>
    public static readonly ParameterJsonType String = new("string");

    /// <summary>
    /// Represents the "number" parameter view type.
    /// </summary>
    public static readonly ParameterJsonType Number = new("number");

    /// <summary>
    /// Represents the "object" parameter view type.
    /// </summary>
    public static readonly ParameterJsonType Object = new("object");

    /// <summary>
    /// Represents the "array" parameter view type.
    /// </summary>
    public static readonly ParameterJsonType Array = new("array");

    /// <summary>
    /// Represents the "boolean" parameter view type.
    /// </summary>
    public static readonly ParameterJsonType Boolean = new("boolean");

    /// <summary>
    /// Gets the name of the parameter view type.
    /// </summary>
    public string Name { get; init; } = !string.IsNullOrEmpty(Name) ? Name : throw new ArgumentNullException(nameof(Name));

    /// <summary>
    /// Returns a string representation of the parameter view type.
    /// </summary>
    /// <returns>A string representing the parameter view type.</returns>
    public override string ToString() => this.Name;

    /// <summary>
    /// Returns the hash code for this instance.
    /// </summary>
    /// <returns>A hash code for the current instance.</returns>
    public override int GetHashCode()
    {
        return this.Name.GetHashCode();
    }
}
