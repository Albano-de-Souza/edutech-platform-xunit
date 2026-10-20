using EduTechPlatform.App;

namespace EduTechPlatform.Tests;

public class FrequenciaServiceTests
{
    private readonly FrequenciaService _service = new FrequenciaService();

    [Theory]
    [InlineData(80, 20, 75.0)]
    [InlineData(100, 10, 90.0)]
    public void CalcularPorcentagemPresenca_DeveRetornarPercentualCorreto(int aulasTotais, int faltas, double percentualEsperado)
    {
        var resultado = _service.CalcularPorcentagemPresenca(aulasTotais, faltas);

        // Comparação com 2 casas decimais, adequada para valores double.
        Assert.Equal(percentualEsperado, resultado, 2);
    }

    [Theory]
    [InlineData(80, 20, true)]
    [InlineData(80, 21, false)]
    public void AprovadoPorFrequencia_DeveExigirMinimoDe75PorCento(int aulasTotais, int faltas, bool esperado)
    {
        var resultado = _service.AprovadoPorFrequencia(aulasTotais, faltas);

        Assert.Equal(esperado, resultado);
    }
}
