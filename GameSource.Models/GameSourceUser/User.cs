﻿using GameSource.Models.GameSource;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameSource.Models.GameSourceUser
{
    public class User : IdentityUser<int>
    {
        public User()
        {

        }

        [StringLength(20)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(20)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName { get { return $"{FirstName} {LastName}"; } }

        public int? Age { get; set; }

        public string Location { get; set; }

        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }

        public int UserStatusID { get; set; }

        public int UserRoleID { get; set; }

        [Display(Name = "User Status")]
        public UserStatus UserStatus { get; set; }

        [Display(Name = "User Role")]
        public UserRole UserRole { get; set; }

        public UserProfile UserProfile { get; set; }

        public ICollection<UserProfileComment> UserProfileCommentsCreated { get; set; }

        public ICollection<NewsArticle> NewsArticlesCreated { get; set; }
    }
}
