using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CameraIA
{
    internal class ImagePrediction : ImageData
    {
        public float[] Score;
        public string PredictedLabelValue;

        MLContext mLContext = new MLContext();


        struct InceptionSettings
        {
            public const int ImageHeight = 224;
            public const int ImageWidth = 224;
            public const float Mean = 117;
            public const float Scale = 1;
            public const bool ChannelsLast = true;
        }
        ITransformer GenerateModel(MLContext mlContext)
        {
            string _assetsPath = Path.Combine(Environment.CurrentDirectory, "assets");
            string _imagesFolder = Path.Combine(_assetsPath, "images");
            string _inceptionTensorFlowModel = Path.Combine(_assetsPath, "inception", "tensorflow_inception_graph.pb");
            string _trainTagsTsv = Path.Combine(_imagesFolder, "tags.tsv");
            string _testTagsTsv = Path.Combine(_imagesFolder, "test-tags.tsv");

            IEstimator<ITransformer> pipeline = mlContext.Transforms.LoadImages(outputColumnName: "input", imageFolder: _imagesFolder, inputColumnName: nameof(ImageData.ImagePath))
                .Append(mlContext.Transforms.ResizeImages(outputColumnName: "input", imageWidth: InceptionSettings.ImageWidth, imageHeight: InceptionSettings.ImageHeight, inputColumnName: "input"))
                .Append(mlContext.Transforms.ExtractPixels(outputColumnName: "input", interleavePixelColors: InceptionSettings.ChannelsLast, offsetImage: InceptionSettings.Mean))
                .Append(mlContext.Model.LoadTensorFlowModel(_inceptionTensorFlowModel).
                    ScoreTensorFlowModel(outputColumnNames: new[] { "softmax2_pre_activation" }, inputColumnNames: new[] { "input" }, addBatchDimensionInput: true))
                .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "LabelKey", inputColumnName: "Label"))
                .Append(mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy(labelColumnName: "LabelKey", featureColumnName: "softmax2_pre_activation"))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabelValue", "PredictedLabel"))
                .AppendCacheCheckpoint(mlContext);
            IDataView trainingData = mlContext.Data.LoadFromTextFile<ImageData>(path: _trainTagsTsv, hasHeader: false);

            ITransformer model = pipeline.Fit(trainingData);

            IDataView testData = mlContext.Data.LoadFromTextFile<ImageData>(path: _testTagsTsv, hasHeader: false);
            IDataView predictions = model.Transform(testData);
            IEnumerable<ImagePrediction> imagePredictionData = mlContext.Data.CreateEnumerable<ImagePrediction>(predictions, true);
            DisplayResults(imagePredictionData);

            MulticlassClassificationMetrics metrics =
                mlContext.MulticlassClassification.Evaluate(predictions,
                labelColumnName: "LabelKey",
                predictedLabelColumnName: "PredictedLabel");

            Console.WriteLine($"LogLoss is: {metrics.LogLoss}");
            Console.WriteLine($"PerClassLogLoss is: {String.Join(" , ", metrics.PerClassLogLoss.Select(c => c.ToString()))}");

            return model;
        }
        void DisplayResults(IEnumerable<ImagePrediction> predictions)
        {
            foreach(ImagePrediction prediction in predictions)
            {
                Console.WriteLine($"Image: {Path.GetFileName(prediction.ImagePath)} predicted as: {prediction.PredictedLabelValue} with score: {prediction.Score.Max()}");
            }
        }
        string ClassifySingleImage(MLContext mLContext, ITransformer model, string path)
        {
            string _assetsPath = Path.Combine(Environment.CurrentDirectory, "assets");
            string _imagesFolder = Path.Combine(_assetsPath, "images");
            string _trainTagsTsv = Path.Combine(_imagesFolder, "tags.tsv");
            string _testTagsTsv = Path.Combine(_imagesFolder, "test-tags.tsv");
            string _predictSingleImage = Path.Combine(path);
            string _inceptionTensorFlowModel = Path.Combine(_assetsPath, "inception", "tensorflow_inception_graph.pb");

            var imageData = new ImageData()
            {
                ImagePath = _predictSingleImage
            };

            var predictor = mLContext.Model.CreatePredictionEngine<ImageData, ImagePrediction>(model);
            var prediction = predictor.Predict(imageData);

            Interface.predictionText = "My prediction is: " + prediction.PredictedLabelValue;
            Interface.percentageText = "I'm " + (prediction.Score.Max()*100) + "% shure";

            return $"Image: {Path.GetFileName(imageData.ImagePath)} predicted as: {prediction.PredictedLabelValue} with Score: {prediction.Score.Max()}";
        }

        public string StartPrediction(String path)
        {
            ITransformer model = GenerateModel(mLContext);
            return ClassifySingleImage(mLContext, model, path);
        }
    }
}
public class ImageData
{
    [LoadColumn(0)]
    public string ImagePath;
    [LoadColumn(1)]
    public string Label;
}
