using checkpoint3.Data;
using Checkpoint3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.IO;
using System.Linq;

namespace Checkpoint3.Controllers
{
    public class DadosLivro
    {
        [LoadColumn(0)] public string Titulo { get; set; }
        [LoadColumn(1)] public string Autor { get; set; }
        [LoadColumn(2)] public string Genero { get; set; }
        [LoadColumn(3)] public int AnoPublicacao { get; set; }
        [LoadColumn(4)] public decimal? Classificacao { get; set; }
    }

    public class PrevisaoLivro
    {
        [ColumnName("PredictedLabel")]
        public string PrevisaoTitulo { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class PrevisaoLivroController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly string caminhoModelo = Path.Combine(Environment.CurrentDirectory, "wwwroot", "MLModels", "ModeloLivro.zip");
        private readonly MLContext mlContext;

        public PrevisaoLivroController(AppDbContext context)
        {
            _context = context;
            mlContext = new MLContext();

            if (!System.IO.File.Exists(caminhoModelo))
            {
                Console.WriteLine("Modelo não encontrado. Iniciando treinamento");
                TreinarModelo();
            }
        }

        private void TreinarModelo()
        {
            var pastaModelo = Path.GetDirectoryName(caminhoModelo);
            if (!Directory.Exists(pastaModelo))
            {
                Directory.CreateDirectory(pastaModelo);
                Console.WriteLine($"Diretório criado: {pastaModelo}");
            }

            var livros = _context.Livros.Select(l => new DadosLivro
            {
                Titulo = l.Titulo,
                Autor = l.Autor,
                Genero = l.Genero,
                AnoPublicacao = l.AnoPublicacao,
                Classificacao = l.Classificacao
            }).ToList();

            IDataView dadosTreinamento = mlContext.Data.LoadFromEnumerable(livros);

            var pipeline = mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(DadosLivro.Titulo))
                .Append(mlContext.Transforms.Categorical.OneHotEncoding("GeneroEncoded", nameof(DadosLivro.Genero)))
                .Append(mlContext.Transforms.Concatenate("Features", "GeneroEncoded", nameof(DadosLivro.AnoPublicacao), nameof(DadosLivro.Classificacao)))
                .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PrevisaoTitulo", "PredictedLabel"));

            var modelo = pipeline.Fit(dadosTreinamento);

            mlContext.Model.Save(modelo, dadosTreinamento.Schema, caminhoModelo);
            Console.WriteLine($"Modelo treinado e salvo em: {caminhoModelo}");
        }

        [HttpPost("prever")]
        public ActionResult<PrevisaoLivro> PreverLivro([FromBody] DadosLivro dados)
        {
            if (!System.IO.File.Exists(caminhoModelo))
            {
                return BadRequest("O modelo ainda não foi treinado.");
            }

            ITransformer modelo;
            try
            {
                using (var stream = new FileStream(caminhoModelo, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    modelo = mlContext.Model.Load(stream, out var modeloSchema);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao carregar o modelo: {ex.Message}");
            }

            var enginePrevisao = mlContext.Model.CreatePredictionEngine<DadosLivro, PrevisaoLivro>(modelo);

            PrevisaoLivro previsao;
            try
            {
                previsao = enginePrevisao.Predict(dados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao prever o livro: {ex.Message}");
            }

            if (previsao == null || string.IsNullOrEmpty(previsao.PrevisaoTitulo))
            {
                return Ok(new PrevisaoLivro { PrevisaoTitulo = "Título não encontrado" });
            }

            return Ok(previsao);
        }
    }
}
