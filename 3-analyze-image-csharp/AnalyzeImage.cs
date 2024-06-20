using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.BedrockRuntime;
using Amazon.BedrockRuntime.Model;

internal class ConverseMultimodal
{
    private const string FilePath = "../image.png";

    private static async Task RunMultimodalRequest()
    {
        var bedrock = new AmazonBedrockRuntimeClient(RegionEndpoint.USEast1);

        var modelId = "anthropic.claude-3-haiku-20240307-v1:0";

        try
        {
            using var imageBytes = new MemoryStream(await File.ReadAllBytesAsync(FilePath));

            var prompt = "This is the design of a birthday card for my grandma's 80'th birthday." +
                         "Please have a look and let me know if you think she's going to like it.";

            List<Message> messages =
            [
                new Message
                {
                    Role = ConversationRole.User,
                    Content =
                    [
                        new ContentBlock { Text = prompt },
                        new ContentBlock
                        {
                            Image = new ImageBlock
                            {
                                Format = ImageFormat.Png, Source = new ImageSource { Bytes = imageBytes }
                            }
                        }
                    ]
                }
            ];

            var response = await bedrock.ConverseAsync(new ConverseRequest { ModelId = modelId, Messages = messages });

            var responseText = response?.Output?.Message?.Content?.FirstOrDefault()?.Text ?? "";
            Console.WriteLine(responseText);
        }
        catch (Exception e)
        {
            Console.WriteLine($"ERROR: An unexpected error occurred. Reason: {e.Message}");
        }
    }

    public static async Task Main(string[] args)
    {
        await RunMultimodalRequest();
    }
}