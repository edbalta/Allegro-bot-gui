using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Leaf.xNet;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace Allegro_bot_gui
{
    public class ReviewModel
    {
        public string reviewId;
        public string reviewBody;
        public bool reviewState;
        public ReviewModel(string reviewId, string reviewBody, bool reviewState)
        {
            this.reviewId = reviewId;
            this.reviewBody = reviewBody;
            this.reviewState = reviewState;
        }
        public ReviewModel()
        {
            
        }
        public override string ToString()
        {
            return reviewBody;
        }

    }
    public class ReviewScrapper
    {
        public string GetProductID(string url)
        {
            string product_id = null;
            if (url.Contains("?"))
            {
                string[] url_parts = url.Split(new string[] { "https://allegro.pl/oferta/" }, StringSplitOptions.None)[1].Split(new string[] { "?" }, StringSplitOptions.None);
                string[] id_parts = url_parts[0].Split(new string[] { "-" }, StringSplitOptions.None);
                foreach (string item in id_parts)
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(item, "^[0-9]*$") && (item.Length == 11 || item.Length == 10))
                    {
                        product_id = item;
                    }
                }
            }
            else if (url.Contains("#product"))
            {
                string[] url_parts = url.Split(new string[] {
                    "https://allegro.pl/oferta/" }, StringSplitOptions.None)[1].Split(new string[] { "#productReviews" }, StringSplitOptions.None);
                string[] id_parts = url_parts[0].Split(new string[] { "-" }, StringSplitOptions.None);
                foreach (string item in id_parts)
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(item, "^[0-9]*$") && (item.Length == 11 || item.Length == 10))
                    {
                        product_id = item;
                    }
                }
            }
            else
            {
                string[] id_parts = url.Split(new string[] { "https://allegro.pl/oferta/" }, StringSplitOptions.None)[1].Split(new string[] { "-" }, StringSplitOptions.None);
                foreach (string item in id_parts)
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(item, "^[0-9]*$") && (item.Length == 11 || item.Length == 10))
                    {
                        product_id = item;
                    }
                }
            }
            return product_id;
        }
        public async Task<List<ReviewModel>> ScrapeAllegroReviews(string url)
        {
            // Extract product ID from URL
            string product_id = null;
            if (url.Contains("?"))
            {
                string[] url_parts = url.Split(new string[] { "https://allegro.pl/oferta/" }, StringSplitOptions.None)[1].Split(new string[] { "?" }, StringSplitOptions.None);
                string[] id_parts = url_parts[0].Split(new string[] { "-" }, StringSplitOptions.None);
                foreach (string item in id_parts)
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(item, "^[0-9]*$") && (item.Length == 11 || item.Length == 10))
                    {
                        product_id = item;
                    }
                }
            }
            else if (url.Contains("#product"))
            {
                string[] url_parts = url.Split(new string[] {
                    "https://allegro.pl/oferta/" }, StringSplitOptions.None)[1].Split(new string[] { "#productReviews" }, StringSplitOptions.None);
                string[] id_parts = url_parts[0].Split(new string[] { "-" }, StringSplitOptions.None);
                foreach (string item in id_parts)
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(item, "^[0-9]*$") && (item.Length == 11 || item.Length == 10))
                    {
                        product_id = item;
                    }
                }
            }
            else
            {
                string[] id_parts = url.Split(new string[] { "https://allegro.pl/oferta/" }, StringSplitOptions.None)[1].Split(new string[] { "-" }, StringSplitOptions.None);
                foreach (string item in id_parts)
                {
                    if (System.Text.RegularExpressions.Regex.IsMatch(item, "^[0-9]*$") && (item.Length == 11 || item.Length == 10))
                    {
                        product_id = item;
                    }
                }
            }

            if (product_id == null)
            {
                return new List<ReviewModel>();
            }


            // Create list to hold scraped reviews
            List<ReviewModel> scrapedReviews = new List<ReviewModel>();

            // Make initial request for reviews
            string initialUri = $"https://edge.allegro.pl/offers/{product_id}?include=product.reviews&product.reviews.page=1&product.reviews.sortBy=MINE_THEN_MOST_HELPFUL&destination=SHOW-OFFER_PRODUCT-REVIEWS-PAGINATION_OFDF";
            HttpRequest r = new HttpRequest();
            r.AddHeader("accept", "application/vnd.allegro.offer.view.internal.v1+json");
            r.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/105.0.0.0 Safari/537.36");

                dynamic initialResponse = JsonConvert.DeserializeObject<dynamic>(r.Get(initialUri).ToString());
                int totalPages = initialResponse.product.reviews.pagination.totalPages;
                Console.WriteLine($"Scrapping reviews, total pages: {totalPages}");
                foreach (var review in initialResponse.product.reviews.opinions)
                {
                    ReviewModel reviewObj = new ReviewModel();
                    reviewObj.reviewId = review.id;
                    reviewObj.reviewBody = review.opinion;
                    reviewObj.reviewState = false;
                    if (reviewObj.reviewBody != null && reviewObj.reviewId != null)
                    {
                    reviewObj.reviewBody = JsonConvert.DeserializeObject<string>(JsonConvert.SerializeObject(review.opinion)).Replace("\n", ".").Replace("\n\n", ".");  
                    scrapedReviews.Add(reviewObj);
                    }
                    else
                    {
                        Console.WriteLine($"Review {scrapedReviews.Count + 1} on page 1 was null");
                        string pros = review.pros ?? "null";
                        string cons = review.cons ?? "null";
                        reviewObj.reviewBody = $"(Null Review) Pros: {pros}, Cons: {cons}";
                        scrapedReviews.Add(reviewObj);
                    }
                }

                // Make requests for remaining pages of reviews
                for (int i = 2; i <= totalPages; i++)
                {
                    string uri = $"https://edge.allegro.pl/offers/{product_id}?include=product.reviews&product.reviews.page={i}&product.reviews.sortBy=MINE_THEN_MOST_HELPFUL&destination=SHOW-OFFER_PRODUCT-REVIEWS-PAGINATION_OFDF";
                    HttpRequest req = new HttpRequest();
                    req.AddHeader("accept", "application/vnd.allegro.offer.view.internal.v1+json");
                    req.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/105.0.0.0 Safari/537.36");
                    dynamic ResponseJson = JsonConvert.DeserializeObject < dynamic >(req.Get(uri).ToString());
                    Console.WriteLine($"Scrapped Page: {i}");
                    foreach (var review in ResponseJson.product.reviews.opinions)
                    {
                        ReviewModel reviewObj = new ReviewModel();
                        reviewObj.reviewId = review.id;
                        reviewObj.reviewBody = review.opinion;
                        reviewObj.reviewState = false;
                        if (reviewObj.reviewBody != null && reviewObj.reviewId != null)
                        {
                            reviewObj.reviewBody = JsonConvert.DeserializeObject<string>(JsonConvert.SerializeObject(review.opinion)).Replace("\n", ".").Replace("\n\n", ".");
                            scrapedReviews.Add(reviewObj);
                        }
                        else
                        {
                            Console.WriteLine($"Review {scrapedReviews.Count + 1} on page {i} was null");
                            string pros = review.pros ?? "null";
                            string cons = review.cons ?? "null";
                            reviewObj.reviewBody = $"(Null Review) Pros: {pros}, Cons: {cons}";
                            scrapedReviews.Add(reviewObj);
                        }
                    }

                }
                Console.WriteLine("Scrapping complete.");
                return scrapedReviews;
        }
        public async Task<List<ReviewModel>> FilterSavedReviews(List<ReviewModel> savedReviews, string url)
        {
            // load saved reviews from JSON file
            // scrape new reviews
            List<ReviewModel> newReviews = await ScrapeAllegroReviews(url);
            Dictionary<string, ReviewModel> seenReviews= new Dictionary<string, ReviewModel>();
            // create a new array for filtered reviews
            List<ReviewModel> filteredReviews = new List<ReviewModel>();
            foreach (var r in savedReviews)
            {
                seenReviews[r.reviewId] = r;
            }
            foreach (var review in newReviews)
            {
                if (seenReviews.ContainsKey(review.reviewId))
                {
                    filteredReviews.Add(seenReviews[review.reviewId]);
                } else
                {
                    filteredReviews.Add(review);
                }
            }
            return filteredReviews;
        }

    }
    }

