using System;
using System.Collections.Generic;
using System.Text;

namespace Trilogic.EasyARGS
{
    public class ArgException : Exception
    {
        public ArgException(string msg)
            : base(msg)
        {
        }
    }
}
