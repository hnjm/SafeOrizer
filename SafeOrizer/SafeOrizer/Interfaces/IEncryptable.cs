using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeOrizer.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEncryptable
    {
        byte[] GetIV();
        void SetIV(byte[] iv);

        byte[] GetData();
        void SetData(byte[] data);
    }
}
