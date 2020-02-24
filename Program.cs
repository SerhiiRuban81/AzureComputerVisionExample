using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.IO;

namespace AzureComputerVisionExample
{
    class Program
    {
        //static string subscriptionKey = "9b5ef114e7924ffc9d75d05d0cb66514";
        static string subscriptionKey = "ba3d4382148448a6873c78ad6f9c4c0b";
        static string endPoint = "https://mypcvision.cognitiveservices.azure.com/";
        static string landMarkUrl = "https://espanarusa.com/files/autoupload/94/45/7/rgmvapq3371736.jpg";
        static string imageUrl = "https://vogue.ua/media/cache/resolve/inline_990x/uploads/article-inline/98d/2f2/e44/5e00e442f298d.jpeg";
        static string brandUrl = "https://www.shop-fcdk.com/ru/media/catalog/product/cache/4/image/9df78eab33525d08d6e5fb8d27136e95/i/m/img_7330_2.jpg";
        static string textUrl = "https://mir-s3-cdn-cf.behance.net/project_modules/disp/1fbe7074222345.5c26f6d6ea509.png";
        static async Task Main(string[] args)
        {
            ComputerVisionClient visionClient = Authenticate(subscriptionKey, endPoint);
            //анализ содержимого изображений
            ///await AnalyzeImageAsync(visionClient, imageUrl);
            //получение списка категорий изображения
            //await GetCategoriesAsync(visionClient, imageUrl);
            //получение тегов изображения
            //await GetTagsAsync(visionClient, imageUrl);
            //получение объектов, находяихся на изображении
            //await GetObjectsAsync(visionClient, imageUrl);
            //получение информации и брендах, находящихся на изображении
            //await GetBrandsAsync(visionClient, brandUrl);
            //анализ изображение на наличие недопустимого содержимого
            //await CheckForAdultContentAsync(visionClient, imageUrl);
            //получение информации о лицах на изображении
            //await GetFacesAsync(visionClient, imageUrl);
            //получение информации о цветовой гамме изображения
            //await GetColorSchemeAsync(visionClient, imageUrl);
            //получение содержимого, связанного с определенной областью
            //await GetCelebritiesAsync(visionClient, imageUrl);
            //await GetLandMarksAsync(visionClient, landMarkUrl);
            await GetTextFromImageAsync(visionClient, textUrl);
        }

        static ComputerVisionClient Authenticate(string key, string endpoint)
        {
            ComputerVisionClient visionClient = new ComputerVisionClient(
                new ApiKeyServiceClientCredentials(key))
            { Endpoint = endpoint };
            return visionClient;
        }
        static async Task AnalyzeImageAsync(ComputerVisionClient visionClient, string url)
        {
            List<VisualFeatureTypes> features = Enum.GetValues(typeof(VisualFeatureTypes)).OfType<VisualFeatureTypes>().ToList();
            Console.WriteLine($"Analizing image {Path.GetFileName(url)}");
            Console.WriteLine("---------------");
            ImageAnalysis analysis = await visionClient.AnalyzeImageAsync(url, features);
            foreach (var caption in analysis.Description.Captions)
            {
                Console.WriteLine($"{caption.Text} with {caption.Confidence}");
            }
        }

        static async Task GetCategoriesAsync(ComputerVisionClient visionClient, string url)
        {
            List<VisualFeatureTypes> features = Enum.GetValues(typeof(VisualFeatureTypes)).OfType<VisualFeatureTypes>().ToList();
            Console.WriteLine($"Analizing image {Path.GetFileName(url)}");
            Console.WriteLine("---------------");
            ImageAnalysis analysis = await visionClient.AnalyzeImageAsync(url, features);
            foreach (var category in analysis.Categories)
            {
                Console.WriteLine($"{category.Name} с достоверностью {category.Score}");
            }
        }

        static async Task GetTagsAsync(ComputerVisionClient visionClient, string url)
        {
            List<VisualFeatureTypes> features = Enum.GetValues(typeof(VisualFeatureTypes)).OfType<VisualFeatureTypes>().ToList();
            Console.WriteLine($"Анализируемое изображение {Path.GetFileName(url)}");
            Console.WriteLine("---------------");
            ImageAnalysis analysis = await visionClient.AnalyzeImageAsync(url, features);
            foreach (var tag in analysis.Tags)
            {
                Console.WriteLine($"{tag.Name} с достоверностью {tag.Confidence}");
            }
        }

        static async Task GetObjectsAsync(ComputerVisionClient visionClient, string url)
        {
            List<VisualFeatureTypes> features = Enum.GetValues(typeof(VisualFeatureTypes)).OfType<VisualFeatureTypes>().ToList();
            Console.WriteLine($"Анализируемое изображение {Path.GetFileName(url)}");
            Console.WriteLine("---------------");
            ImageAnalysis analysis = await visionClient.AnalyzeImageAsync(url, features);
            foreach (var obj in analysis.Objects)
            {
                Console.WriteLine($"Объект {obj.ObjectProperty} с достоверностью {obj.Confidence}" +
                    $"находится в области ({obj.Rectangle.X},{obj.Rectangle.Y}) - " +
                    $"({obj.Rectangle.X + obj.Rectangle.W}, {obj.Rectangle.Y + obj.Rectangle.H})");
            }
        }

        static async Task GetBrandsAsync(ComputerVisionClient visionClient, string url)
        {
            List<VisualFeatureTypes> features = Enum.GetValues(typeof(VisualFeatureTypes)).OfType<VisualFeatureTypes>().ToList();
            Console.WriteLine($"Анализируемое изображение {Path.GetFileName(url)}");
            Console.WriteLine("---------------");
            ImageAnalysis analysis = await visionClient.AnalyzeImageAsync(url, features);
            foreach (var brand in analysis.Brands)
            {
                Console.WriteLine($"Объект {brand.Name} с достоверностью {brand.Confidence}" +
                   $"находится в области ({brand.Rectangle.X},{brand.Rectangle.Y}) - " +
                   $"({brand.Rectangle.X + brand.Rectangle.W}, {brand.Rectangle.Y + brand.Rectangle.H})");
            }
        }

        static async Task CheckForAdultContentAsync(ComputerVisionClient visionClient, string url)
        {

            List<VisualFeatureTypes> features = Enum.GetValues(typeof(VisualFeatureTypes)).OfType<VisualFeatureTypes>().ToList();
            Console.WriteLine($"Анализируемое изображение {Path.GetFileName(url)}");
            Console.WriteLine("---------------");
            ImageAnalysis analysis = await visionClient.AnalyzeImageAsync(url, features);
            Console.WriteLine("--------Запрещенное содержимое-----------");
            Console.WriteLine($"Изображение содержит контент для взрослых: {analysis.Adult.IsAdultContent} с достоверностью  {analysis.Adult.AdultScore}");
            Console.WriteLine($"Изображение содержит расистский контент: {analysis.Adult.IsRacyContent} с достоверностью {analysis.Adult.RacyScore}");
        }

        static async Task GetFacesAsync(ComputerVisionClient visionClient, string url)
        {
            List<VisualFeatureTypes> features = Enum.GetValues(typeof(VisualFeatureTypes)).OfType<VisualFeatureTypes>().ToList();
            Console.WriteLine($"Анализируемое изображение {Path.GetFileName(url)}");
            Console.WriteLine("---------Лица на изображении----------");
            ImageAnalysis analysis = await visionClient.AnalyzeImageAsync(url, features);
            foreach (var face in analysis.Faces)
            {
                Console.WriteLine($"Обнаружено лицо, пол : {face.Gender}, возраст: {face.Age} ");
                Console.WriteLine($"в области ({face.FaceRectangle.Left}, {face.FaceRectangle.Top})" +
                    $"({face.FaceRectangle.Left + face.FaceRectangle.Width}, {face.FaceRectangle.Top + face.FaceRectangle.Height})");
            }
        }

        static async Task GetColorSchemeAsync(ComputerVisionClient visionClient, string url)
        {
            List<VisualFeatureTypes> features = Enum.GetValues(typeof(VisualFeatureTypes)).OfType<VisualFeatureTypes>().ToList();
            Console.WriteLine($"Анализируемое изображение {Path.GetFileName(url)}");
            ImageAnalysis analysis = await visionClient.AnalyzeImageAsync(url, features);
            Console.WriteLine($"Акцентный цвет: {analysis.Color.AccentColor}");
            Console.WriteLine($"Преобладающи цвет фона: {analysis.Color.DominantColorBackground}");
            Console.WriteLine($"Преобладающий основной цвет: {analysis.Color.DominantColorForeground}");
            Console.WriteLine("Основные цвета: " + string.Join(", ", analysis.Color.DominantColors));
            Console.WriteLine("Анализируемое изображение " + (analysis.Color.IsBWImg ? "черно-белое" : "цветное"));
        }


        static async Task GetCelebritiesAsync(ComputerVisionClient visionClient, string url)
        {
            List<VisualFeatureTypes> features = Enum.GetValues(typeof(VisualFeatureTypes)).OfType<VisualFeatureTypes>().ToList();
            Console.WriteLine($"Анализируемое изображение {Path.GetFileName(url)}");
            ImageAnalysis analysis = await visionClient.AnalyzeImageAsync(url, features);
            foreach (var category in analysis.Categories)
            {
                if (category.Detail?.Celebrities != null)
                {
                    foreach (var celeb in category.Detail.Celebrities)
                    {
                        Console.WriteLine($"Знаменитость {celeb.Name} c достоверностью {celeb.Confidence}");
                        Console.WriteLine($"обнаружена в области ({celeb.FaceRectangle.Left}, {celeb.FaceRectangle.Top})" +
                    $"({celeb.FaceRectangle.Left + celeb.FaceRectangle.Width}, {celeb.FaceRectangle.Top + celeb.FaceRectangle.Height})");
                    }
                }
            }
        }

        static async Task GetLandMarksAsync(ComputerVisionClient visionClient, string url)
        {
            List<VisualFeatureTypes> features = Enum.GetValues(typeof(VisualFeatureTypes)).OfType<VisualFeatureTypes>().ToList();
            Console.WriteLine($"Анализируемое изображение {Path.GetFileName(url)}");
            ImageAnalysis analysis = await visionClient.AnalyzeImageAsync(url, features);
            foreach (var category in analysis.Categories)
            {
                if (category.Detail?.Landmarks != null)
                {
                    foreach (var landMark in category.Detail.Landmarks)
                    {
                        Console.WriteLine($"Достопримечательность {landMark.Name} c достоверностью {landMark.Confidence}");
                    }
                }
            }
        }

        static async Task GetTextFromImageAsync(ComputerVisionClient visionClient, string url)
        {
            BatchReadFileHeaders textHeaders = await visionClient.BatchReadFileAsync(url);
            string operationLocation = textHeaders.OperationLocation;
            int maxCharsInOperationId = 36;
            string operationId = operationLocation.Substring(operationLocation.Length - maxCharsInOperationId);
            int i = 0;
            int maxRetries = 10;
            ReadOperationResult results = null;
            do
            {
                results = await visionClient.GetReadOperationResultAsync(operationId);
                Console.WriteLine($"Статус сервера {results.Status}, выполняется {i} c");
                await Task.Delay(1000);
                if(i==9)
                    Console.WriteLine("Сервер вызвал таймаут");
            }
            while ((results.Status == TextOperationStatusCodes.Running || 
            results.Status == TextOperationStatusCodes.NotStarted) && i++ < maxRetries);
            Console.WriteLine();
            var textRecognitionResults = results.RecognitionResults;
            foreach (TextRecognitionResult textRecognitionResult in results.RecognitionResults)
            {
                foreach (var line in textRecognitionResult.Lines)
                {
                    Console.WriteLine(line.Text);
                }
            }

        }
    }
}





