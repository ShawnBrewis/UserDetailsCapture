﻿namespace UserDetailsCapture.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the <see cref="UserDetailsCaptureModel" />.
    /// </summary>
    public class UserDetailsCaptureModel
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name Is Required")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Surname.
        /// </summary>
        [Display(Name = "Surname")]
        [Required(ErrorMessage = "Surname Is Required")]
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the Email.
        /// </summary>
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Email Is Required")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the Password.
        /// </summary>
        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Your password must contain 6 characters")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the ConfirmPassword.
        /// </summary>
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The Password Does Not Match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or sets the FavouriteColour.
        /// </summary>
        [Display(Name = "Favourite Colour")]
        public string FavouriteColour { get; set; }

        /// <summary>
        /// Gets or sets the Country.
        /// </summary>
        [Display(Name = "Country")]
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the Birthday.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Gets or sets the CellphoneNumber.
        /// </summary>
        //[Range(100000000, 999999999, ErrorMessage = "Cell Number must contain 10 numbers")]
        //[DataType(DataType.PhoneNumber)]
        [DisplayFormat(DataFormatString = "{0:###-###-####}")]
        public string CellphoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the Comments.
        /// </summary>
        [DataType(DataType.MultilineText)]
        public string Comments { get; set; }

        /// <summary>
        /// Gets or sets the Day.
        /// </summary>
        public int Day { get; set; }

        /// <summary>
        /// Gets or sets the Month.
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// Gets or sets the Year.
        /// </summary>
        public int Year { get; set; }

        public bool isUpdate { get; set; }


    }
}
