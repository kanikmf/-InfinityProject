namespace InfinityPlatform.MvcUI.ApiServices
{
    public interface IHttpApiService
  {
     

        Task<T> GetData<T>(string requestUri, string token = null);
        Task<T> PostData<T>(string requestUri, string jsonData, string token = null);
        Task<bool> DeleteData(string requestUri, string token = null);
    }
}
