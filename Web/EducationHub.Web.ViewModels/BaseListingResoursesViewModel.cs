﻿namespace EducationHub.Web.ViewModels
{
    using System;

    public abstract class BaseListingResoursesViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
