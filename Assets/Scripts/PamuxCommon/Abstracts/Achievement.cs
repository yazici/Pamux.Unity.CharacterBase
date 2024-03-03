// ------------------------------------------------------------------------------------------------
// <copyright file="Achievement.cs" company="Pamux Studios">
//     Copyright (c) Pamux Studios.  All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace Pamux.Abstracts
{
    using System.Collections.Generic;

    public class Achievement
    {
        public string Id { get; private set; }

        public string Description { get; private set; }

        public Achievement(IDictionary<string, int> headerNameToColMap, string[] fields)
        {
            Id = fields[0];
            Description = fields[1];

            Achievements.AllAchievements[Id] = this;
        }
    }
}