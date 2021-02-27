namespace DataAccessLibrary.BusinessLogic
{
    using DataAccessLibrary.DataAccess;
    using DataAccessLibrary.Models;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the <see cref="UserDataCapture" />.
    /// </summary>
    public class UserDataCapture
    {
        public static int CreateUser(string name, string surname, string email, string password, string country, string colour, DateTime birthday, string cellNumber, string comments)
        {
            if (string.IsNullOrEmpty(colour))
            {
                colour = "none";
            }
            if (string.IsNullOrEmpty(comments))
            {
                comments = "none";
            }
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

            int id = lastID.Result;
            return id;
        }

        /// <returns>The <see cref="int"/>.</returns>
        public static int UpdateUser(int id, string name, string surname, string email, string password, string country, string colour, DateTime birthday, string cellNumber, string comments)
        {
            if (string.IsNullOrEmpty(colour))
            {
                colour = "none";
            }
            if (string.IsNullOrEmpty(comments))
            {
                comments = "none";
            }

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

            string sql = string.Format
              (@"UPDATE [UserDetailsCapture].[dbo].[tblUser]
                       SET [Name] = '{0}'
                          ,[Surname] = '{1}'
                          ,[Email] = '{2}'
                          ,[Password] = '{3}'
                          ,[Country] = '{4}'
                          ,[FavouriteColour] = '{5}'
                          ,[Birthday] = '{6}'
                          ,[CellphoneNumber] = '{7}'
                          ,[Comments] = '{8}'
                     WHERE id ='" + id + "'", data.Name, data.Surname, data.Email, data.Password,
               data.Country, data.FavouriteColour, data.Birthday, data.CellphoneNumber, data.Comments);

            int rowsAffected = SqlDataAccess.UpdateData(sql, data);

            return rowsAffected;
        }

        /// <summary>
        /// The ValidatePassword.
        /// </summary>
        /// <param name="password">The password<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool ValidatePassword(string password)
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
                            [id]
                           ,[Name]
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

        public static IEnumerable<string> ValidateEmail(string email)
        {
            string sql = @"SELECT
                           [Email]
                         FROM [UserDetailsCapture].[dbo].[tblUser] WHERE [Email] = '" + email + "'";

            return SqlDataAccess.CheckEmail<string>(sql);
        }
    }
}
