using DiplomnaStudentApi.Model;
using MongoRepo.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.Repository
{
    public class ApplicationForSettlementManager: CommonManager<ApplicationForSettlement>, IApplicationForSettlementManager
    {
        public ApplicationForSettlementManager(): base(new ApplicationForSettlementRepository())
        {

        }
    }
}
