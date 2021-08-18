using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Games.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class CicloDeVidaIDController : ControllerBase
    {

        public readonly IExempleSingleton _exempleSingleton1;
        public readonly IExempleSingleton _exempleSingleton2;

        public readonly IExempleScoped _exempleScoped1;
        public readonly IExempleScoped _exempleScoped2;

        public readonly IExempleTransient _exempleTransient1;
        public readonly IExempleTransient _exempleTransient2;

        public CicloDeVidaIDController(IExempleSingleton exempleSingleton1,
                                       IExempleSingleton exempleSingleton2,
                                       IExempleScoped exempleScoped1,
                                       IExempleScoped exempleScoped2,
                                       IExempleTransient exempleTransien1,
                                       IExempleTransient exempleTransient2)
        {
            _exempleSingleton1 = exempleSingleton1;
            _exempleSingleton2 = exempleSingleton2;
            _exempleScoped1 = exempleScoped1;
            _exempleScoped2 = exempleScoped2;
            _exempleTransient1 = exempleTransien1;
            _exempleTransient2 = exempleTransient2;
        }

        [HttpGet]
        public Task<string> Get()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Singleton 1: {_exempleSingleton1.Id}");  //Guarda a instancia o processamento todo, Nunca muda o ID "instância"
            stringBuilder.AppendLine($"Singleton 2: {_exempleSingleton2.Id}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Scoped 1: {_exempleScoped1.Id}");        //Muda se mudar a requisição apenas
            stringBuilder.AppendLine($"Scoped 2: {_exempleScoped2.Id}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Transient 1: {_exempleTransient1.Id}");  //Toda vez que injeta da uma instancia nova, 
            stringBuilder.AppendLine($"Transient 2: {_exempleTransient2.Id}");
            stringBuilder.AppendLine();

            return Task.FromResult(stringBuilder.ToString());
        }

        public interface IExempleGeral
        {
            public Guid Id { get; }
        }

        public interface IExempleSingleton : IExempleGeral
        { }
         
        public interface IExempleScoped : IExempleGeral
        { }

        public interface IExempleTransient : IExempleGeral
        { }

        public class ExLifeCycle : IExempleSingleton, IExempleScoped, IExempleTransient
        {
            private readonly Guid _guid;

            public ExLifeCycle()
            {
                _guid = Guid.NewGuid();
            }

            public Guid Id => _guid;
        }
    }
}
