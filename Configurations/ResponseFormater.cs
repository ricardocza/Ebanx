using Ebanx.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System.Collections;
using System.Text;

namespace Ebanx.Configurations;

public class ResponseFormater : TextOutputFormatter
{
    public ResponseFormater()
    {
        SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/json"));
        SupportedEncodings.Add(Encoding.UTF8);
        SupportedEncodings.Add(Encoding.Unicode);
    }

    public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
    {
        var response = context.HttpContext.Response;

        var buffer = new StringBuilder();        
        var data = context.Object as ResponseDto;
        if (data != null)
        {
            buffer.Append(data?.ToString());
        }
        else
        {
            buffer.Append(context.Object);
        }

        await response.WriteAsync(buffer.ToString());
    }
}
