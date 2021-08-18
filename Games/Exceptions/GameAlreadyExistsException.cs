﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games.Exceptions
{
    public class GameAlreadyExistsException : Exception
    {
        public GameAlreadyExistsException()
            : base("This game already exists!")
        { }
    }
}

