using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger.BLL.Exceptions
{
    public class BadRequestException: Exception
    {
        public BadRequestException() : base() { }

        public BadRequestException(string message) : base(message) { }
    }
}
