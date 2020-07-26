using tabuleiro;

namespace xadrez {
    class Rei : Peca{

        private PartidaDeXadrez Partida;

        public Rei(Cor cor, Tabuleiro tab, PartidaDeXadrez partida) : base(cor, tab) {
            Partida = partida;
        }

        public override string ToString() {
            return "R";
        }

        private bool PodeMover(Posicao pos) {
            Peca p = Tab.ReturnPeca(pos);
            return p == null || p.Cor != Cor;
        }
        
        private bool TesteTorreRoque(Posicao pos) {
            Peca p = Tab.ReturnPeca(pos);
            return p != null && p is Torre && p.Cor == Cor && p.QteMovimentos==0;
        }

        public override bool[,] MovimentosPossiveis() {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new Posicao(0, 0);

            //acima

            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if(Tab.PosicaoValida(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //Nordeste

            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna +1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //Direita

            pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //Sudeste

            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //Abaixo

            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna );
            if (Tab.PosicaoValida(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //so

            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //Esquerda

            pos.DefinirValores(Posicao.Linha , Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //No

            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }

            //#ESPECIAL

            //ROQUE 
            if(QteMovimentos ==-0 && !Partida.Xeque) {
                //pequeno 
                Posicao posT1 = new Posicao(Posicao.Linha,Posicao.Coluna+3);
                if (TesteTorreRoque(posT1)) {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);
                    if (Tab.ReturnPeca(p1) == null && Tab.ReturnPeca(p2)== null) {
                        mat[Posicao.Linha, Posicao.Coluna + 2] = true;
                    }
                }

                //grande 
                Posicao posT2 = new Posicao(Posicao.Linha, Posicao.Coluna -4);
                if (TesteTorreRoque(posT2)) {
                    Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
                    Posicao p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);
                    if (Tab.ReturnPeca(p1) == null && Tab.ReturnPeca(p2) == null && Tab.ReturnPeca(p3)==null) {
                        mat[Posicao.Linha, Posicao.Coluna - 2] = true;
                    }
                }

            }



            return mat;


        }
    }
}
