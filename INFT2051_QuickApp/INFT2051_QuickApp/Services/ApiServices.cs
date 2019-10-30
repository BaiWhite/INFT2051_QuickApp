using INFT2051_QuickApp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Json.Net;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Text;

namespace INFT2051_QuickApp.Services
{
    internal class ApiServices
    {
        public async Task<User> RegisterUserAsync(User user)
        {
            var client = new HttpClient();
            string endUrl = "api/users/passwordLogin?companyName=" + user.CompanyName + "&password=" + user.Password;
            string url = Constants.LocalServer + endUrl;
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(content);
                    if (user.UserID > 0)
                    {
                        return new User();
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("\tERROR {0}", e.Message);
                throw;
            }

            var json = JsonConvert.SerializeObject(user);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            
            try
            {
                HttpResponseMessage response = await client.PostAsync(Constants.LocalServer + "api/users", httpContent);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(content);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("\tERROR {0}", e.Message);
                throw;
            }

            return user;
        }

        public async Task<List<BusinessPage>> LoadPagesAsync()
        {
            var client = new HttpClient();

            var json = await client.GetStringAsync(
                Constants.LocalServer + "api/BusinessPages");

            var pages = JsonConvert.DeserializeObject<List<BusinessPage>>(json);

            return pages;
        }

        internal async Task<BusinessPage> UploadPageAsync(string companyName, string description, string storeLatitude, string storeLongitude)
        {
            var client = new HttpClient();
            BusinessPage businessPage = new BusinessPage
            {
                Description = description,
                StoreLatitude = storeLatitude,
                StoreLongitude = storeLongitude,
                CompanyName = companyName
            };

            var json = JsonConvert.SerializeObject(businessPage);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync(Constants.LocalServer + "api/BusinessPages", httpContent);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    businessPage = JsonConvert.DeserializeObject<BusinessPage>(content);
                    businessPage.UploadSuccess = true;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("\tERROR {0}", e.Message);
                throw;
            }

            return businessPage;
        }

        internal async Task<bool> PushImageAsync(string companyName, string description, string storeImage)
        {
            return true;
        }

        public async Task<User> LoginAsync(string companyName, string password) {
            var client = new HttpClient();
            User user = null;
            string endUrl = "api/users/passwordLogin?companyName=" + companyName + "&password=" + password;
            string url = Constants.LocalServer + endUrl;
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    user = JsonConvert.DeserializeObject<User>(content);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("\tERROR {0}", e.Message);
                throw;
            }

            return user;
        }

        internal async Task<bool> MakePaymentAsync(long userID, int cardNumber, string nameOnCard, string expiryDate, int securityCode)
        {
            var client = new HttpClient();
            User user = new User
            {
                CompanyName = ApplicationSettings.CompanyName,
                paid = true,
                Password = ApplicationSettings.Password,
                UserID = ApplicationSettings.UserID
            };
            string endUrl = "api/users/" + ApplicationSettings.UserID;

            var json = JsonConvert.SerializeObject(user);

            HttpContent httpContent = new StringContent(json);

            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                HttpResponseMessage response = await client.PutAsync(Constants.LocalServer + endUrl, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("\tERROR {0}", e.Message);
                throw;
            }
        }

        public async Task<MyPage> GetMyPageAsync(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var json = await client.GetStringAsync(Constants.LocalServer + "api/MyPage");

            var myPage = JsonConvert.DeserializeObject<MyPage>(json);
            return myPage;
        }

        public async Task PostMyPageAsync(MyPage myPage, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var json = JsonConvert.SerializeObject(myPage);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(Constants.LocalServer + "api/MyPage", content);
        }

        public async Task PutMyPageAsync(MyPage myPage, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var json = JsonConvert.SerializeObject(myPage);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PutAsync(
                Constants.LocalServer + "api/Ideas/" + myPage.UserID, content);
        }

        public async Task DeleteMyPageAsync(int UserID, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.DeleteAsync(
                Constants.LocalServer + "api/Ideas/" + UserID);
        }

        public async Task<List<MyPage>> SearchIdeasAsync(string keyword, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var json = await client.GetStringAsync(
                Constants.LocalServer + "api/ideas/Search/" + keyword);

            var pages = JsonConvert.DeserializeObject<List<MyPage>>(json);

            return pages;
        }
    }
}
