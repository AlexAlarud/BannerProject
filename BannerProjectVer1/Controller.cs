using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BannerProjectVer1
{
    class Controller
    {

        private DAL dal;

        public Controller()
        {
            this.dal = new DAL();
        }

        //Colors

        public List<Color> ReadAllColors()
        {
            return dal.ReadAllColors();
        }

        //Patterns

        public List<Pattern> ReadAllPatterns()
        {
            return dal.ReadAllPatterns();
        }


    //Category
        public int CreateCategory(String categoryName)
        {
            return dal.CreateCategory(categoryName);
        }

        public int UpdateCategory(int categoryID, string categoryName)
        {
            return dal.UpdateCategory(categoryID, categoryName);
        }

        public int DeleteCategory(int categoryID)
        {
            return dal.DeleteCategory(categoryID);
        }

        public List<Category> ReadAllCategorys()
        {
            return dal.ReadAllCategorys();
        }

        public List<Category> ReadAllSelectableCategorys()
        {
            return dal.ReadAllSelectableCategorys();
        }

    //Banner

        public int CreateBanner(string bannerName, string bannerColor, string bannerPicture, int bannerCategoryId, List<int> secondaryCategoryIDs, List<BannerStep> bannerSteps)
        {
            return dal.CreateBanner(bannerName, bannerColor, bannerPicture, bannerCategoryId, secondaryCategoryIDs, bannerSteps);
        }


    }
}
