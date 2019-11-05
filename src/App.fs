module App

(**
Minimal application showing how to use Elmish
You can find more info about Emish architecture and samples at https://elmish.github.io/
*)

open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props
open Elmish
open Elmish.React

// MODEL

type Model =
    { Value : string }

type Msg =
    | ChangeValue of string

let init () = { Value = "Hello!" }

// UPDATE

let update (msg : Msg) (_ : Model) =
  match msg with
  | ChangeValue newValue ->
      { Value = newValue }

// VIEW (rendered with React)

let mainContainerStyle = 
  [ Display DisplayOptions.Flex
    JustifyContent AlignContentOptions.Center
    AlignItems AlignItemsOptions.Center 
    FlexDirection "column"
    Width "100vw" 
    Height "100vh" ]

let inputStyle =
  [ Padding ".25rem"
    FontSize "16px"
    Width "250px"
    Margin "1rem" ]

let view model dispatch =
    main [ Style mainContainerStyle ]
        [ input [ Style inputStyle
                  Value model.Value
                  OnChange (fun ev -> ev.target?value |> string |> ChangeValue |> dispatch) ]
          span [ ]
            [ str " — "
              str model.Value
              str " — " ] ]

// App
Program.mkSimple init update view
|> Program.withReactSynchronous "elmish-app"
|> Program.withConsoleTrace
|> Program.run