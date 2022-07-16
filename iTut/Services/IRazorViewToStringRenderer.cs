using System.Threading.Tasks;

namespace iTut.Services
{
    public interface IRazorViewToStringRenderer
    {
        Task<string> RenderViewToStringAsync<T>(string viewName, T model);
    }
}
