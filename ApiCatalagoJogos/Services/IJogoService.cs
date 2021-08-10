using ApiCatalagoJogos.Entities;
using ApiCatalagoJogos.InputModel;
using ApiCatalagoJogos.viewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalagoJogos.Services
{
    public interface IJogoService : IDisposable
    {
        Task<List<JogoViewModel>> Obter(int pagina, int quantidade);
        Task<JogoViewModel> Obter(Guid id);
        Task<JogoViewModel> Inserir(JogoInputModel  jogo );
        Task Atualizar(Guid id, JogoInputModel jogo);
        Task Atualizar(Guid id, double preco);
        Task Atualizar( JogoViewModel entidadeJogo );
        Task Remover(Guid id);
        Task Inserir(Jogo jogoInsert);
        Task Obter(string nome, string produtora);
        Task Obter(string nome, object produtora);
    }
}
