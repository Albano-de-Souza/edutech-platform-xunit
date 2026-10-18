using EduTechPlatform.App;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var notas = new NotaDesempenhoService();
var frequencia = new FrequenciaService();
var matricula = new MatriculaFinanceiraService();

Console.WriteLine("=== EduTechPlatform ===");
Console.WriteLine($"Média das notas 7, 7 e 7: {notas.CalcularMediaPonderada(7.0m, 7.0m, 7.0m):N2}");
Console.WriteLine($"Média das notas 10, 10 e 5: {notas.CalcularMediaPonderada(10.0m, 10.0m, 5.0m):N2}");
Console.WriteLine($"Situação com média 8,5: {notas.ObterSituacaoAcademica(8.5m)}");
Console.WriteLine($"Situação com média 6,0: {notas.ObterSituacaoAcademica(6.0m)}");
Console.WriteLine($"Situação com média 4,2: {notas.ObterSituacaoAcademica(4.2m)}");
Console.WriteLine($"Presença com 80 aulas e 20 faltas: {frequencia.CalcularPorcentagemPresenca(80, 20):N1}%");
Console.WriteLine($"Presença com 100 aulas e 10 faltas: {frequencia.CalcularPorcentagemPresenca(100, 10):N1}%");
Console.WriteLine($"Aprovado por frequência (80 aulas, 20 faltas): {frequencia.AprovadoPorFrequencia(80, 20)}");
Console.WriteLine($"Aprovado por frequência (80 aulas, 21 faltas): {frequencia.AprovadoPorFrequencia(80, 21)}");
Console.WriteLine($"Mensalidade R$ 1.000, dia 10, bolsista: R$ {matricula.CalcularValorMensalidade(1000.00m, 10, true):N2}");
Console.WriteLine($"Mensalidade R$ 1.000, dia 4, não bolsista: R$ {matricula.CalcularValorMensalidade(1000.00m, 4, false):N2}");
Console.WriteLine($"Mensalidade R$ 1.000, dia 10, não bolsista: R$ {matricula.CalcularValorMensalidade(1000.00m, 10, false):N2}");
Console.WriteLine($"Rematrícula sem pendência e APROVADO: {matricula.PodeRealizarRematricula(false, "APROVADO")}");
Console.WriteLine($"Rematrícula com pendência e APROVADO: {matricula.PodeRealizarRematricula(true, "APROVADO")}");
Console.WriteLine($"Rematrícula sem pendência e REPROVADO: {matricula.PodeRealizarRematricula(false, "REPROVADO")}");
