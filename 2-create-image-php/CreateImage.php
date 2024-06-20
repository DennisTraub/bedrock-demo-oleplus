<?php

use Aws\BedrockRuntime\BedrockRuntimeClient;

include __DIR__ . "/vendor/autoload.php";

class BedrockRuntimeService
{
    private string $file_name = "../image.png";

    public function createImage()
    {
        $bedrock = new BedrockRuntimeClient([ "region" => "us-east-1" ]);

        $modelId = "amazon.titan-image-generator-v1";

        $prompt = "A stylized illustration of a hot air balloon, with the number '80' " .
            "on its side, soaring above a serene landscape to signify your grandma's " .
            "life journey and new heights she has reached";

        echo "Creating image with prompt: $prompt...\n";
        try {
            $result = $bedrock->invokeModel([
                'contentType' => 'application/json',
                'modelId' => $modelId,
                'body' => json_encode([
                    'imageGenerationConfig' => [ 'seed' => rand(0, 2147483647) ],
                    'textToImageParams' => [ 'text' => $prompt ],
                    'taskType' => 'TEXT_IMAGE'
                ])
            ]);

            $response_body = json_decode($result['body']);

            $base64 = $response_body->images[0];

            $this->saveImage($base64);

            echo "Image saved to $this->file_name\n";

        } catch (Exception $e) {
            echo "Error: ({$e->getCode()}) - {$e->getMessage()}";
        }
    }

    private function saveImage($base64)
    {
        $image_data = base64_decode($base64);
        $file = fopen($this->file_name, 'wb');
        fwrite($file, $image_data);
        fclose($file);
    }
}

$runner = new BedrockRuntimeService();
$runner->createImage();