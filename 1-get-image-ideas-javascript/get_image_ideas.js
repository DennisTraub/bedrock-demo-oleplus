import {BedrockRuntimeClient, ConverseCommand} from "@aws-sdk/client-bedrock-runtime";

const client = new BedrockRuntimeClient({ region: "us-east-1" });

const modelId = "mistral.mistral-large-2402-v1:0";

const prompt =
    "It's my grandma's 80th birthdays and I need some design ideas for a card without text. " +
    "Please write return the results so that they can be used by image-generating AIs.";

console.log("Mistral is generating ideas...")
const command = new ConverseCommand({
    modelId,
    messages: [{ role: "user", content: [{ text: prompt }] }]
});

const response = await client.send(command);
const responseText = response.output.message.content[0].text;
console.log(responseText);
