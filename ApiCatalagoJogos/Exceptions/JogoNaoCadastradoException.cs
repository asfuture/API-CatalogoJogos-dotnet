using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalagoJogos.Exception
{
    public class JogoNaoCadastradoException : SystemException
    {
        public JogoNaoCadastradoException()
            : base("Este já jogo está cadastrado")
        { }
    }
}
