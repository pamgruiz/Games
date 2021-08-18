using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games.Exceptions
{
    public class GameNotFoundException : Exception
    {
        public GameNotFoundException() 
            : base("Game Not Found!")
        { }
    }
}
