﻿namespace EducationHub.Web.ViewModels.Forum.Home
{
    using System.Collections.Generic;

    public class HomePageViewModel
    {
        public IEnumerable<HomePageCategoryViewModel> Categories { get; set; }

        public IEnumerable<HomePagePostViewModel> Posts { get; set; }
    }
}
