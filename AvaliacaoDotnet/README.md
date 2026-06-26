# Avaliação Técnica - Gerador de Arquivos TXT

Projeto desenvolvido como parte de um desafio técnico, com o objetivo de evoluir um gerador de arquivos TXT baseado em dados de um arquivo JSON, suportando múltiplas versões de leiaute.

## Funcionalidades

* Geração do Leiaute 1
* Geração do Leiaute 2
* Seleção da versão do leiaute via console
* Validação da soma dos itens do documento
* Geração das linhas 09 (total por tipo de registro)
* Geração da linha 99 (total de linhas do arquivo)
* Testes unitários utilizando NUnit

## Tecnologias

* C#
* .NET Framework 4.8
* Newtonsoft.Json
* NUnit

## Como executar

1. Clone o repositório.
2. Abra a solution `AvaliacaoDotnet.sln`.
3. Compile o projeto.
4. Execute a aplicação.
5. Configure o caminho do arquivo JSON.
6. Configure o diretório de saída.
7. Escolha a versão do leiaute desejada para gerar o arquivo.

## Executando os testes

```bash
dotnet test
```

## Estrutura do projeto

* **AvaliacaoDotnet**: aplicação principal responsável pela geração dos arquivos.
* **AvaliacaoDotnet.Tests**: testes unitários utilizando NUnit.

## Challenge

This is a challenge by Coodesh: https://coodesh.com/
