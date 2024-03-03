// ------------------------------------------------------------------------------------------------
// <copyright file="PlatformFactory.cs" company="Pamux Studios">
//     Copyright (c) Pamux Studios.  All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace Pamux.Factories
{
    using Pamux.Abstracts;
    using Pamux.Platforms;

    public class PlatformFactory
    {
        public static Platform GetPlatform()
        {
            return new WindowsStore();
        }
    }
}