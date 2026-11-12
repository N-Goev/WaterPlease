using System.Collections.Generic;
using UnityEngine;

public class NewsOutcomeHandler : MonoBehaviour
{
    [SerializeField] private List<NewsOutcomeScriptableObject> Level1PossibleOutcomes = new();
    [SerializeField] private List<NewsOutcomeScriptableObject> Level2PossibleOutcomes = new();
    [SerializeField] private List<NewsOutcomeScriptableObject> Level3PossibleOutcomes = new();

    public NewsOutcomeScriptableObject GetOutcomeInfo(int SceneIndex, List<StampResult> stampResults)
    {
        switch (SceneIndex)
        {
            case 1:
                return GetLevel1Outcome(stampResults);

            case 2:
                return GetLevel2Outcome(stampResults);

            case 3:
                return GetLevel3Outcome(stampResults);

            default:
                Debug.LogError("Incorrect scene index(" + SceneIndex + ") when loading news outcome.");
                return null;
        }
    }

    private NewsOutcomeScriptableObject GetLevel1Outcome(List<StampResult> stampResults)
    {
        if (Level1PossibleOutcomes == null && Level1PossibleOutcomes.Count <= 0) { return null; }
        if (stampResults.Count != 3) { return null; }

        StampResult result1 = stampResults[0];
        StampResult result2 = stampResults[1];
        StampResult result3 = stampResults[2];

        //if(result2 == StampResult.Denied)
        //{
        //    return Level1PossibleOutcomes[1];
        //}
        //else if (result1 == StampResult.Denied && result3 == StampResult.Denied)
        //{
        //    return Level1PossibleOutcomes[0];
        //}
        //else if (result1 == StampResult.Approved && result3 == StampResult.Approved)
        //{
        //    return Level1PossibleOutcomes[3];
        //}
        //else
        //{
        //    return Level1PossibleOutcomes[2];
        //}

        //deny construction worker
        if (result2 == StampResult.Denied)
        {
            return Level1PossibleOutcomes[1];
        }
        //approve construction worker
        else
        {
            return Level1PossibleOutcomes[0];
        }
    }

    private NewsOutcomeScriptableObject GetLevel2Outcome(List<StampResult> stampResults)
    {
        if (Level1PossibleOutcomes == null && Level1PossibleOutcomes.Count <= 0) { return null; }
        if (stampResults.Count != 4) { return null; }

        StampResult result1 = stampResults[0];
        StampResult result2 = stampResults[1];
        StampResult result3 = stampResults[2];
        StampResult result4 = stampResults[3];

        //decline riot police
        if (result4 == StampResult.Denied)
        {
            return Level2PossibleOutcomes[3];
        }
        //decline firefighter
        else if (result1 == StampResult.Denied)
        {
            return Level2PossibleOutcomes[1];
        }
        //approve everything
        else if (result2 == StampResult.Approved && result3 == StampResult.Approved)
        {
            return Level2PossibleOutcomes[0];
        }
        //approve beer company
        else if (result2 == StampResult.Approved)
        {
            return Level2PossibleOutcomes[2];
        }
        //perfect outcome
        else
        {
            return Level2PossibleOutcomes[4];
        }
    }

    private NewsOutcomeScriptableObject GetLevel3Outcome(List<StampResult> stampResults)
    {
        if (Level1PossibleOutcomes == null && Level1PossibleOutcomes.Count <= 0) { return null; }
        if (stampResults.Count != 5) { return null; }

        StampResult result1 = stampResults[0];
        StampResult result2 = stampResults[1];
        StampResult result3 = stampResults[2];
        StampResult result4 = stampResults[3];
        StampResult result5 = stampResults[4];

        //perfect outcome
        if (result1 == StampResult.Approved && result2 == StampResult.Approved && result5 == StampResult.Approved)
        {
            return Level3PossibleOutcomes[0];
        }
        else if (result5 == StampResult.Denied)
        {
            return Level3PossibleOutcomes[2];
        }
        else if (result1 == StampResult.Denied)
        {
            return Level3PossibleOutcomes[1];
        }
        else
        {
            return Level3PossibleOutcomes[0];
        }
    }
}
