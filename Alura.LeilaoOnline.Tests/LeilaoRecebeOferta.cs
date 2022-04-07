using Xunit;
using Alura.LeilaoOnline.Core;
using System.Linq;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeOferta
    {
        [Theory]
        [InlineData(4, new double[] { 1000, 1200, 1400, 1300 })]
        [InlineData(2, new double[] { 800, 900 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int qtdEsperada, double[] oferta)
        {
            {
                // Arrange
                var leilao = new Leilao("Van Gogh");
                var fulano = new Interessada("Fulano", leilao);

                leilao.IniciaPregao();
                foreach (var valor in oferta)
                {
                    leilao.RecebeLance(fulano, 800);
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
