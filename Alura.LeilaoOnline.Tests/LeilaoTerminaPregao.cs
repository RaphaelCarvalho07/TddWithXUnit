using Alura.LeilaoOnline.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
        [Theory]
        [InlineData(1200, new double[] { 800, 900, 1000, 1200 })]
        [InlineData(1000, new double[] { 800, 900, 1000, 990 })]
        [InlineData(800, new double[] { 800 })]
        public void RetornaMaiorValorDadoLeilaoComApenasUmLance(double valorEsperado, double[] lances)
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

            // Act - método em teste
            leilao.TerminaPregao();

            // Assert
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }

        [Fact]
        public void LancaInvalidOperationExceptionDadoPregaoNaoIniciado()
        {
            // Arrange
            var leilao = new Leilao("Van Gogh");

            try
            {
                // Act - método em teste
                leilao.TerminaPregao();
                Assert.True(false);
            }
            catch (System.Exception e)
            {
                // Assert
                Assert.IsType<InvalidOperationException>(e);
            }
        }

        [Fact]
        public void RetornaZeroDadoLeilaoSemLances()
        {
            // Arrange
            var leilao = new Leilao("Van Gogh");

            // Act - método em teste
            leilao.TerminaPregao();

            // Assert
            var valorEsperado = 0;
            var valorObtido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
