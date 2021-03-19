using System;
using System.Collections.Generic;
using System.Text;

namespace FormRecognizer
{
    public class IdentificationResult
    {
        public string Status { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime LastUpdatedDateTime { get; set; }
        public AnalyzeResult AnalyzeResult { get; set; }
    }

    public class AnalyzeResult
    {
        public string Version { get; set; }
        public ReadResult[] ReadResults { get; set; }
        public DocumentResult[] DocumentResults { get; set; }
    }

    public class ReadResult
    {
        public int Page { get; set; }
        public float Angle { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Unit { get; set; }
        public Line[] Lines { get; set; }
    }

    public class Line
    {
        public string Text { get; set; }
        public int[] BoundingBox { get; set; }
        public Word[] Words { get; set; }
        public Appearance Appearance { get; set; }
    }

    public class Appearance
    {
        public Style Style { get; set; }
    }

    public class Style
    {
        public string Name { get; set; }
        public float Confidence { get; set; }
    }

    public class Word
    {
        public string Text { get; set; }
        public int[] BoundingBox { get; set; }
        public float Confidence { get; set; }
    }

    public class DocumentResult
    {
        public string DocType { get; set; }
        public float DocTypeConfidence { get; set; }
        public int[] PageRange { get; set; }
        public Fields Fields { get; set; }
    }

    public class Fields
    {
        public Field Address { get; set; }
        public Field Country { get; set; }
        public Field DateOfBirth { get; set; }
        public Field DateOfExpiration { get; set; }
        public Field DocumentNumber { get; set; }
        public Field FirstName { get; set; }
        public Field LastName { get; set; }
        public Field Region { get; set; }
        public Field Sex { get; set; }
    }

    public class Field
    {
        public string Type { get; set; }
        public string ValueString { get; set; }
        public string ValueDate { get; set; }
        public string ValueCountry { get; set; }
        public string Text { get; set; }
        public int[] BoundingBox { get; set; }
        public int Page { get; set; }
        public float Confidence { get; set; }
        public string[] Elements { get; set; }
        public string ValueGender { get; set; }
    }

}
