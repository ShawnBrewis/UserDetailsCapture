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
        public static int CreateUser(int Id, string firstName, string lastName, string email, string password, string country, string colour, DateTime birthday, int cellNumber, string comments)
        {
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
                    ID = Id,
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Password = password,
                    Country = country,
                    Colour = colour,
                    Birthday = birthday,
                    CellNumber = cellNumber,
                    Comments = comments
                };

                string sql = @"INSERT INTO [UserDetailsCapture].[dbo].[tblUser]
                             [id]                            (
                            ,[Name]
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
                               @Id
                               @FirstName, 
                               @LastName, 
                               @Email, 
                               @Password, 
                               @Country, 
                               @Colour, 
                               @Birthday, 
                               @CellNumber, 
                               @Comments
                             )";

                return SqlDataAccess.SaveData(sql, data);
            }
            return Id;
        }

        /// <summary>
        /// The GetInertedID.
        /// </summary>
        /// <param name="id">The id<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int GetInertedID(int id)
        {
            UserDetailsCaptureModelDB data = new UserDetailsCaptureModelDB
            {
                ID = id,
            };

            string sql = @"SELECT IDENT_CURRENT('tblUser');";
            return int.Parse(SqlDataAccess.GetID(id, sql).ToString());
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
        public static List<UserDetailsCaptureModelDB> LoadUserDetails()
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
                         FROM [UserDetailsCapture].[dbo].[tblUser]";

            List<UserDetailsCaptureModelDB> userDataCaptures = SqlDataAccess.LoadData<UserDetailsCaptureModelDB>(sql).ToList();
            return userDataCaptures.ToList();
        }

        /// <summary>
        /// Defines the <see cref="RestClient" />.
        /// </summary>
        public class RestClient
        {
            /// <summary>
            /// Defines the client.
            /// </summary>
            private HttpClient client;

            /// <summary>
            /// Defines the ApiUri.
            /// </summary>
            public const string ApiUri = "http://localhost:49619/";

            /// <summary>
            /// Defines the MediaTypeJson.
            /// </summary>
            public const string MediaTypeJson = "application/json";

            /// <summary>
            /// Defines the RequestMsg.
            /// </summary>
            public const string RequestMsg = "Request has not been processed";

            /// <summary>
            /// Gets or sets the ReasonPhrase.
            /// </summary>
            public static string ReasonPhrase { get; set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="RestClient"/> class.
            /// </summary>
            public RestClient()
            {
                this.client = new HttpClient();
                this.client.BaseAddress = new Uri(ApiUri);
                this.client.DefaultRequestHeaders.Accept.Clear();
                this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeJson));
            }

            /// <summary>
            /// The RunAsyncGetAll.
            /// </summary>
            /// <typeparam name="T">.</typeparam>
            /// <typeparam name="U">.</typeparam>
            /// <param name="uri">The uri<see cref="dynamic"/>.</param>
            /// <returns>The <see cref="Task{List{U}}"/>.</returns>
            public async Task<List<U>> RunAsyncGetAll<T, U>(dynamic uri)
            {
                HttpResponseMessage response = await this.client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    //return await response.Content.ReadAsAsync<List<U>>();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    throw new ApplicationException(response.ReasonPhrase);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadGateway)
                {
                    throw new Exception(response.ReasonPhrase);
                }

                throw new Exception(RequestMsg);
            }
        }
    }
}
