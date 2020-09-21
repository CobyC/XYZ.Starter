using ServiceStack;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XYZ.Starter.Core
{
    public static class ContentHelper
    {
        public static StringContent GetStringContentAsJson(object obj)
        {
            return new StringContent(obj.ToJson(), Encoding.Default, "application/json");
        }

        public static async Task<T> ContentTo<T>(HttpContent content)
        {
            try
            {
                //ServiceStack
                return await JsonSerializer.DeserializeFromStreamAsync<T>
                   (await content.ReadAsStreamAsync());

                //Newtonsoft
                // return await JsonSerializer.DeserializeAsync<IEnumerable<CompanyFullDto>>
                //   (await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            //catch (UnsupportedContentTypeException contentex)
            //{
            //    throw contentex;
            //}
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
