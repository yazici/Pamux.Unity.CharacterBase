// ------------------------------------------------------------------------------------------------
// <copyright file="Platform.cs" company="Pamux Studios">
//     Copyright (c) Pamux Studios.  All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace Pamux.Abstracts
{
    using Pamux.Interfaces;

    public class Platform
    {
        private class NoOpPushNotifications : IPushNotifications
        {
            public void Register()
            {
            }

            public void Unregister()
            {
            }
        }

        private IPushNotifications pushNotifications = new NoOpPushNotifications();

        public IPushNotifications PushNotifications
        {
            get { return pushNotifications; }
            protected set { pushNotifications = value; }
        }
    }
}