using DiplomnaStudentApi.Model;
using MongoRepo.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.Repository
{
    public class NotificationManager: CommonManager<Notification>, INotificationManager
    {
        public NotificationManager(): base(new NotificationRepository())
        {

        }
    }
}
