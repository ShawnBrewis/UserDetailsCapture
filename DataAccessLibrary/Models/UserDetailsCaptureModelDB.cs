namespace DataAccessLibrary.Models
{
    using System;

    /// <summary>
    /// Defines the <see cref="UserDetailsCaptureModelDB" />.
    /// </summary>
    public class UserDetailsCaptureModelDB
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the FirstName.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the LastName.
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Gets or sets the Email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the Password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the Colour.
        /// </summary>
        public string FavouriteColour { get; set; }

        /// <summary>
        /// Gets or sets the Country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets the Birthday.
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// Gets or sets the CellNumber.
        /// </summary>
        public string CellphoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the Comments.
        /// </summary>
        public string Comments { get; set; }

    }
}
