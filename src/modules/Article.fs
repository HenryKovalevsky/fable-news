namespace App.Modules

open System

open Fable.React
open Fable.React.Props

type Article =
  { Url: string
    ImgUrl: string
    Title: string
    Text: string
    Source: string
    PublishedAt: DateTime }

module Article =

  let article (model: Article) =
    article [ Class "article" ] [
      a [ Href model.Url; Target "_blank" ] [
        img [ Src model.ImgUrl; Class "article__img" ]
        h2 [ Class "article__title" ] [ str model.Title ]
        p [ Class "article__text" ] [ str model.Text ]
      ]
      section [ Class "article__meta" ] [
        span [] [ str <| sprintf "%s, %s" model.Source (model.PublishedAt.ToString "dd.mm.yyyy") ]
      ]
    ]