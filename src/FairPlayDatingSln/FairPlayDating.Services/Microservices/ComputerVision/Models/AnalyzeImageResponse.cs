using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FairPlayDating.Services.Microservices.ComputerVision.Models
{

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public class AnalyzeImageResponse
    {
        public Category[] categories { get; set; }
        public Adult adult { get; set; }
        public Tag[] tags { get; set; }
        public Description description { get; set; }
        public string requestId { get; set; }
        public Metadata metadata { get; set; }
        public Face[] faces { get; set; }
        public Color color { get; set; }
        public Imagetype imageType { get; set; }
        public Object[] objects { get; set; }
    }

    public class Adult
    {
        public bool isAdultContent { get; set; }
        public bool isRacyContent { get; set; }
        public bool isGoryContent { get; set; }
        public float adultScore { get; set; }
        public float racyScore { get; set; }
        public float goreScore { get; set; }
    }

    public class Description
    {
        public string[] tags { get; set; }
        public Caption[] captions { get; set; }
    }

    public class Caption
    {
        public string text { get; set; }
        public float confidence { get; set; }
    }

    public class Metadata
    {
        public int width { get; set; }
        public int height { get; set; }
        public string format { get; set; }
    }

    public class Color
    {
        public string dominantColorForeground { get; set; }
        public string dominantColorBackground { get; set; }
        public string[] dominantColors { get; set; }
        public string accentColor { get; set; }
        public bool isBWImg { get; set; }
    }

    public class Imagetype
    {
        public int clipArtType { get; set; }
        public int lineDrawingType { get; set; }
    }

    public class Category
    {
        public string name { get; set; }
        public float score { get; set; }
        public Detail detail { get; set; }
    }

    public class Detail
    {
        public Celebrity[] celebrities { get; set; }
        public Landmark[] landmarks { get; set; }
    }

    public class Celebrity
    {
        public string name { get; set; }
        public Facerectangle faceRectangle { get; set; }
        public float confidence { get; set; }
    }

    public class Facerectangle
    {
        public int left { get; set; }
        public int top { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Landmark
    {
        public string name { get; set; }
        public float confidence { get; set; }
    }

    public class Tag
    {
        public string name { get; set; }
        public float confidence { get; set; }
    }

    public class Face
    {
        public int age { get; set; }
        public string gender { get; set; }
        public Facerectangle1 faceRectangle { get; set; }
    }

    public class Facerectangle1
    {
        public int left { get; set; }
        public int top { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class Object
    {
        public Rectangle rectangle { get; set; }
        public string _object { get; set; }
        public float confidence { get; set; }
    }

    public class Rectangle
    {
        public int x { get; set; }
        public int y { get; set; }
        public int w { get; set; }
        public int h { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}