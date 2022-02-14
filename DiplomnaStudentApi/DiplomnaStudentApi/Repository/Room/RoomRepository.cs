﻿using DiplomnaStudentApi.Model;
using MongoRepo.Interfaces.Repository;
using MongoRepo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.Repository
{
    public class RoomRepository : CommonRepository<Room>, IRoomRepository
    {
        public RoomRepository(): base(new MongoRepo.Context.ApplicationDbContext(DatabaseConfig.DbConnectionString, DatabaseConfig.DbName))
        {

        }
    }
}
