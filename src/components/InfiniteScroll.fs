namespace App.Components

open Fable.Core
open Fable.Core.JsInterop
open Fable.React

open Browser.Types

module InfiniteScroll =

  type InfiniteScrollProps =
    /// Name of the element that the component should render as.
    /// Defaults to 'div'.
    | Element of string option
    /// Whether there are more items to be loaded. Event listeners are removed if false.
    /// Defaults to false.
    | HasMore of bool option
    /// Whether the component should load the first set of items.
    /// Defaults to true.
    | InitialLoad of bool option
    /// Whether new items should be loaded when user scrolls to the top of the scrollable ar
    /// Default to false.
    | IsReverse of bool option
    /// A callback for when more items are requested by the user.
    /// Page param is next page index.
    | LoadMore of (float -> unit)
    /// The number of the first page to load, with the default of 0, the first page is 1.
    /// Defaults to 0.
    | PageStart of float option
    /// The distance in pixels before the end of the items that will trigger a call to loadM
    /// Defaults to 250.
    | Threshold of float option
    /// Proxy to the useCapture option of the added event listeners.
    /// Defaults to false.
    | UseCapture of bool option
    /// Add scroll listeners to the window, or else, the component's parentNode.
    /// Defaults to true.
    | UseWindow of bool option
    /// Loader component for indicating "loading more".
    | Loader of ReactElement option
    /// Override method to return a different scroll listener if it's not the immediate pare
    | GetScrollParent of (unit -> HTMLElement option)

  let inline infiniteScroll (props : InfiniteScrollProps list) (elems : ReactElement list) : ReactElement = 
    ofImport "default" "react-infinite-scroller"  (keyValueList CaseRules.LowerFirst props) elems