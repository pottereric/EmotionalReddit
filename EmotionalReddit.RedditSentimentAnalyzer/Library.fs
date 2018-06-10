namespace EmotionalReddit.RedditSentimentAnalyzer

module Say =
    let hello name =
        printfn "Hello %s" name

    let Foo () =
        Reddit.GetTopProgrammingTitles()
        |> List.map(fun t ->
            let text, score = t
            text)
    //let Foo =
    //    [
    //        "FSharpStory1";
    //        "FSharpStory2";
    //        "FSharpStory3";
    //        "FSharpStory4";
    //    ]
