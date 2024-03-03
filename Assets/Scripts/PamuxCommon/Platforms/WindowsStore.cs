// ------------------------------------------------------------------------------------------------
// <copyright file="WindowsStore.cs" company="Pamux Studios">
//     Copyright (c) Pamux Studios.  All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace Pamux.Platforms
{

    //using Microsoft.Services.Store.Engagement.Unity3D;

    using Pamux.Abstracts;
    using Pamux.Interfaces;

    public class WindowsStore : Platform
    {
        public class WindowsStorePushNotifications : IPushNotifications
        {
            public void Register()
            {
                //StoreServicesEngagementManager.RegisterNotificationChannelAsync((result) =>
                //{

                //});
            }

            public void Unregister()
            {
                //StoreServicesEngagementManager.UnregisterNotificationChannelAsync((result) =>
                //{

                //});
            }
        }

        public WindowsStore()
        {
            PushNotifications = new WindowsStorePushNotifications();
        }
    }
}