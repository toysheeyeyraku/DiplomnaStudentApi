using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomnaStudentApi.Services
{
    public interface IWordGeneratorService
    {
        MemoryStream GenerateWordApplicationForSettlement(string applicationSettlementId);
    }
}
