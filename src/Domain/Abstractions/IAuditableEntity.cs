﻿using System;

namespace Domain.Abstractions
{
    /// <summary>
    /// Represents the marker interface for auditable entities.
    /// </summary>
    public interface IAuditableEntity
    {
        /// <summary>
        /// Gets the created on date and time in UTC format.
        /// </summary>
        DateTime CreatedOnUtc { get; }

        /// <summary>
        /// Gets the modified on date and time in UTC format.
        /// </summary>
        DateTime? ModifiedOnUtc { get; }

        /// <summary>
        /// Gets the date and time in UTC format the entity was deleted on.
        /// </summary>
        DateTime? DeletedOnUtc { get; }
    }
}