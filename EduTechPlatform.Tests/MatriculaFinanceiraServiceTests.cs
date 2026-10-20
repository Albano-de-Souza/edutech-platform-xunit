using EduTechPlatform.App;

namespace EduTechPlatform.Tests;

public class MatriculaFinanceiraServiceTests
{
    private readonly MatriculaFinanceiraService _service = new MatriculaFinanceiraService();

    // O [InlineData] não aceita valores decimal; os valores chegam como double
    // e são convertidos para decimal dentro do teste.
    [Theory]
    [InlineData(1000.00, 10, true, 500.00)]
    [InlineData(1000.00, 4, false, 900.00)]
    [InlineData(1000.00, 10, false, 1000.00)]
    public void CalcularValorMensalidade_DeveAplicarDescontoCorreto(double valorBase, int diaPagamento, bool eBolsista, double valorEsperado)
    {
        var resultado = _service.CalcularValorMensalidade((decimal)valorBase, diaPagamento, eBolsista);

        Assert.Equal((decimal)valorEsperado, resultado);
    }

    [Theory]
    [InlineData(false, "APROVADO", true)]
    [InlineData(true, "APROVADO", false)]
    [InlineData(false, "REPROVADO", false)]
    public void PodeRealizarRematricula_DeveValidarPendenciaESituacao(bool possuiPendenciaFinanceira, string situacaoAcademica, bool esperado)
    {
        var resultado = _service.PodeRealizarRematricula(possuiPendenciaFinanceira, situacaoAcademica);

        Assert.Equal(esperado, resultado);
    }
}
