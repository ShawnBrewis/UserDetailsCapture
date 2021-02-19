namespace UserDetailsCapture.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Defines the <see cref="EmailModel" />.
    /// </summary>
    public class EmailModel
    {
        /// <summary>
        /// Gets or sets the FromName.
        /// </summary>
        [Required, Display(Name = "Your name")]
        public string FromName { get; set; }

        /// <summary>
        /// Gets or sets the FromEmail.
        /// </summary>
        [Required, Display(Name = "Your email"), EmailAddress]
        public string FromEmail { get; set; }

        /// <summary>
        /// Gets or sets the Message.
        /// </summary>
        [Required]
        public string Message { get; set; }
    }
}