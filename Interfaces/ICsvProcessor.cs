using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Impexpdata.Interfaces
{
    public interface ICsvProcessor
    {
        void ImportDataFromCsv(string fileName);

        void ExportDataToCsv(string fileName);
    }
}
