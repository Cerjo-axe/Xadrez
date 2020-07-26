using tabuleiro;

namespace Xadrez_console.xadrez {
    class Peao : Peca {
        public Peao(Cor cor, Tabuleiro tab) : base(cor, tab) {
        }

        public override string ToString() {
            return "P";
        }


        private bool ExisteInimigo(Posicao pos) {
            Peca p = Tab.ReturnPeca(pos);
            return p != null && p.Cor != Cor;
        }

        private bool Livre(Posicao pos) {
            Peca p = Tab.ReturnPeca(pos);
            return p == null;
        }

        public override bool[,] MovimentosPossiveis() {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new Posicao(0, 0);

            if (Cor == Cor.Branca) {
                pos.DefinirValores(pos.Linha - 1,pos.Coluna);
                if(Tab.PosicaoValida(pos) && Livre(pos)) {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(pos.Linha - 2, pos.Coluna);
                if (Tab.PosicaoValida(pos) && Livre(pos) && QteMovimentos==0) {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(pos.Linha - 1, pos.Coluna - 1);
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos)) {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(pos.Linha - 1, pos.Coluna+1);
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos)) {
                    mat[pos.Linha, pos.Coluna] = true;
                }
            } else {
                pos.DefinirValores(pos.Linha +1, pos.Coluna);
                if (Tab.PosicaoValida(pos) && Livre(pos)) {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(pos.Linha + 2, pos.Coluna);
                if (Tab.PosicaoValida(pos) && Livre(pos) && QteMovimentos == 0) {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(pos.Linha + 1, pos.Coluna - 1);
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos)) {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(pos.Linha + 1, pos.Coluna + 1);
                if (Tab.PosicaoValida(pos) && ExisteInimigo(pos)) {
                    mat[pos.Linha, pos.Coluna] = true;
                }
            }

            return mat;


        }
    }
}
