
using tabuleiro;

namespace xadrez {
    class Bispo : Peca {
        public Bispo(Cor cor, Tabuleiro tab) : base(cor, tab) {

        }


        private bool PodeMover(Posicao pos) {
            Peca p = Tab.ReturnPeca(pos);
            return p == null || p.Cor != Cor;
        }

        public override bool[,] MovimentosPossiveis() {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new Posicao(0, 0);

            //NO

            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna-1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.ReturnPeca(pos) != null && Tab.ReturnPeca(pos).Cor != Cor) {
                    break;
                }
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            }

            //NE

            pos.DefinirValores(Posicao.Linha -1, Posicao.Coluna+1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.ReturnPeca(pos) != null && Tab.ReturnPeca(pos).Cor != Cor) {
                    break;
                }
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);

            }

            //SE

            pos.DefinirValores(Posicao.Linha+1, Posicao.Coluna + 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.ReturnPeca(pos) != null && Tab.ReturnPeca(pos).Cor != Cor) {
                    break;
                }
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            }

            //SO

            pos.DefinirValores(Posicao.Linha+1, Posicao.Coluna - 1);
            while (Tab.PosicaoValida(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
                if (Tab.ReturnPeca(pos) != null && Tab.ReturnPeca(pos).Cor != Cor) {
                    break;
                }
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            }

            return mat;


        }



        public override string ToString() {
            return "B";
        }
    }
}
