using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Games.ViewModel
{
    public class GameViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Producter { get; set; }
        public double Price { get; set; }

    }
}
