using System;
using System.Collections.Generic;
using System.Text;

namespace tabuleiro {
    class Tabuleiro {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] Pecas;

        public Tabuleiro(int linhas, int colunas) {
            Linhas = linhas;
            Colunas = colunas;
            Pecas = new Peca[linhas, colunas];
        }

        public Peca ReturnPeca(int linha, int coluna) {
            return Pecas[linha, coluna];
        }

        public Peca ReturnPeca(Posicao pos) {
            return Pecas[pos.Linha, pos.Coluna];
        }


        public bool ExistePeca(Posicao pos) {
            ValidarPosicao(pos);
            return ReturnPeca(pos) != null;
        }




        public void ColocarPeca(Peca p ,Posicao pos) {
            if (ExistePeca(pos)) {
                throw new TabuleiroException("Ja existe uma peça nesta posição!!");
            }
            Pecas[pos.Linha, pos.Coluna] = p;
            p.Posicao = pos;
        }

        public Peca RetirarPeca(Posicao pos) {
            if (ReturnPeca(pos) == null) {
                return null;
            }
            Peca aux = ReturnPeca(pos);
            aux.Posicao = null;
            Pecas[pos.Linha, pos.Coluna] = null;
            return aux;
        }

        public bool PosicaoValida(Posicao pos) {
            if(pos.Linha<0 || pos.Linha >= Linhas || pos.Coluna<0 || pos.Coluna>=Colunas) {
                return false;
            }
            return true;
        }
        public void ValidarPosicao (Posicao pos) {
            if (!PosicaoValida(pos)) {
                throw new TabuleiroException("Posição invalida!!");
            }
        }
    }
}
