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
        private void AtualizarPosicoesPecaPosicionadas()
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
            this.AtualizarPecaAtual();
        }

        public void JogarPeca(bool inicio)
        {
            if (inicio)
            {
                if (this.PecasPosicionadas.Count > 0)
                {
                    if (this.ValidaJogada(0))
                    {
                        this.PecasPosicionadas.Insert(0, this.pecaAtual);
                        this.Pecas.Remove(this.pecaAtual);
                        this.DeslocarPeca();
                    }
                }
                else
                {
                    this.PecasPosicionadas.Insert(0, this.pecaAtual);
                    this.Pecas.Remove(this.pecaAtual);
                    this.DeslocarPeca();
                }
            }
            else
            {
                if (this.PecasPosicionadas.Count > 0)
                {
                    if (this.ValidaJogada(this.PecasPosicionadas.Count - 1))
                    {
                        this.PecasPosicionadas.Add(this.pecaAtual);
                        this.Pecas.Remove(this.pecaAtual);
                        this.DeslocarPeca();
                    }
                }
                else
                {
                    this.PecasPosicionadas.Add(this.pecaAtual);
                    this.Pecas.Remove(this.pecaAtual);
                    this.DeslocarPeca();
                }
            }
        }

        private void DeslocarPeca() {
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
            this.AtualizarPecaAtual();
            this.AtualizarPosicoesPecaPosicionadas();
        }

        private bool ValidaJogada(int posicaoNaLista)
        {
            Peca pecaParaConectar = this.PecasPosicionadas[posicaoNaLista];

            if (!pecaParaConectar.is_conectado_direita)
            {
                if (pecaParaConectar.bolinhas_direita == this.pecaAtual.bolinhas_direita)
                {
                    pecaParaConectar.is_conectado_direita = true;
                    this.pecaAtual.is_conectado_direita = true;
                    return true;

                }
                else if (pecaParaConectar.bolinhas_direita == this.pecaAtual.bolinhas_esquerda)
                {
                    pecaParaConectar.is_conectado_direita = true;
                    this.pecaAtual.is_conectado_esquerda = true;
                    return true;
                }
            }
            else if (!pecaParaConectar.is_conectado_esquerda)
            {
                if (pecaParaConectar.bolinhas_esquerda == this.pecaAtual.bolinhas_direita)
                {
                    pecaParaConectar.is_conectado_esquerda = true;
                    this.pecaAtual.is_conectado_direita = true;
                    return true;
                }
                else if (pecaParaConectar.bolinhas_esquerda == this.pecaAtual.bolinhas_esquerda)
                {
                    pecaParaConectar.is_conectado_esquerda = true;
                    this.pecaAtual.is_conectado_esquerda = true;
                    return true;
                }
            }
            return false;
        }

        private void AtualizarPecaAtual()
        {
            if (this.pecaAtual != null)
            {
                this.pecaAtual.Translacao(-1, 'y');
            }

            this.pecaAtual = this.Pecas[indexPecaAtual];

            this.pecaAtual.Translacao(1, 'y');
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
            this.AtualizarPecaAtual();
        }
        public Peca PegarPecaAtual()
        {
            return this.pecaAtual;
        }
    }
}