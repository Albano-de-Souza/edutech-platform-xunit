namespace EduTechPlatform.App;

public class FrequenciaService
{
    public double CalcularPorcentagemPresenca(int aulasTotais, int faltas)
    {
        return ((aulasTotais - faltas) / (double)aulasTotais) * 100;
    }

    public bool AprovadoPorFrequencia(int aulasTotais, int faltas)
    {
        return CalcularPorcentagemPresenca(aulasTotais, faltas) >= 75.0;
    }
}
