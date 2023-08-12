using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuctionApi.Services.Utils
{
    public class ExceptionsHandler
    {
        public static void NotFoundExceptionHandler<T>(T entity)
        {
            var className = typeof(T).Name;
            if(entity == null)
            {
                throw new NotFoundException($"{className} not found");
            }
        }

    }
}
