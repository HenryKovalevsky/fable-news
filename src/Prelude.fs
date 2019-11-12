namespace Prelude

open System

[<AutoOpen>]
module Prelude =
  let inline flip f x y = f y x

  // $ - function application
  let inline (^) f x = f x 

  let (|DateTime|_|) str =
    match DateTime.TryParse str with
    | true, dt -> Some(dt)
    | _ -> None