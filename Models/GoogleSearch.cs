using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class GoogleSearch {

    public string searchTerm { get; set; }

    public async Task<GoogleRootObject> CustomSearch(string searchTerm){
        GoogleRootObject googleData = 
            await API.GetData<GoogleRootObject>($"https://www.googleapis.com/customsearch/v1?key=AIzaSyDuiGEHCsPE0xyxB_2_mKnYSGcGOmqIQcU&cx=005799490306923276768:cnqn7nbjlxe&q={searchTerm}");
        return googleData;
    }

}

public class Url
{
    public string type { get; set; }
    public string template { get; set; }
}

public class Request
{
    public string title { get; set; }
    public string totalResults { get; set; }
    public string searchTerms { get; set; }
    public int count { get; set; }
    public int startIndex { get; set; }
    public string inputEncoding { get; set; }
    public string outputEncoding { get; set; }
    public string safe { get; set; }
    public string cx { get; set; }
}

public class NextPage
{
    public string title { get; set; }
    public string totalResults { get; set; }
    public string searchTerms { get; set; }
    public int count { get; set; }
    public int startIndex { get; set; }
    public string inputEncoding { get; set; }
    public string outputEncoding { get; set; }
    public string safe { get; set; }
    public string cx { get; set; }
}

public class Queries
{
    public List<Request> request { get; set; }
    public List<NextPage> nextPage { get; set; }
}

public class Context
{
    public string title { get; set; }
}

public class SearchInformation
{
    public double searchTime { get; set; }
    public string formattedSearchTime { get; set; }
    public string totalResults { get; set; }
    public string formattedTotalResults { get; set; }
}

public class Offer
{
    public string price { get; set; }
    public string availability { get; set; }
    public string itemcondition { get; set; }
}

public class CseThumbnail
{
    public string width { get; set; }
    public string height { get; set; }
    public string src { get; set; } = "";
}

public class Product
{
    public string name { get; set; }
    public string manufacturer { get; set; }
    public string image { get; set; }
    public string description { get; set; }
}

// public class Metatag
// {
//     public string __invalid_name__og:title { get; set; }
//     public string __invalid_name__og:type { get; set; }
//     public string __invalid_name__og:description { get; set; }
//     public string __invalid_name__og:site_name { get; set; }
//     public string __invalid_name__og:url { get; set; }
//     public string __invalid_name__og:image { get; set; }
//     public string __invalid_name__globalsign-domain-verification { get; set; }
//     public string __invalid_name__p:domain_verify { get; set; }
//     public string __invalid_name__msvalidate.01 { get; set; }
// }

public class CseImage
{
    public string src { get; set; }
}

public class Hproduct
{
    public string description { get; set; }
    public string fn { get; set; }
    public string photo { get; set; }
    public string currency { get; set; }
    public string currency_iso4217 { get; set; }
}

public class Pagemap
{
    public List<Offer> offer { get; set; }
    public List<CseThumbnail> cse_thumbnail { get; set; } = new List<CseThumbnail>();
    public List<Product> product { get; set; }
    // public List<Metatag> metatags { get; set; }
    public List<CseImage> cse_image { get; set; } = new List<CseImage>();
    public List<Hproduct> hproduct { get; set; }
}

public class Item
{
    public string kind { get; set; }
    public string title { get; set; }
    public string htmlTitle { get; set; }
    public string link { get; set; }
    public string displayLink { get; set; }
    public string snippet { get; set; }
    public string htmlSnippet { get; set; }
    public string cacheId { get; set; }
    public string formattedUrl { get; set; }
    public string htmlFormattedUrl { get; set; }
    public Pagemap pagemap { get; set; }
}

public class GoogleRootObject
{
    public string kind { get; set; }
    public Url url { get; set; }
    public Queries queries { get; set; }
    public Context context { get; set; }
    public SearchInformation searchInformation { get; set; }
    public List<Item> items { get; set; }
}