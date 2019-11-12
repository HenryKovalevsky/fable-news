namespace App.Data

open Fetch
open Thoth

open Prelude

open App.Modules

module NewsApi =

  type private Source =
    { Name: string }

  type private Article = 
    { Url: string 
      UrlToImage: string option
      Title: string
      Description: string
      Source: Source
      PublishedAt: string }

  type private News = 
    { articles: Article list
      totalResults: int }

  let private mapArticleModel (a : Article) =
    match a.UrlToImage, a.PublishedAt with
    | Some imgUrl, DateTime dateTime ->
        Some { Url = a.Url
               ImgUrl = imgUrl
               Title = a.Title
               Text = a.Description
               Source = a.Source.Name
               PublishedAt = dateTime }
    | _ -> None

  let [<Literal>] ApiKey = "b84d4316a55c468fa5023b2b39d24cb3"

  let private getNewsPageUrl page =
    sprintf "https://newsapi.org/v2/everything?q=fable&language=en&page=%i&apiKey=%s" page ApiKey

  let fetchNews page =
    fetch (getNewsPageUrl page) [] 
    |> Promise.bind (fun res -> res.text())
    |> Promise.map (fun txt -> 
      let decoded = Json.Decode.Auto.fromString<News> (txt, isCamelCase=true)
      match decoded with 
      | Ok news -> List.choose mapArticleModel news.articles
      | Error decodingError -> failwith (sprintf "Unable to decode: %s. Reason: %s" txt decodingError)
      )