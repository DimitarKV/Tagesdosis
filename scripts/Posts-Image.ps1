param (
   [string]$imageName = "tagesdosis-posts",
   [Parameter(Mandatory=$true)][string]$action,
   [Parameter(Mandatory=$true)][string]$technology
)

function Create-Image {
	[CmdletBinding()]
	param(
		[Parameter()]
		[string] $technology
	)
	
    if ($technology -eq "rest")
    {
        docker build -t $imageName -f ../src/Services/Tagesdosis.Services.Posts/Tagesdosis.Services.Posts.Api/Dockerfile ../
        return;
    }
    if ($technology -eq "grpc")
    {
        docker build -t $imageName -f ../src/Services/Tagesdosis.Posts.Posts/Tagesdosis.Services.Posts.Grpc/Dockerfile ../
        return;
    }

    Write-Output "The post microservice does not support this technology"
}

switch ($action)
{
    "build" { Create-Image -technology $technology}
    "push" {}
    Default { write-output "No such action." }
}
