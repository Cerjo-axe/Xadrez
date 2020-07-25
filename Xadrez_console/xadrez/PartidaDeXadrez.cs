﻿using tabuleiro;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace xadrez {
    class PartidaDeXadrez {

        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;

        public bool Xeque { get; private set; }

        public PartidaDeXadrez() {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino) {
            Peca p = Tab.RetirarPeca(origem);
            p.ImcrementarQteMovimentos();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
            if(pecaCapturada != null) {
                Capturadas.Add(pecaCapturada);
            }
            return pecaCapturada; 
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada) {
            Peca p = Tab.RetirarPeca(destino);
            p.DecrementarQteMovimentos();
            if(pecaCapturada != null) {
                Tab.ColocarPeca(pecaCapturada, destino);
                Capturadas.Remove(pecaCapturada);
            }
            Tab.ColocarPeca(p, origem); 
        }

        public void RealizaJogada(Posicao origem, Posicao destino) {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual)) {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Voce nao pode se colocar em Xeque");
            }
            if (EstaEmXeque(Adversaria(JogadorAtual))) {
                Xeque = true;
            } else {
                Xeque = false;
            }
            if (TesteXequeMate(Adversaria(JogadorAtual))) {
                Terminada = true;
            } else {
                Turno++;
                MudaJogador();
            }
        }

        public void ValidarPosicaoOrigem(Posicao pos) {
            if (Tab.ReturnPeca(pos) == null) {
                throw new TabuleiroException("^Não existe peca na posicao escolhida");
            }
            if (JogadorAtual != Tab.ReturnPeca(pos).Cor) {
                throw new TabuleiroException("A peca não é sua!!");
            }
            if (!Tab.ReturnPeca(pos).ExisteMovimentosPossiveis()) {
                throw new TabuleiroException("Não existe movimentos possiveis para essa peca!!");
            }
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino) {
            if (!Tab.ReturnPeca(origem).MovimentoPossivel(destino)) {
                throw new TabuleiroException("Posicao destino invalida");
            }
        }

        private void MudaJogador() {
            if (JogadorAtual == Cor.Branca) {
                JogadorAtual = Cor.Preta;
            } else {
                JogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> PecasCapturadas(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in Capturadas) {
                if (x.Cor == cor) {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor) {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in Pecas) {
                if (x.Cor == cor) {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        private Cor Adversaria(Cor cor) {
            if (cor == Cor.Branca) {
                return Cor.Preta;
            } else {
                return Cor.Branca;
            }
        }

        private Peca Rei(Cor cor) {
            foreach(Peca x in PecasEmJogo(cor)) {
                if(x is Rei) {
                    return x;
                }
            }
            return null;
        }

        public bool EstaEmXeque(Cor cor) {
            Peca r = Rei(cor);
            if (r == null) {
                throw new TabuleiroException("Nao tem Rei");
            }

            foreach(Peca x in PecasEmJogo(Adversaria(cor))) {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[r.Posicao.Linha, r.Posicao.Coluna]) {
                    return true; 
                }
            }
            return false;
        }


        public bool TesteXequeMate(Cor cor) {
            if (!EstaEmXeque(cor)) {
                return false;

            }
            foreach(Peca x in PecasEmJogo(cor)) {
                bool[,] mat = x.MovimentosPossiveis();
                for(int i = 0; i < Tab.Linhas; i++) {
                    for(int j = 0; j < Tab.Colunas; j++) {
                        if (mat[i, j]) {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque) {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca) {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }

        private void ColocarPecas() {
            ColocarNovaPeca('c', 1, new Torre(Cor.Branca, Tab));
            ColocarNovaPeca('c', 2, new Torre(Cor.Branca, Tab));
            ColocarNovaPeca('d', 2, new Torre(Cor.Branca, Tab));
            ColocarNovaPeca('e', 2, new Torre(Cor.Branca, Tab));
            ColocarNovaPeca('e', 1, new Torre(Cor.Branca, Tab));
            ColocarNovaPeca('d', 1, new Rei(Cor.Branca, Tab));

            ColocarNovaPeca('c', 7, new Torre(Cor.Preta, Tab));
            ColocarNovaPeca('c', 8, new Torre(Cor.Preta, Tab));
            ColocarNovaPeca('d', 7, new Torre(Cor.Preta, Tab));
            ColocarNovaPeca('e', 7, new Torre(Cor.Preta, Tab));
            ColocarNovaPeca('e', 8, new Torre(Cor.Preta, Tab));
            ColocarNovaPeca('d', 8, new Rei(Cor.Preta, Tab));
        }
    }
}
