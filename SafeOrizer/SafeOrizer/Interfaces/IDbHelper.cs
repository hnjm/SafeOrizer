// <copyright file="IDbHelper.cs" company="Christoph Nienaber, https://github.com/zuckerthoben">
// Copyright (c) Christoph Nienaber, https://github.com/zuckerthoben. All rights reserved.
// </copyright>

namespace SafeOrizer.Interfaces
{
    /// <summary>
    /// Interface to implement SQLite on all platforms via Dependency Service
    /// </summary>
    public interface IDbHelper
    {
        /// <summary>
        /// Get the local file path of the database
        /// </summary>
        /// <param name="filename">the name of the database</param>
        /// <returns>the path to the local SQLite database on the device</returns>
        string GetLocalFilePath(string filename);
    }
}
