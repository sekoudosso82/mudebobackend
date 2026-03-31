using ModelsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public interface ILogins
    {
        Task<bool> FindLoginsAsync(Logins log);
    }
}
