using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZZZ.ShoppingManager.UI.Models
{
    public class PagenationText
    {
        private Pagenation _pagenation;
        public PagenationText(Pagenation pagenation)
        {
            this._pagenation = pagenation;
        }
        public int FirstAfter { get; set; }
        public int LastBefore { get; set; }
        public int CurrentAfter { get; set; }
        public int CurrenntBefore { get; set; }
        public string SkitText { get; set; }
        public List<int> GetNumber()
        {
            var result = (Enumerable.Range(1, FirstAfter)
                .Concat(Enumerable.Range(_pagenation.PageCount - LastBefore + 1, LastBefore))
                .Concat(Enumerable.Range(_pagenation.PageNo - CurrenntBefore+1, CurrenntBefore + CurrentAfter + 1)))
                .Where(n => n >= 1 && n <= _pagenation.PageCount)
                .Distinct();
            return result.OrderBy(n => n).ToList();
        }
        public List<string> GetNumberText()
        {
            List<string> result = new List<string>();
            List<int> Numbers = GetNumber();
            for (int i = 0; i <= Numbers.Count-1; i++)
            {
                if (i < Numbers.Count - 1)
                {
                    if (Numbers[i + 1] - Numbers[i] > 1)
                    {
                        result.Add(Numbers[i].ToString());
                        result.Add(SkitText);
                    }
                    else
                    {
                        result.Add(Numbers[i].ToString());
                    }
                }
                else
                {
                    result.Add(Numbers[i].ToString());
                }

            }
            return result;
        }
    }
}