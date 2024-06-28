using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diabetia.Domain.Entities
{
    public class FoodDish
    {
        public string? ImageBase64 { get; set; }
        public int ImageId { get; set; }
        public ProcessedImageSize ProcessedImageSize { get; set; }
        public string Occasion { get; set; }
        public List<OccasionInfo> OccasionInfo { get; set; }
        public FoodType FoodType { get; set; }
        public List<FoodFamily> FoodFamily { get; set; }
        public Dictionary<string, string> ModelVersions { get; set; }
        public List<SegmentationResult> SegmentationResults { get; set; }

        public FoodDish()
        {
            OccasionInfo = new List<OccasionInfo>();
            ModelVersions = new Dictionary<string, string>();
            SegmentationResults = new List<SegmentationResult>();
        }

    }


    public class ModelVersions
    {
        public string Drinks { get; set; }
        public string FoodType { get; set; }
        public string FoodGroups { get; set; }
        public string FoodRec { get; set; }
        public string Ingredients { get; set; }
        public string Ontology { get; set; }
        public string Segmentation { get; set; }
    }

    public class ProcessedImageSize
    {
        public int Height { get; set; }

        public int Width { get; set; }
    }

    public class OccasionInfo
    {
        public int? Id { get; set; }
        public string Translation { get; set; }
    }

    public class FoodType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class FoodFamily
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Prob { get; set; }
    }

    public class SegmentationResult
    {
        public int FoodItemPosition { get; set; }
        public Center Center { get; set; }
        public ContainedBoundingBox ContainedBoundingBox { get; set; }
        public List<int> Polygon { get; set; }
        public float ServingSize { get; set; }
        public List<RecognitionResult> RecognitionResults { get; set; }

        public SegmentationResult()
        {
            Polygon = new List<int>();
            RecognitionResults = new List<RecognitionResult>();
        }
    }

    public class Center
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class ContainedBoundingBox
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int H { get; set; }
        public int W { get; set; }
    }

    public class RecognitionResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Prob { get; set; }
        public FoodType FoodType { get; set; }
        public List<FoodFamily> FoodFamily { get; set; }

        public RecognitionResult()
        {
            FoodFamily = new List<FoodFamily>();
        }
    }

    public class Subclass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Prob { get; set; }
        public FoodType FoodType { get; set; }
        public List<FoodFamily> FoodFamily { get; set; }
    }

    public class NutriScore
    {
        public string NutriScoreCategory { get; set; }
        public int NutriScoreStandardized { get; set; }
    }
}