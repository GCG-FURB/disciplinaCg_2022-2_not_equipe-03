using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using System.Collections.Generic;
using System;

namespace gcgcg
{
    internal class Domino
    {
        private char objetoId = '@';
        private Peca pecaAtual;
        private int indexPecaAtual = 0;
        private int deslocamentoParaCima = 4;
        private List<Peca> Pecas = new List<Peca>();
        private List<Peca> PecasPosicionadas = new List<Peca>();
        public Domino()
        {

        }

        public void TrocarPecaAtual(string direcao)
        {
            if (direcao.Equals("direita"))
            {
                if (this.indexPecaAtual != this.Pecas.Count - 1)
                {
                    this.indexPecaAtual++;
                }
                else
                {
                    this.indexPecaAtual = 0;
                }
            }
            else
            {
                if (this.indexPecaAtual != 0)
                {
                    this.indexPecaAtual--;
                }
                else
                {
                    this.indexPecaAtual = this.Pecas.Count - 1;
                }
            }
            this.pecaAtual = this.Pecas[this.indexPecaAtual];
        }

        public void JogarPeca(bool inicio)
        {
            if (inicio)
            {
                this.PecasPosicionadas.Add(this.pecaAtual);
            }
            else
            {
                this.PecasPosicionadas.Add(this.pecaAtual);
            }
            this.Pecas.Remove(this.pecaAtual);

            this.pecaAtual.ResetarPeca();
            this.pecaAtual.Translacao(this.deslocamentoParaCima, 'y');
            this.pecaAtual.Rotacao(-90, 'x');
            if (!this.pecaAtual.is_carroca)
            {
                this.pecaAtual.Rotacao(90, 'z');
            }

            if (this.indexPecaAtual > this.Pecas.Count - 1)
            {
                this.indexPecaAtual = this.Pecas.Count - 1;
            }
            this.pecaAtual = this.Pecas[indexPecaAtual];
        }

        public void CriarPecas(List<Objeto> objetosLista)
        {
            int posAtual = 0;
            for (int i = 0; i < 7; i++)
            {
                for (int j = i; j < 7; j++)
                {
                    objetoId = Utilitario.charProximo(objetoId);
                    Peca peca = new Peca(objetoId, null, i, j);
                    objetosLista.Add(peca);
                    this.Pecas.Add(peca);
                    peca.Translacao(posAtual, 'x');
                    peca.SalvarPosicao();
                    posAtual += 2;
                }
            }
            this.pecaAtual = this.Pecas[this.indexPecaAtual];
        }
        public Peca PegarPecaAtual()
        {
            return this.pecaAtual;
        }
    }
}