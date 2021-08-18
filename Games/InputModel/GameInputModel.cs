using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Games.InputModel
{
    public class GameInputModel
    {
        [Required]   //Principio de Fail Fast, se nao se encaixar nesse padrao nem chega a bater na controller
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The name of the game must contain between 3 and 100 caracters")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The name of the producter must contain between 3 e 100 caracters")]
        public string Producter { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "The minimum price is 1 dolar and maximum 1000 dolar")]
        public double Price { get; set; }
    }
}
