using GeradorTxt;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace AvaliacaoDotnet.Tests
{
    public class GeradorArquivoTests
    {
        [Test]
        public void DeveGerarArquivoLeiaute1()
        {
            var empresas = new List<Empresa>
            {
                new Empresa
                {
                    CNPJ = "123",
                    Nome = "Empresa Teste",
                    Telefone = "9999",
                    Documentos = new List<Documento>
                    {
                        new Documento
                        {
                            Modelo = "NF",
                            Numero = "1",
                            Valor = 10,
                            Itens = new List<ItemDocumento>
                            {
                                new ItemDocumento
                                {
                                    Descricao = "Item 1",
                                    Valor = 10
                                }
                            }
                        }
                    }
                }
            };

            var arquivo = Path.GetTempFileName();

            var gerador = new GeradorArquivoBase();

            gerador.Gerar(empresas, arquivo);

            Assert.That(File.Exists(arquivo), Is.True);

            File.Delete(arquivo);
        }

        [Test]
        public void DeveLancarExcecaoQuandoValorDocumentoForInvalido()
        {
            var empresas = new List<Empresa>
            {
                new Empresa
                {
                    CNPJ = "123",
                    Nome = "Empresa",
                    Telefone = "9999",
                    Documentos = new List<Documento>
                    {
                        new Documento
                        {
                            Modelo = "NF",
                            Numero = "1",
                            Valor = 100,
                            Itens = new List<ItemDocumento>
                            {
                                new ItemDocumento
                                {
                                    Descricao = "Item",
                                    Valor = 10
                                }
                            }
                        }
                    }
                }
            };

            var gerador = new GeradorArquivoBase();

            Assert.Throws<Exception>(() =>
                gerador.Gerar(empresas, Path.GetTempFileName()));
        }

        [Test]
        public void DeveGerarArquivoLeiaute2()
        {
            var empresas = new List<Empresa>
            {
                new Empresa
                {
                    CNPJ = "123",
                    Nome = "Empresa",
                    Telefone = "9999",
                    Documentos = new List<Documento>
                    {
                        new Documento
                        {
                            Modelo = "NF",
                            Numero = "1",
                            Valor = 10,
                            Itens = new List<ItemDocumento>
                            {
                                new ItemDocumento
                                {
                                    NumeroItem = 1,
                                    Descricao = "Item",
                                    Valor = 10,
                                    Categorias = new List<CategoriaItem>
                                    {
                                        new CategoriaItem
                                        {
                                            NumeroCategoria = 1,
                                            DescricaoCategoria = "Categoria"
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var arquivo = Path.GetTempFileName();

            var gerador = new GeradorArquivoV2();

            gerador.Gerar(empresas, arquivo);

            var texto = File.ReadAllText(arquivo);

            Assert.That(texto.Contains("03|1|Categoria"), Is.True);

            File.Delete(arquivo);
        }
    }
}