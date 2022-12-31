using CsvHelper.Configuration.Attributes;

public class TokyoCrawlModel {
    public string idx {get; set;}
    public string location {get; set;}
    [Name("location-url")]
    public string location_url {get; set;}
    public string area {get; set;}

    [Name("area-url")]
    public string area_url {get; set;}
    public string? category {get;set;}
    public string? content {get; set;}

    [Name("content-url")]
    public string? content_url {get;set;}

    [Name("content-img")]
    public string? content_img {get;set;}
    public string? tableData {get;set;}
    public string? openHourData {get; set;}
    public string? priceData {get; set;}
    public string? facilityInfo {get;set;}
    public string? covidInfo {get;set;}
}