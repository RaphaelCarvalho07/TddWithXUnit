using Xunit;
using Alura.LeilaoOnline.Core;
using System.Linq;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeLance
    {
        [Fact]
        public void NaoAceitaProximoLanceDadoMesmoCLienteRealizouUltimoLance()
        {
            {
                // Arrange
                var leilao = new Leilao("Van Gogh");
                var fulano = new Interessada("Fulano", leilao);

                leilao.IniciaPregao();
                leilao.RecebeLance(fulano, 800);


                // Act - método em teste
                leilao.RecebeLance(fulano, 1000);

                // Assert
                var qtdEsperada = 1;
                var qtdObtida = leilao.Lances.Count();
                Assert.Equal(qtdEsperada, qtdObtida);
            }
        }


        [Theory]
        [InlineData(4, new double[] { 1000, 1200, 1400, 1300 })]
        [InlineData(2, new double[] { 800, 900 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int qtdEsperada, double[] lances)
        {
            {
                // Arrange
                var leilao = new Leilao("Van Gogh");
                var fulano = new Interessada("Fulano", leilao);
                var maria = new Interessada("Maria", leilao);

                leilao.IniciaPregao();
                for (int i = 0; i < lances.Length; i++)
                {
                    var valor = lances[i];
                    if ((i % 2) == 0)
                    {
                        leilao.RecebeLance(fulano, valor);
                    }
                    else
                    {
                        leilao.RecebeLance(maria, valor);
                    }
                }

                leilao.TerminaPregao();

                // Act - método em teste
                leilao.RecebeLance(fulano, 1000);

                // Assert
                var qtdObtida = leilao.Lances.Count();
                Assert.Equal(qtdEsperada, qtdObtida);
            }
        }
    }
}
