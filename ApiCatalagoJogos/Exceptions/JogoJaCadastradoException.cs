using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalagoJogos.Exception
{
    public class JogoJaCadastradoException : SystemException
    {
        public JogoJaCadastradoException()
            : base("Este já jogo está cadastrado")
        { }

    }
}
