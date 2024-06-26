## Prepare:

```shell
cd 1-get-image-ideas-javascript

npm install

cd ../2-create-image-php

composer install

cd ../3-analyze-image-csharp

dotnet build
```

## Run:

**1: Generate ideas with Mistral using JavaScript**
```shell
cd 1-get-image-ideas-javascript
node get_image_ideas.js
```

This just generates a list, there's nothing you need to do. Step 2 has a prepared prompt that you can change in the script itself.

**2: Create an image with Titan using PHP**
```shell
cd ../2-create-image-php
php CreateImage.php
```

This uses a prepared prompt. The image will be stored in the project root as image.png

**3. Analyze the image with Claude 3 using C#**
```shell
cd ../3-analyze-image-csharp
dotnet run
```

This will automatically pick up the image generated by step 2.