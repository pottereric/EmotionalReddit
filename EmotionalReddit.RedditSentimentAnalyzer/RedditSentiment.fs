namespace EmotionalReddit.RedditSentimentAnalyzer

module RedditSentiment =

    type RedditTitleSentiment = 
        { Votes: int
          Title: string
          Sentiment: float option
        }

    let convertTitlesToInputs titles =
        titles |> List.mapi (fun i t -> 
            let text, score = t
            "en", i.ToString(), text)


    let getSentimentAndTitlesForsubreddit subscriptionKey subreddit sort =
        let titles = Reddit.GetTitles subreddit sort
        let inputs = titles |> convertTitlesToInputs

        let client = FognitiveServices.Text.Client.create subscriptionKey Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models.AzureRegions.Westcentralus
        let result = FognitiveServices.Text.Client.sentimentAnalysis client inputs

        let sentiments = result.Documents |> Seq.map(fun d -> d.Id, (d.Score |> Option.ofNullable)) |> List.ofSeq
        let combinedLists = List.zip3 titles inputs sentiments
        combinedLists |> List.map(fun item -> 
            let (text, score),(lang, id, title), (id2, sent) = item
            {Votes = score; Title = text; Sentiment = sent}
        )


