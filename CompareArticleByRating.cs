using System.Collections.Generic;

namespace Lab05_CSharp
{
    internal class CompareArticleByRating : IComparer<Article>
    {
        public int Compare(Article x, Article y)
        {
            return x.articleRating.CompareTo(y.articleRating);
        }
    }
}
