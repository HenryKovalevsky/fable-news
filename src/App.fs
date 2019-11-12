module App

open App.Modules
open App.Data

open Elmish

// MODEL

type Model = Article list

type PageNumber = int
type ErrorMessage = string

type Msg =
  | LoadData of PageNumber
  | DataLoaded of Article list
  | FetchFail of ErrorMessage

let init() = [], Cmd.none

// UPDATE

let update msg model =
  match msg with
  | LoadData page -> 
      model, Cmd.OfPromise.either 
        NewsApi.fetchNews page 
        (DataLoaded << id) 
        (FetchFail << fun exc -> exc.Message)
  | DataLoaded data ->
      model @ data, Cmd.none
  | FetchFail error ->
      printfn "Fetch failed — %s" error
      model, Cmd.none


// VIEW (rendered with React)

open Elmish.React
open Fable.React
open Fable.React.Props

open App.Components.InfiniteScroll
open App.Modules.Article

let view (model : Model) dispatch =
  main [ Class "main"] [
    header [ Class "header" ] [
      h1 [] [ str "Fable News" ]
      div [] [
        span [] [ str "Powered by " ] 
        a [ Href "https://newsapi.org"; Target "_blank"; Class "link" ] [ str "News API" ]
      ]
    ]
    hr []
    infiniteScroll [ LoadMore (dispatch << LoadData << int); HasMore (Some true); ] [
      div [] (List.map article model)
    ] 
  ]

// App
Program.mkProgram init update view
|> Program.withReactSynchronous "elmish-app"
|> Program.withConsoleTrace
|> Program.run