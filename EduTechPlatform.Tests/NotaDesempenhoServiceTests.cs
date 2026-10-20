using EduTechPlatform.App;

namespace EduTechPlatform.Tests;

public class NotaDesempenhoServiceTests
{
    private readonly NotaDesempenhoService _service = new NotaDesempenhoService();

    // O [InlineData] não aceita valores decimal; as notas chegam como double
    // e são convertidas para decimal dentro do teste.
    [Theory]
    [InlineData(7.0, 7.0, 7.0, 7.0)]
    [InlineData(10.0, 10.0, 5.0, 8.0)]
    public void CalcularMediaPonderada_DeveAplicarPesos(double nota1, double nota2, double notaProjeto, double mediaEsperada)
    {
        var resultado = _service.CalcularMediaPonderada((decimal)nota1, (decimal)nota2, (decimal)notaProjeto);

        Assert.Equal((decimal)mediaEsperada, resultado);
    }

    [Theory]
    [InlineData(8.5, "APROVADO")]
    [InlineData(6.0, "RECUPERACAO")]
    [InlineData(4.2, "REPROVADO")]
    public void ObterSituacaoAcademica_DeveClassificarPelaMedia(double mediaFinal, string situacaoEsperada)
    {
        var resultado = _service.ObterSituacaoAcademica((decimal)mediaFinal);

        Assert.Equal(situacaoEsperada, resultado);
    }
}
