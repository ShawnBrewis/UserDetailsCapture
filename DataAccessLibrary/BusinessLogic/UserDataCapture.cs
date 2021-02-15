namespace DataAccessLibrary.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using DataAccessLibrary.DataAccess;
    using DataAccessLibrary.Models;

    /// <summary>
    /// Defines the <see cref="UserDataCapture" />.
    /// </summary>
    public class UserDataCapture
    {
        /// <summary>
        /// The CreateUser.
        /// </summary>
        /// <param name="firstName">The firstName<see cref="string"/>.</param>
        /// <param name="lastName">The lastName<see cref="string"/>.</param>
        /// <param name="email">The email<see cref="string"/>.</param>
        /// <param name="password">The password<see cref="string"/>.</param>
        /// <param name="country">The country<see cref="string"/>.</param>
        /// <param name="colour">The colour<see cref="string"/>.</param>
        /// <param name="birthday">The birthday<see cref="DateTime"/>.</param>
        /// <param name="cellNumber">The cellNumber<see cref="int"/>.</param>
        /// <param name="comments">The comments<see cref="string"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int CreateUser(string name, string surname, string email, string password, string country, string colour, DateTime birthday, int cellNumber, string comments)
        {
            int id = 0;

            if (string.IsNullOrEmpty(colour))
            {
                colour = "none";
            }
            if (string.IsNullOrEmpty(comments))
            {
                comments = "none";
            }

            if (ValidatePassword(password))
            {
                UserDetailsCaptureModelDB data = new UserDetailsCaptureModelDB
                {
                    Name = name,
                    Surname = surname,
                    Email = email,
                    Password = password,
                    Country = country,
                    FavouriteColour = colour,
                    Birthday = birthday,
                    CellphoneNumber = cellNumber,
                    Comments = comments
                };

                string sql = @"INSERT INTO [UserDetailsCapture].[dbo].[tblUser]
                             ([Name]
                            ,[Surname]
                            ,[Email]
                            ,[Password]
                            ,[Country]
                            ,[FavouriteColour]
                            ,[Birthday]
                            ,[CellphoneNumber]
                            ,[Comments])
                          values 
                             (
                               @Name 
                               ,@Surname
                               ,@Email
                               ,@Password
                               ,@Country
                               ,@FavouriteColour
                               ,@Birthday
                               ,@CellphoneNumber
                               ,@Comments
                             ); SELECT SCOPE_IDENTITY()";

                Task<int> lastID = SqlDataAccess.SaveDataAsync(sql, data);
                id = lastID.Result;
            }
            return id;
        }

        /// <summary>
        /// The GetInertedID.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int InertedID
        {
            get
            {
                var id = 0;
                string sql = @"SELECT IDENT_CURRENT('tblUser') AS '@id';";
                id = SqlDataAccess.GetIDAsync<int>(sql);
                return id;
            }
        }

        /// <summary>
        /// The ValidatePassword.
        /// </summary>
        /// <param name="password">The password<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private static bool ValidatePassword(string password)
        {
            Regex hasNumber = new Regex(@"[0-9]+");
            Regex hasUpperChar = new Regex(@"[A-Z]+");
            Regex hasMinimum8Chars = new Regex(@".{8,}");

            bool isValidated = hasNumber.IsMatch(password) && hasUpperChar.IsMatch(password) && hasMinimum8Chars.IsMatch(password);

            if (isValidated)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// The LoadUserDetails.
        /// </summary>
        /// <returns>The <see cref="List{UserDetailsCaptureModel}"/>.</returns>
        public static List<UserDetailsCaptureModelDB> LoadUserDetails(int id)
        {
            string sql = @"SELECT 
                            [Name]
                           ,[Surname]
                           ,[Email]
                           ,[Password]
                           ,[Country]
                           ,[FavouriteColour]
                           ,[Birthday]
                           ,[CellphoneNumber]
                           ,[Comments]
                         FROM [UserDetailsCapture].[dbo].[tblUser] WHERE id = '" + id + "'";

            List<UserDetailsCaptureModelDB> userDataCaptures = SqlDataAccess.LoadData<UserDetailsCaptureModelDB>(sql).ToList();
            return userDataCaptures.ToList();
        }
    }
}
