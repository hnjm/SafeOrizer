using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeOrizer.Interfaces
{
    public interface IDbHelper
    {
        string GetLocalFilePath(string filename);
    }
}
