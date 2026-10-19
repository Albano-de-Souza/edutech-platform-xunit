# edutech-platform-xunit

## Visão geral

A EduTechPlatform é uma aplicação de console em .NET 10 que reúne regras de negócio de uma plataforma acadêmica, distribuídas em três classes de serviço: desempenho (notas e situação do aluno), frequência (presença e aprovação por frequência) e matrícula financeira (mensalidade e rematrícula). As regras são validadas por testes unitários parametrizados em xUnit. Atividade da disciplina Gestão e Qualidade de Software.

## Estrutura

- `EduTechPlatform.App`: código de produção (classes `NotaDesempenhoService`, `FrequenciaService` e `MatriculaFinanceiraService`)
- `EduTechPlatform.Tests`: testes unitários (classes `NotaDesempenhoServiceTests`, `FrequenciaServiceTests` e `MatriculaFinanceiraServiceTests`)

## Requisitos do ambiente

- .NET 10 SDK (projetos com `TargetFramework` `net10.0`)
- Git
- xUnit (restaurado automaticamente pelo `dotnet`)

## Responsabilidade das classes de serviço

### NotaDesempenhoService

Calcula o desempenho acadêmico do aluno.

| Método | Retorno | Regra |
|---|---|---|
| `CalcularMediaPonderada(decimal nota1, decimal nota2, decimal notaProjeto)` | `decimal` | Média com pesos 3, 3 e 4: `((nota1 * 3) + (nota2 * 3) + (notaProjeto * 4)) / 10` |
| `ObterSituacaoAcademica(decimal mediaFinal)` | `string` | `"APROVADO"` para média maior ou igual a 7,0; `"RECUPERACAO"` de 5,0 a 6,99; `"REPROVADO"` abaixo de 5,0 |

### FrequenciaService

Controla a presença do aluno nas aulas.

| Método | Retorno | Regra |
|---|---|---|
| `CalcularPorcentagemPresenca(int aulasTotais, int faltas)` | `double` | Percentual de presença: `((aulasTotais - faltas) / (double)aulasTotais) * 100` |
| `AprovadoPorFrequencia(int aulasTotais, int faltas)` | `bool` | `true` se a presença for de no mínimo 75% |

### MatriculaFinanceiraService

Trata a parte financeira da matrícula.

| Método | Retorno | Regra |
|---|---|---|
| `CalcularValorMensalidade(decimal valorBase, int diaPagamento, bool eBolsista)` | `decimal` | Bolsista: 50% de desconto em qualquer dia. Não bolsista pagando até o dia 5: 10% de desconto. Não bolsista pagando após o dia 5: valor integral |
| `PodeRealizarRematricula(bool possuiPendenciaFinanceira, string situacaoAcademica)` | `bool` | `true` apenas se não houver pendência financeira e a situação acadêmica não for `"REPROVADO"` |

## Guia de execução pelo terminal

Clonar o repositório:

```bash
git clone https://github.com/Albano-de-Souza/edutech-platform-xunit.git
cd edutech-platform-xunit
```

Restaurar as dependências:

```bash
dotnet restore
```

Compilar a solução:

```bash
dotnet build
```

Executar os testes unitários:

```bash
dotnet test
```

Resultado esperado: `total: 15; falhou: 0; bem-sucedido: 15`.

Para rodar a aplicação, que imprime exemplos de cada regra:

```bash
dotnet run --project EduTechPlatform.App
```

## Cobertura dos testes parametrizados com [Theory] e [InlineData]

Os testes usam `[Theory]` com `[InlineData]`: cada método de teste recebe parâmetros e é executado uma vez para cada linha de dados, e cada `[InlineData]` aparece no resultado como um teste individual. São 6 métodos de teste e 15 cenários, cobrindo os seis métodos das três classes de serviço.

Como atributos em C# não aceitam valores `decimal`, notas e valores monetários são informados no `[InlineData]` como `double` e convertidos para `decimal` dentro de cada teste. O percentual de presença, que é `double`, é comparado com precisão de 2 casas decimais.

### NotaDesempenhoServiceTests

| Teste | Dados de entrada | Resultado esperado | Cenário |
|---|---|---|---|
| Média ponderada | `7.0`, `7.0`, `7.0` | `7.0` | Notas iguais |
| Média ponderada | `10.0`, `10.0`, `5.0` | `8.0` | (30 + 30 + 20) / 10 |
| Situação acadêmica | `8.5` | `"APROVADO"` | Média maior ou igual a 7,0 |
| Situação acadêmica | `6.0` | `"RECUPERACAO"` | Média entre 5,0 e 6,99 |
| Situação acadêmica | `4.2` | `"REPROVADO"` | Média abaixo de 5,0 |

### FrequenciaServiceTests

| Teste | Dados de entrada | Resultado esperado | Cenário |
|---|---|---|---|
| Percentual de presença | `80`, `20` | `75.0` | 60 presenças em 80 aulas |
| Percentual de presença | `100`, `10` | `90.0` | 90 presenças em 100 aulas |
| Aprovação por frequência | `80`, `20` | `true` | 75% de presença |
| Aprovação por frequência | `80`, `21` | `false` | Abaixo de 75%, reprovado por falta |

### MatriculaFinanceiraServiceTests

| Teste | Dados de entrada | Resultado esperado | Cenário |
|---|---|---|---|
| Valor da mensalidade | `1000.00`, `10`, `true` | `500.00` | Bolsista, 50% de desconto |
| Valor da mensalidade | `1000.00`, `4`, `false` | `900.00` | Não bolsista, paga no dia 4 |
| Valor da mensalidade | `1000.00`, `10`, `false` | `1000.00` | Não bolsista, paga no dia 10 |
| Rematrícula | `false`, `"APROVADO"` | `true` | Sem pendência e aprovado |
| Rematrícula | `true`, `"APROVADO"` | `false` | Com pendência financeira |
| Rematrícula | `false`, `"REPROVADO"` | `false` | Aluno reprovado |

## Equipe

| Papel | Responsável | Entregas |
|---|---|---|
| Desenvolvedor 1 (Backend / Core) | Albano de Souza | Solução, projetos e as três classes de serviço |
| Desenvolvedor 2 (QA / Testes) | Alice Fernandes Barbosa | `NotaDesempenhoServiceTests` |
| Desenvolvedor 2 (QA / Testes) | Débora Gomes | `FrequenciaServiceTests` e `MatriculaFinanceiraServiceTests` |
| Desenvolvedor 3 (Documentação / DevOps) | Ana Carolina de Sousa Freitas | `.gitignore`, licença MIT e `README.md` |

## Licença

MIT
