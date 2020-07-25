using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace tabuleiro {
    abstract class Peca {

        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QteMovimentos { get; protected set; }
        public Tabuleiro Tab { get; protected set; }

        public Peca(Cor cor, Tabuleiro tab) {
            Posicao = null;
            Cor = cor;
            Tab = tab;
            QteMovimentos = 0;
        }

        public void ImcrementarQteMovimentos() {
            QteMovimentos++;
        }

        public abstract bool[,] MovimentosPossiveis();


    }
}
