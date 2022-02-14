using DiplomnaStudentApi.Model;
using MongoRepo.Interfaces.Manager;
using MongoRepo.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.Repository
{
    public interface IRoomRepository: ICommonRepository<Room>
    {
    }
}
