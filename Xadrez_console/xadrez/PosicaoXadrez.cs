using System;
using System.Collections.Generic;
using System.Text;

namespace xadrez {
    class PosicaoXadrez {
        public char Coluna { get; set; }
        public int Linha { get; set; }

        public PosicaoXadrez(char coluna, int linha) {
            Coluna = coluna;
            Linha = linha;
        }

        public override string ToString() {
            return "" + Coluna + Linha;
        }
    }
}
