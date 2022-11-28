using BotAPI.ResponseModel;
using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace BotAPI.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static T? ParseCsvResponseToObject<T>(this HttpResponseMessage httpResponseMessage)
        {
            T? mappedObject;

            var contentStream = httpResponseMessage.Content.ReadAsStream();

            var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HeaderValidated = null,
            };

            using (var sr = new StreamReader(contentStream))
            using (var csvReader = new CsvReader(sr, csvConfiguration))
            {
                var csv = csvReader.GetRecords<T>();

                if (csv == null)
                    mappedObject = default;
                else
                {
                    try
                    {
                        mappedObject = csv.FirstOrDefault()!;
                    }
                    catch (Exception)
                    {

                        mappedObject = default;
                    }
                }
            }

            return mappedObject;
        }
    }
}
