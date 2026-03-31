using DbLayer;
using Microsoft.EntityFrameworkCore;
using ModelsLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class LoginsService : ILogins
    {
        private readonly MudeboDb _context;
        public LoginsService(MudeboDb context)
        { _context = context; }
        public async  Task<bool> FindLoginsAsync(Logins log)
        {
            var result = new Logins();
            var noResult = new Logins();
            try
            {
                var loggg = await _context.Logins.SingleOrDefaultAsync(x => x.UserName == log.UserName && x.Password == log.Password);
                if(loggg != null) { result = loggg;  }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was problem finding this login => {ex.InnerException}");
                return false;
            }
            return true;
        }
    }
}
