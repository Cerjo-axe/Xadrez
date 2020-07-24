using System;
using tabuleiro;

namespace Xadrez_console {
    class Tela {
        public static void ImprimirTabuleiro(Tabuleiro tab) {
            for (int i = 0; i < tab.Linhas; i++) {
                for (int j = 0; j < tab.Colunas; j++) {

                    if (tab.ReturnPeca(i, j) == null) {
                        Console.Write("-  ");
                    } else {

                        Console.Write(tab.ReturnPeca(i, j) + " ");
                    }

                }
                Console.WriteLine();
            }
        }
    }
}
