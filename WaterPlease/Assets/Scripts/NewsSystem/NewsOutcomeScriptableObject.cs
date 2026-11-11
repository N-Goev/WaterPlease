using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewsOutcome", menuName = "ScriptableObjects/NewsOutcomeScriptableObject")]
public class NewsOutcomeScriptableObject : ScriptableObject
{
    public Sprite CoverImage;
    public Sprite AuthorImage;
    public string authorName;
    public string publishDate;
    public string HeadlineText;
    public string detailsText;
    public List<NewsComment> comments;
}
