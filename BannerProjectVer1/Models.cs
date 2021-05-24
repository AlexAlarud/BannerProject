using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BannerProjectVer1
{
    class Color
    {
        public Color() { }

        public Color(string colorID, string colorName)
        {
            this.ColorID = colorID;
            this.ColorName = colorName;
        }

        public string ColorID { set; get; }
        public string ColorName { set; get; }

    }

    class Pattern
    {
        public Pattern() { }

        public Pattern(string patternID, string patternName, string patternPicture)
        {
            this.PatternID = patternID;
            this.PatternName = patternName;
            this.PatternPicture = patternPicture;
        }

        public string PatternID { set; get; }
        public string PatternName { set; get; }
        public string PatternPicture { set; get; }

    }

    class Category
    {

        public Category() { }

        public Category(int categoryID, string categoryName)
        {
            this.CategoryID = categoryID;
            this.CategoryName = categoryName;
        }

        public int CategoryID { set; get; }

        public string CategoryName { set; get; }
    }

    class BannerStep
    {
        public BannerStep() { }

        public BannerStep(int bannerID, int stepNumber, string colorID, string patternID)
        {
            this.BannerStepBannerID = bannerID;
            this.BannerStepNumber = stepNumber;
            this.BannerStepColorID = colorID;
            this.BannerStepPatternID = patternID;
        }

        public int BannerStepBannerID { set; get; }
        public int BannerStepNumber { set; get; }
        public string BannerStepColorID { set; get; }
        public string BannerStepPatternID { set; get; }


    }

}
