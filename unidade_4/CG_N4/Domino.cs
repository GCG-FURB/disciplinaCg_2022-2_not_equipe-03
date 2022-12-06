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
        private double TransalacaoParaEsquerda = 0;
        private double TransalacaoParaDireita = 0;
        public List<Peca> Pecas = new List<Peca>();
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
            this.AtualizarPecaAtual();
        }

        public bool JogarPeca(bool inicio)
        {
            if (inicio)
            {
                if (this.PecasPosicionadas.Count > 0)
                {
                    string jogada = this.ValidaJogada(inicio, 0);
                    if (!jogada.Equals("nao pode"))
                    {
                        this.PecasPosicionadas.Insert(0, this.pecaAtual);
                        this.Pecas.Remove(this.pecaAtual);
                        this.CalculaNovaPosicaoPeca(inicio, jogada);
                        return true;
                    }
                }
                else
                {
                    if (this.ValidaCarroca())
                    {
                        this.PecasPosicionadas.Insert(0, this.pecaAtual);
                        this.Pecas.Remove(this.pecaAtual);
                        this.CalculaNovaPosicaoPeca(inicio, "cruzado");
                        return true;
                    }
                }
            }
            else
            {
                if (this.PecasPosicionadas.Count > 0)
                {
                    string jogada = this.ValidaJogada(inicio, this.PecasPosicionadas.Count - 1);
                    if (!jogada.Equals("nao pode"))
                    {
                        this.PecasPosicionadas.Add(this.pecaAtual);
                        this.Pecas.Remove(this.pecaAtual);
                        this.CalculaNovaPosicaoPeca(inicio, jogada);
                        return true;
                    }
                }
                else
                {
                    if (this.ValidaCarroca())
                    {
                        this.PecasPosicionadas.Add(this.pecaAtual);
                        this.Pecas.Remove(this.pecaAtual);
                        this.CalculaNovaPosicaoPeca(inicio, "cruzado");
                        return true;
                    }
                }
            }
            return false;
        }

        private bool ValidaCarroca()
        {
            if (this.pecaAtual.is_carroca)
            {
                return true;
            }
            return false;
        }

        private void ColocarPecaNoInicio()
        {
            this.pecaAtual.ResetarPeca();
            this.pecaAtual.Translacao(4, 'z');
            this.pecaAtual.Rotacao(-90, 'x');
            this.pecaAtual.Translacao(-2, 'y');
        }

        private bool CalculaNovaPosicaoPeca(bool inicio, string jogada)
        {
            this.ColocarPecaNoInicio();

            if (this.PecasPosicionadas.Count > 1)
            {
                // atualiza valores para proxima jogada
                if (!this.pecaAtual.is_carroca)
                {
                    if (jogada.Equals("cruzado"))
                    {
                        this.pecaAtual.Rotacao(90, 'z');
                    }
                    else
                    {
                        this.pecaAtual.Rotacao(-90, 'z');
                    }

                    if (inicio)
                    {
                        // if (this.PecasPosicionadas[1].bolinhas_direita == this.pecaAtual.bolinhas_esquerda)
                        // {
                        //     int bolinhas = 0;
                        //     bolinhas = this.PecasPosicionadas[1].bolinhas_esquerda;
                        //     this.PecasPosicionadas[1].bolinhas_esquerda = this.PecasPosicionadas[1].bolinhas_direita;
                        //     this.PecasPosicionadas[1].bolinhas_direita = bolinhas;
                        //     this.pecaAtual.Rotacao(90, 'z');
                        // }
                        // else if (this.PecasPosicionadas[1].bolinhas_direita == this.pecaAtual.bolinhas_direita)
                        // {
                        //     this.pecaAtual.Rotacao(-90, 'z');
                        // }

                        this.TransalacaoParaDireita += 2.21;
                        this.pecaAtual.Translacao(TransalacaoParaDireita, 'x');
                    }
                    else
                    {
                        // if (this.PecasPosicionadas[PecasPosicionadas.Count - 1].bolinhas_esquerda == this.pecaAtual.bolinhas_esquerda)
                        // {
                        //     this.pecaAtual.Rotacao(-90, 'z');
                        // }
                        // else if (this.PecasPosicionadas[PecasPosicionadas.Count - 1].bolinhas_esquerda == this.pecaAtual.bolinhas_direita)
                        // {
                        //     this.pecaAtual.Rotacao(90, 'z');
                        // }


                        this.TransalacaoParaEsquerda -= 2.21;
                        this.pecaAtual.Translacao(TransalacaoParaEsquerda, 'x');
                    }
                }
                else
                {
                    if (inicio)
                    {
                        this.TransalacaoParaDireita += 2.2;
                        this.pecaAtual.Translacao(TransalacaoParaDireita, 'x');
                    }
                    else
                    {
                        this.TransalacaoParaEsquerda -= 2.2;
                        this.pecaAtual.Translacao(TransalacaoParaEsquerda, 'x');
                    }
                }
            }
            this.AtualizarPecaAtual();
            if (this.Pecas.Count == 0) {
                return true;
            }
            return false;
        }

        private void DeslocarPeca()
        {
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

        }

        private string ValidaJogada(bool inicio, int posicaoNaLista)
        {
            Peca pecaParaConectar = this.PecasPosicionadas[posicaoNaLista];

            if (inicio)
            {
                if (pecaParaConectar.bolinhas_esquerda == this.pecaAtual.bolinhas_direita)
                {
                    pecaParaConectar.is_conectado_direita = true;
                    this.pecaAtual.is_conectado_direita = true;
                    return "lado certo";
                }
                else if (pecaParaConectar.bolinhas_esquerda == this.pecaAtual.bolinhas_esquerda)
                {
                    pecaParaConectar.is_conectado_direita = true;
                    this.pecaAtual.is_conectado_direita = true;
                    this.pecaAtual.GirarPecaLogica();
                    return "cruzado";
                }
            }
            else
            {
                if (pecaParaConectar.bolinhas_direita == this.pecaAtual.bolinhas_direita)
                {
                    pecaParaConectar.is_conectado_esquerda = true;
                    this.pecaAtual.is_conectado_esquerda = true;
                    this.pecaAtual.GirarPecaLogica();
                    return "cruzado";
                }
                else if (pecaParaConectar.bolinhas_direita == this.pecaAtual.bolinhas_esquerda)
                {
                    pecaParaConectar.is_conectado_esquerda = true;
                    this.pecaAtual.is_conectado_esquerda = true;
                    return "lado certo";
                }
            }
            return "nao pode";
        }

        private void AtualizarPecaAtual()
        {
            if (this.Pecas.Count == 0) {
                return;
            }
            if (this.indexPecaAtual > this.Pecas.Count - 1)
            {
                this.indexPecaAtual = this.Pecas.Count - 1;
            }

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
                    peca.transalacao_inicial = posAtual * -1;
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