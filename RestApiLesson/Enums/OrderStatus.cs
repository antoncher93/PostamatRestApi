using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiLesson.Enums
{
    public enum OrderStatus
    {
        Registered = 1,
        InStock = 2,
        IssuedToCourier = 3,
        InPostamat = 4,
        Received = 5,
        Canceled = 6
    }
}
